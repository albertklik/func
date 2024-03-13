using System.Diagnostics;
using Funcionarios.Interface;
using Microsoft.AspNetCore.Mvc;
using Funcionarios.Models;

namespace Funcionarios.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFuncionarioService _service;

    public HomeController(ILogger<HomeController> logger, IFuncionarioService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        //ViewData["funcionarios"] = ;
        return View(await _service.GetAll());
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