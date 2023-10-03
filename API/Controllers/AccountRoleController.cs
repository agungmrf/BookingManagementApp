using API.Contracts;
using API.DTOs.AccountRoles;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class AccountRoleController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IAccountRoleRepository _accountRoleRepository;
    
    public AccountRoleController(IAccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }
    
    // Untuk menangani request GET dengan route /api/[controller].
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRoleRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        var data = result.Select(x => (AccountRoleDto)x);
        return Ok(data);
    }
    
    // Untuk menangani request GET dengan route /api/[controller]/guid.
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountRoleRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok((AccountRoleDto)result);
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
    {
        var result = _accountRoleRepository.Create(createAccountRoleDto);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok((AccountRoleDto)result);
    }

    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(AccountRoleDto accountRoleDto)
    {
        var result = _accountRoleRepository.Update(accountRoleDto);
        if (!result)
        {
            return BadRequest("Data Not Updated");
        }
        return Ok("Data has been updated successfully");
    }
    
    // Untuk menangani request DELETE dengan route /api/[controller].
    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        var result = _accountRoleRepository.Delete(accountRole);
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }
        
        return Ok("Data has been deleted successfully");
    }
}