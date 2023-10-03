using API.Contracts;
using API.DTOs.Educations;
using API.Models;
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
        var result = _educationRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }
        
        var data = result.Select(x => (EducationDto)x);
        
        return Ok(data);
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _educationRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok((EducationDto)result);
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateEducationDto createEducationDto)
    {
        var result = _educationRepository.Create(createEducationDto);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok((EducationDto)result);
    }
    
    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(EducationDto educationDto)
    {
        var result = _educationRepository.Update(educationDto);
        if (!result)
        {
            return BadRequest("Data Not Updated");
        }
        return Ok("Data has been updated successfully");
    }

    // Untuk menangani request DELETE dengan route /api/[controller]/guid.
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var education = _educationRepository.GetByGuid(guid);
        var result = _educationRepository.Delete(education);
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }
        return Ok("Data has been deleted successfully");
    }
}