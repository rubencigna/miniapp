using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;
public class CustomerController : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        var listCustomers = await CustomerService.Read(new CustomerModel());
        return View(listCustomers);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SubmitAsync(CustomerModel payload)
    {
        try
        {
            bool isOK = await CustomerService.Create(payload);
            ViewData["ok"] = isOK;
            if (isOK)
            {
                return RedirectToAction("Index", "Customer");
            }
        }
        catch (Exception ex)
        {
            ViewData["errorMessage"] = "please check your data input and try again!" + ex.Message;
        }

        return View();

    }
    public IActionResult Edit()
    {
        return View();
    }

    public IActionResult Delete()
    {
        return View();
    }



}