using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mweb.Models;

namespace mweb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Blank()
    {
        return View();
    }

    public IActionResult Index()
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if(!User.Identity.IsAuthenticated){
            return RedirectToAction("Login2","Account");
        }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        return View();
    }

  
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
