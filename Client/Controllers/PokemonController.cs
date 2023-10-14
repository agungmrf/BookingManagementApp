using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class PokemonController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}