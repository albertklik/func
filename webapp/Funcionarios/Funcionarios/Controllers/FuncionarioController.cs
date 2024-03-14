using Funcionarios.Interface;
using Funcionarios.Model;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Funcionarios.Controllers;

public class FuncionarioController: Controller
{
    private readonly IFuncionarioService _service;

    public FuncionarioController(IFuncionarioService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Funcionario funcionario)
    {
        await _service.Save(funcionario);
        return Redirect(Request.Headers["Referer"].ToString());

    }
    
}