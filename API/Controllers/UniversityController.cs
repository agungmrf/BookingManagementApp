using API.Contracts;
using API.Models;
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

    // Untuk menangani request GET dengan route /api/[controller].
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _universityRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _universityRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(University university)
    {
        var result = _universityRepository.Create(university);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok(result);
    }
    
    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(University university)
    {
        var result = _universityRepository.Update(university);
        if (!result)
        {
            return BadRequest("Data Not Updated");
        }
        return Ok("Data Updated");
    }
    
    // Untuk menangani request DELETE dengan route /api/[controller].
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var university = _universityRepository.GetByGuid(guid);
        var result = _universityRepository.Delete(university);
        
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }
        return Ok("Data Deleted");
    }
}