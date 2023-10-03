using API.Contracts;
using API.DTOs.Roles;
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

        var data = result.Select(x => (RoleDto)x);
        
        return Ok(data);
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

        return Ok((RoleDto)result);
    }
    
    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(CreateRoleDto createRoleDto)
    {
        var result = _roleRepository.Create(createRoleDto);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok((RoleDto) result);
    }
    
    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(RoleDto roleDto)
    {
        var entity = _roleRepository.GetByGuid(roleDto.Guid);
        if (entity is null)
        {
            return NotFound("Id Not Found");
        }
        
        Role toUpdate = roleDto;
        toUpdate.CreatedDate = entity.CreatedDate;
        
        var result = _roleRepository.Update(toUpdate);
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