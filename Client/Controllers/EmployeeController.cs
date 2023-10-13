using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Client.Models;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    public IActionResult Index()
    {
        return View();
    } 
}