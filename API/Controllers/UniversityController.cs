using API.Contracts;
using API.DTOs.Universities;
using API.Models;
using API.Services;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menandakan bahwa controller ini adalah controller API.
[Route("api/[controller]")] // Untuk menandakan bahwa controller ini dapat diakses melalui route /api/[controller].
public class UniversityController : ControllerBase
{
    private readonly IUniversityRepository _universityRepository; // Untuk menyimpan instance dari IUniversityRepository.

    public UniversityController(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
    }

    /*private readonly UniversityService _universityService;

    public UniversityController(UniversityService universityService)
    {
        _universityService = universityService;
    }*/

    // Untuk menangani request GET dengan route /api/[controller].
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _universityRepository.GetAll();
        if (!result.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Mengubah IEnumerable<University> menjadi IEnumerable<UniversityDto>
        var data = result.Select(x => (UniversityDto)x);
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<UniversityDto>>(data));
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<UniversityDto>((UniversityDto)result));
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateUniversityDto createUniversityDto)
    {
        try
        {
            // Membuat data baru di database.
            var result = _universityRepository.Create(createUniversityDto);
            
            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<UniversityDto>("Data has been created successfully") { Data = (UniversityDto)result });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(UniversityDto universityDto)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _universityRepository.GetByGuid(universityDto.Guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
            
            University toUpdate = universityDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity
            
            // Mengupdate data di database.
            _universityRepository.Update(toUpdate);
            
            // Setelah data berhasil diupdate, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<UniversityDto>("Data has been updated successfully") { Data = (UniversityDto)toUpdate });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to update data", ex.Message));
        }
    }
    
    // Untuk menangani request DELETE dengan route /api/[controller].
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        try
        {
            // Mengambil data di database berdasarkan guid.
            var entity = _universityRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }

            // Menghapus data di database berdasarkan guid.
            _universityRepository.Delete(entity);

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