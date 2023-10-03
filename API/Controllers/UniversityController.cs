using API.Contracts;
using API.DTOs.Universities;
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

        var data = result.Select(x => (UniversityDto)x);

        return Ok(data);
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

        return Ok((UniversityDto)result);
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateUniversityDto createUniversityDto)
    {
        var result = _universityRepository.Create(createUniversityDto);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok((UniversityDto) result);
    }
    
    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(UniversityDto universityDto)
    {
        var entity = _universityRepository.GetByGuid(universityDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        
        University toUpdate = universityDto;
        toUpdate.CreatedDate = entity.CreatedDate; 
        
        var result = _universityRepository.Update(toUpdate); 
        if(!result)
        {
            return BadRequest("Data Not Updated"); 
        }

        return Ok("Data has been updated successfully");
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
        return Ok("Data has been deleted successfully");
    }
}