using ConexaoApp.FirmRegistry.Context;
using ConexaoApp.FirmRegistry.Dtos;
using ConexaoApp.FirmRegistry.Models;
using ConexaoApp.FirmRegistry.Services.Interfaces;
using ConexaoCoreDB.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoApp.FirmRegistry.Controllers;

[ApiController]
[Route("[controller]")]
public class EmpresasController : ControllerBase 
{
    private readonly ILogger<EmpresasController> _logger;
    private readonly IEmpresaService  _empresasService;
 
    public EmpresasController(ILogger<EmpresasController> logger, IEmpresaService  empresasService)
    {
        _logger = logger;
        _empresasService = empresasService;
    }

    [HttpPost]
    public async Task<ActionResult<EmpresaDto>>  Add(EmpresaDto dto)
    {
        return await _empresasService.Add(dto);
    }
    [HttpGet("criarContexto")]
    public void criarContexto(string nomeBanco)
    {
        var context = new AppDBContextSchema(nomeBanco);
        context.Database.EnsureCreated();
    }
     
}
