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

    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
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
}