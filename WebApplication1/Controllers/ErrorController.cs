using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class ErrorController:Controller
{
    public ErrorController()
    {
        
    }
    public  IActionResult Error()
    {
        return View("Error");
    }
}