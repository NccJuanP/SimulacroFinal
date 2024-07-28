using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pdf.Models;
using Rotativa.AspNetCore;

namespace pdf.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Imprimir(){
        return new ViewAsPdf("Index"){
            FileName = $"Hoja.pdf",
            PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            PageSize = Rotativa.AspNetCore.Options.Size.A4
        };
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
