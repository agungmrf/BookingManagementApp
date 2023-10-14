using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class RoomController : ControllerBase // ControllerBase untuk controller tanpa view
{
    // Repository untuk booked room today
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoomRepository _roomRepository;

    public RoomController(IRoomRepository roomRepository, IBookingRepository bookingRepository,
        IEmployeeRepository employeeRepository)
    {
        _roomRepository = roomRepository;
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
    }

    // Untuk menangani request GET dengan route api/[controller]/GetAll
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _roomRepository.GetAll();
        if (!result.Any())
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        // Mengubah IEnumerable<Room> menjadi IEnumerable<RoomDto>
        var data = result.Select(x => (RoomDto)x);

        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(data));
    }

    // Untuk menangani request GET dengan route api/[controller]/guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _roomRepository.GetByGuid(guid);
        if (result is null)
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<RoomDto>((RoomDto)result));
    }

    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(CreateRoomDto createRoomDto)
    {
        try
        {
            // Membuat data baru di database.
            var result = _roomRepository.Create(createRoomDto);

            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<RoomDto>("Data has been created successfully") { Data = (RoomDto)result });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(RoomDto roomDto)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            Room toUpdate = roomDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity yang diambil dari database.

            _roomRepository.Update(toUpdate);

            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<RoomDto>("Data has been updated successfully")
                { Data = (RoomDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    // Untuk menangani request DELETE dengan route api/[controller]
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _roomRepository.GetByGuid(guid);
            if (entity is null) return NotFound(new ResponseNotFoundHandler("Data Not Found"));

            // Menghapus data di database berdasarkan guid.
            _roomRepository.Delete(entity);

            // Setelah data berhasil dihapus, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError,
                new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }

    [HttpGet("reserved-rooms-today")]
    [AllowAnonymous]
    public IActionResult GetReservedRoomsToday()
    {
        // Mendapatkan daftar semua booking, employee, dan room dari repository
        var bookings = _bookingRepository.GetAll();
        var employees = _employeeRepository.GetAll();
        var rooms = _roomRepository.GetAll();

        // Mendapatkan tanggal hari ini dalam format "dd-MM-yyyy"
        var today = DateTime.Today.Date;

        // Mencari semua data booking yang dimulai pada tanggal hari ini
        var reservationRoomToday = from booking in bookings
            join employee in employees on booking.EmployeeGuid equals employee.Guid
            join room in rooms on booking.RoomGuid equals room.Guid
            // Periksa apakah tanggal StartDate sama dengan hari ini
            where booking.StartDate.Date == DateTime.Today
            select new RoomReservationDto
            {
                BookingGuid = booking.Guid,
                RoomName = room.Name,
                Status = booking.Status,
                Floor = room.Floor,
                BookedBy = employee.FirstName + " " + employee.LastName
            };

        // Periksa apakah ada data booking yang dimulai pada hari ini
        if (!reservationRoomToday.Any()) // Perbaikan pengecekan null
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));

        // Jika ada data booking yang dimulai hari ini, maka akan mengembalikan response 200 OK
        return Ok(new ResponseOKHandler<IEnumerable<RoomReservationDto>>(reservationRoomToday));
    }

    [HttpGet("available-rooms")]
    public IActionResult GetAvailableRoomsToday()
    {
        // Mendapatkan daftar semua booking dan room dari repository
        var bookings = _bookingRepository.GetAll();
        var rooms = _roomRepository.GetAll();

        // Mendapatkan tanggal hari ini dalam format "dd-MM-yyyy"
        var today = DateTime.Today.Date;

        // Menggunakan LINQ untuk mencari kamar yang tersedia hari ini
        var availableRoomToday = from booking in bookings
            join room in rooms on booking.RoomGuid equals room.Guid
            where booking.StartDate.Date == DateTime.Today
            select room;

        // Menggunakan operator Except untuk mendapatkan kamar yang tidak terbooking hari ini
        var availableRooms = rooms.Except(availableRoomToday);
        if (!availableRooms.Any()) return NotFound(new ResponseNotFoundHandler("No available rooms today"));

        // Mengubah IEnumerable<Room> menjadi IEnumerable<RoomDto>
        var data = availableRooms.Select(x => (RoomDto)x);

        // Untuk mengembalikan response 200 OK
        return Ok(new ResponseOKHandler<IEnumerable<RoomDto>>(data));
    }
}