using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

[Authorize]
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository repository;

    public EmployeeController(IEmployeeRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await repository.Get();
        var ListEmployee = new List<EmployeeDto>();

        if (result.Data != null)
        {
            ListEmployee = result.Data.ToList();
        }
        return View(ListEmployee);
    }

    [HttpGet]
    public IActionResult CreateEmp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmp(EmployeeDto createEmployee)
    {

        var result = await repository.Post(createEmployee);
        if (result.Status == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Status == "409")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }
        return View();

    }

    [HttpGet]
    public async Task<IActionResult> UpdateEmp(Guid id)
    {
        var result = await repository.Get(id);
        var ListEmployee = new EmployeeDto();

        if (result != null)
        {
            ListEmployee = result.Data;
        }
        return View(ListEmployee);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateEmp(EmployeeDto updateEmployee)
    {
        var result = await repository.Put(updateEmployee.Guid, updateEmployee);

        if (result != null)
        {
            return RedirectToAction(nameof(Index));
        }
        return View(updateEmployee);
    }

    [HttpGet]
    public async Task<IActionResult> DeleteEmp(Guid guid)
    {
        var result = await repository.Get(guid);
        var emp = new EmployeeDto();
        if (result != null)
        {
            emp = result.Data;
        }
        return View(emp);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEmp(EmployeeDto employeeDto)
    {
        var result = await repository.Delete(employeeDto.Guid);

        if (result != null)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(employeeDto);
    }

    [HttpGet]
    public async Task<IActionResult> DetailsEmp(Guid guid)
    {
        var result = await repository.Get(guid);
        if (result != null)
        {
            return RedirectToAction(nameof(Index));
        }
        return NotFound();
    }
}