using API.Contracts;
using API.DTOs.Bookings;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class BookingController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRoomRepository _roomRepository;

    public BookingController(IBookingRepository bookingRepository, IEmployeeRepository employeeRepository, IRoomRepository roomRepository)
    {
        _bookingRepository = bookingRepository;
        _employeeRepository = employeeRepository;
        _roomRepository = roomRepository;
    }

    // Untuk menangani request GET dengan route /api/[controller].
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _bookingRepository.GetAll();
        if (!result.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Mengubah IEnumerable<Booking> menjadi IEnumerable<BookingDto>.
        var data = result.Select(x => (BookingDto)x);
        
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<BookingDto>>(data));
    }

    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _bookingRepository.GetByGuid(guid);
        if (result is null)
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<BookingDto>((BookingDto)result));
    }

    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateBookingDto createBookingDto)
    {
        try
        {
            // Membuat data baru di database.
            var result = _bookingRepository.Create(createBookingDto);
            
            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<BookingDto>("Data has been created successfully") { Data = (BookingDto)result });
        }
        catch (ExceptionHandler ex) // ExceptionHandler untuk menangani exception ketika terjadi error
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }

    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(BookingDto bookingDto)
    {
        try
        {
            // Mengambil data dari database berdasarkan guid.
            var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
        
            Booking toUpdate = bookingDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity yang diambil dari database.
        
            _bookingRepository.Update(toUpdate);

            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<BookingDto>("Data has been updated successfully") { Data = (BookingDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        { 
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }

    // Untuk menangani request DELETE dengan route /api/[controller]/guid.
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Menghapus data dari database berdasarkan guid.
            var entity = _bookingRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }

            _bookingRepository.Delete(entity);

            // Setelah data berhasil dihapus, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<string>("Data has been deleted successfully"));
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to delete data", ex.Message));
        }
    }

    [HttpGet("booking-details")]
    public IActionResult GetDetails()
    {
        // Mengambil semua data booking, employee, dan room dari repository
        var bookings = _bookingRepository.GetAll();
        var employees = _employeeRepository.GetAll();
        var rooms = _roomRepository.GetAll();

        // Menggunakan LINQ untuk menggabungkan data dari ketiga repository ke dalam bookingDetails
        var bookingDetails = from booking in bookings
            join employee in employees on booking.EmployeeGuid equals employee.Guid
            join room in rooms on booking.RoomGuid equals room.Guid
            select new BookingDetailDto
            {
                Guid = booking.Guid,
                BookedNIK = employee.Nik,
                BookedBy = string.Concat(employee.FirstName, " ", employee.LastName),
                RoomName = room.Name,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status.ToString(), // Mengambil status booking dan mengonversinya menjadi string
                Remarks = booking.Remarks
            };
            
            if (!bookingDetails.Any())
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            // Jika ada data bookingDetails, maka akan mengembalikan response 200 OK
            return Ok(new ResponseOKHandler<IEnumerable<BookingDetailDto>>(bookingDetails));
    }

    [HttpGet("booking-details/{guid}")]
    public IActionResult GetDetailByGuid(Guid guid)
    {
        // Mengambil data booking berdasarkan Guid dari repository
        var booking = _bookingRepository.GetByGuid(guid);
        if (booking == null)
        {
            // Jika booking dengan Guid yang diinputkan tidak ditemukan, mengembalikan response 404 Not Found
            return NotFound(new ResponseNotFoundHandler("Id Booking Detail Not Found"));
        }
        
        // Mengambil data employee dan room berdasarkan Guid dari repository
        var employee = _employeeRepository.GetByGuid(booking.EmployeeGuid);
        var room = _roomRepository.GetByGuid(booking.RoomGuid);
        
        // Membuat objek BookingDetailDto berdasarkan data yang diambil
        var bookingDetail = new BookingDetailDto
        {
            Guid = booking.Guid,
            BookedNIK = employee.Nik,
            BookedBy = string.Concat(employee.FirstName, " ", employee.LastName),
            RoomName = room.Name,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status.ToString(),
            Remarks = booking.Remarks
        };
        
        // Mengembalikan response 200 Ok dengan data bookingDetail
        return Ok(new ResponseOKHandler<BookingDetailDto>(bookingDetail));
    }

    [HttpGet("booking-length")]
    public IActionResult GetBookingLength()
    {
        // Mengambil semua data booking dan room dari repository
        var bookings = _bookingRepository.GetAll();
        var rooms = _roomRepository.GetAll();
        
        // Menggunakan LINQ untuk menggabungkan data dari booking dan room serta menghitung panjang peminjaman
        var bookingLengths = from booking in bookings
            join room in rooms on booking.RoomGuid equals room.Guid
            let bookingDays = Enumerable.Range(0, (int)(booking.EndDate - booking.StartDate).TotalDays + 1)
                .Select(offset => booking.StartDate.AddDays(offset))
            where bookingDays.All(date => date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            select new BookingLengthDto
            {
                RoomGuid = room.Guid,
                RoomName = room.Name,
                BookingLength = bookingDays.Count()
            };
        
        if (!bookingLengths.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        
        // Jika ada data bookingLengths, maka akan mengembalikan response 200 OK
        return Ok(new ResponseOKHandler<IEnumerable<BookingLengthDto>>(bookingLengths));
    }
}