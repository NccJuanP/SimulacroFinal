using System.Data;
using System.Diagnostics;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using pdf.Context;
using pdf.Models;
using Rotativa.AspNetCore;

namespace pdf.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BaseContext _context;

    public HomeController(ILogger<HomeController> logger, BaseContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult ExportarExcel()
    {
        var estadisticas = _context.Stadistics.ToList();
        return GenerarExcel("Estadisticas", estadisticas);
    }
    private FileResult GenerarExcel(string NombreArchivo, IEnumerable<Stadistics> Estadisticas){
        DataTable table = new DataTable();
        table.Columns.AddRange(new DataColumn[] {
            new DataColumn("Id"),
            new DataColumn("EventId"),
            new DataColumn("ParticipantsCount"),
            new DataColumn("AttendanceCount")
        });

        foreach (var estadistica in Estadisticas){
            table.Rows.Add(estadistica.Id, estadistica.EventId, estadistica.ParticipantsCount, estadistica.AttendanceCount);
        }

        using (XLWorkbook workbook = new XLWorkbook())
        {
            workbook.Worksheets.Add(table, "Estadisticas");
            workbook.Worksheets.Add(table, "Estadisticas2");
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{NombreArchivo}.xlsx");
            }
        }
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
