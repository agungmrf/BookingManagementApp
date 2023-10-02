using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase // Controller is for MVC
{
    private readonly IAccountRepository _accountRepository; // readonly is for dependency injection

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    // Untuk menangani request GET dengan route /api/[controller].
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountRepository.GetAll();
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
        var result = _accountRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok(result);
    }
    
    // Untuk menangani request POST dengan route /api/[controller].
    [HttpPost]
    public IActionResult Create(Account account)
    {
        var result = _accountRepository.Create(account);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok(result);
    }

    // Untuk menangani request PUT dengan route /api/[controller].
    [HttpPut]
    public IActionResult Update(Account account)
    {
        var result = _accountRepository.Update(account);
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
        var account = _accountRepository.GetByGuid(guid);
        var result = _accountRepository.Delete(account);
        
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }
        
        return Ok("Data has been deleted successfully");
    }
}