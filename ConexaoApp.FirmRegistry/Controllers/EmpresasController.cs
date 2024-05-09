using ConexaoApp.Criptografia.Interfaces;
using ConexaoApp.Criptografia.Models;
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
    private readonly ICriptoComponente  _criptoComponente;

    public EmpresasController(ILogger<EmpresasController> logger, 
        IEmpresaService empresasService, 
        ICriptoComponente criptoComponente )
    {
        _logger = logger;
        _empresasService = empresasService;
        _criptoComponente = criptoComponente;
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
    [HttpGet]
    public string GetKey()
    {return _criptoComponente.GerarChaveCriptografiaToString() ;
    }
    [CriptoAdEncoded(EncodedRequest = true, EncodedResponse = false)]
    [HttpPost("criptografar")]
    public EmpresaDto Criptografar(EmpresaDto dto)
    {
        byte[] bytes = _criptoComponente.GerarChaveCriptografia();
       string criptografia = _criptoComponente.Criptografar(dto.Tipo, bytes);
        Console.WriteLine(criptografia);
        string decriptografia = _criptoComponente.Descriptografar(criptografia, bytes);
        Console.WriteLine(decriptografia);
        //gerar uuid 
        dto.EmpresaId = Guid.NewGuid().ToString();
        return dto;
    }
}
