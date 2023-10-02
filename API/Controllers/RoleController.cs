using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class RoleController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IRoleRepository _roleRepository;

    public RoleController(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }
    
    // Untuk menangani request GET dengan route api/[controller]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roleRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request GET dengan route api/[controller]/guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(Role role)
    {
        var result = _roleRepository.Create(role);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok(result);
    }
    
    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(Role role)
    {
        var result  = _roleRepository.Update(role);
        if (!result)
        {
            return BadRequest("Data Not Updated");
        }

        return Ok("Data has been updated successfully");
    }

    // Untuk menangani request DELETE dengan route api/[controller]/guid
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var role = _roleRepository.GetByGuid(guid);
        var result = _roleRepository.Delete(role);
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }

        return Ok("Data has been deleted successfully");
    }
}