using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}