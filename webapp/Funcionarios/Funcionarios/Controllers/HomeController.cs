using System.Diagnostics;
using Funcionarios.Interface;
using Funcionarios.Model;
using Microsoft.AspNetCore.Mvc;
using Funcionarios.Models;

namespace Funcionarios.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFuncionarioService _service;
    private readonly ICargoService _cargoService;

    public HomeController(ILogger<HomeController> logger, IFuncionarioService service, ICargoService cargoService)
    {
        _logger = logger;
        _service = service;
        _cargoService = cargoService;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["cargos"] = await _cargoService.GetAll();
        if (TempData["codigoFuncionario"] != null)
        {
            var codigo = (int)(TempData["codigoFuncionario"] ?? 0);
            ViewData["funcionario"] = await _service.Get(codigo);
        }

        return View(await _service.GetAll());
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int codigo)
    {
        TempData["codigoFuncionario"] = codigo;
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> EditCargo(int codigo)
    {
        TempData["codigoCargo"] = codigo;
        return RedirectToAction("Cargos");
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Funcionario funcionario)
    {
        if (funcionario.Codigo == 0)
        {
            await _service.Save(funcionario);
        }
        else await _service.Update(funcionario);

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCargo(Cargo cargo)
    {
        if (cargo.Codigo == 0)
        {
            await _cargoService.Save(cargo);
        }
        else await _cargoService.Update(cargo);

        return RedirectToAction("Cargos");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int codigo)
    {
        await _service.Delete(codigo);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteCargo(int codigo)
    {
        await _cargoService.Delete(codigo);
        return RedirectToAction("Cargos");
    }

    public async Task<IActionResult> Cargos()
    {
        if (TempData["codigoCargo"] != null)
        {
            var codigo = (int)(TempData["codigoCargo"] ?? 0);
            ViewData["cargo"] = await _cargoService.Get(codigo);
        }
        return View(await _cargoService.GetAll());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}