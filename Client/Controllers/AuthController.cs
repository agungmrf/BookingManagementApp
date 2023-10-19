using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Client.Controllers;

public class AuthController : Controller
{
    private readonly IAccountRepository _accountRepository;

    public AuthController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var result = await _accountRepository.Login(login);

        if (result.Status == "OK")
        {

            HttpContext.Session.SetString("JWToken", result.Data.Token);
            return RedirectToAction("Index", "Employee");
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("Logout/")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}