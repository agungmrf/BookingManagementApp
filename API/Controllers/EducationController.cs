using API.Contracts;
using API.DTOs.Educations;
using API.Models;
using API.Utilities.Handler;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class EducationController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IEducationRepository _educationRepository; // readonly is for dependency injection

    public EducationController(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet]
    public IActionResult GetAll()
    {
        // Mengambil semua data dari database.
        var result = _educationRepository.GetAll();
        if (!result.Any())
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Mengubah IEnumerable<Education> menjadi IEnumerable<EducationDto>.
        var data = result.Select(x => (EducationDto)x);
        
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<IEnumerable<EducationDto>>(data));
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        // Mengambil data dari database berdasarkan guid.
        var result = _educationRepository.GetByGuid(guid);
        if (result is null)
        {
            // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
            return NotFound(new ResponseNotFoundHandler("Data Not Found"));
        }
        // Jika ada data, maka akan mengembalikan response 200 OK.
        return Ok(new ResponseOKHandler<EducationDto>((EducationDto)result));
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateEducationDto createEducationDto)
    {
        try
        {
            // Membuat data baru di database.
            var result = _educationRepository.Create(createEducationDto);
            
            // Setelah data berhasil dibuat, maka akan mengembalikan response 201 Created.
            return Ok(new ResponseOKHandler<EducationDto>("Data has been created successfully") { Data = (EducationDto)result });
        }
        catch (ExceptionHandler ex)
        {
            // Jika terjadi error, maka akan mengembalikan response 500 Internal Server Error.
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseServerErrorHandler("Failed to create data", ex.Message));
        }
    }
    
    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(EducationDto educationDto)
    {
        try
        {
            // Mengambil data dari database berdasarkan guid.
            var entity = _educationRepository.GetByGuid(educationDto.Guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }
        
            Education toUpdate = educationDto;
            toUpdate.CreatedDate = entity.CreatedDate; // Menyalin CreatedDate dari entity yang diambil dari database.
        
            _educationRepository.Update(toUpdate);
            
            // Setelah data berhasil diubah, maka akan mengembalikan response 200 OK.
            return Ok(new ResponseOKHandler<EducationDto>("Data has been updated successfully") { Data = (EducationDto)toUpdate });
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
            var entity = _educationRepository.GetByGuid(guid);
            if (entity is null)
            {
                // Jika tidak ada data, maka akan mengembalikan response 404 Not Found.
                return NotFound(new ResponseNotFoundHandler("Data Not Found"));
            }

            _educationRepository.Delete(entity);

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