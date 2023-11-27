using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mweb.Models;
using Services;

namespace mweb.Controllers;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    [Authorize]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        // validate
        if (username == "admin" && password == "admin")
        {
            // validate to api
            bool isValid = await AuthService.ValidateLogin(username, password);
            if (isValid)
            {
                var claims = new List<Claim>(){
                    new(ClaimTypes.Name,username),
                    new(ClaimTypes.Role,"admin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var pricipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pricipal);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        ModelState.AddModelError(string.Empty, "Invalid Login.");
        return View();
    }



    [HttpGet]
    public IActionResult Login2()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login2(string username, string password)
    {
        // validate
        if (username == "admin" && password == "admin")
        {
            // validate to api
            bool isValid = await AuthService.ValidateLogin(username, password);
            if (isValid)
            {
                var claims = new List<Claim>(){
                    new(ClaimTypes.Name,username),
                    new(ClaimTypes.Role,"admin")
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var pricipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pricipal);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login2", "Account");
            }
        }
        ModelState.AddModelError(string.Empty, "Invalid Login.");
        return View();
    }


    [HttpGet]
    public IActionResult LoginWithGoogle()
    {
        return View();
    }

    [HttpGet]
    public IActionResult LoginWithFacebook()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
