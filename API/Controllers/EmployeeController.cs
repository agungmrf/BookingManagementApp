using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController] // Untuk menunjukkan bahwa ini adalah controller API
[Route("api/[controller]")] // Untuk menunjukkan route dari controller ini
public class EmployeeController : ControllerBase // ControllerBase untuk controller tanpa view
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
    
    // Untuk menangani request GET dengan route api/[controller]
    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _employeeRepository.GetAll();
        if (!result.Any())
        {
            return NotFound("Data Not Found");
        }

        var data = result.Select(x => (EmployeeDto)x);
        return Ok(data);
    }
    
    // Untuk menangani request GET dengan route api/[controller]/guid
    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _employeeRepository.GetByGuid(guid);
        if (result is null)
        {
            return NotFound("Data Not Found");
        }

        return Ok((EmployeeDto)result);
    }
    
    // Untuk menangani request POST dengan route api/[controller]
    [HttpPost]
    public IActionResult Create(CreateEmployeeDto createEmployeeDto)
    {
        var result = _employeeRepository.Create(createEmployeeDto);
        if (result is null)
        {
            return BadRequest("Data Not Created");
        }
        return Ok((EmployeeDto)result);
    }
    
    // Untuk menangani request PUT dengan route api/[controller]
    [HttpPut]
    public IActionResult Update(EmployeeDto employeeDto)
    {
        var result  = _employeeRepository.Update(employeeDto);
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
        var employee = _employeeRepository.GetByGuid(guid);
        var result = _employeeRepository.Delete(employee);
        if (!result)
        {
            return BadRequest("Data Not Deleted");
        }

        return Ok("Data has been deleted successfully");
    }
}