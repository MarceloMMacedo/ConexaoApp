using ConexaoApp.FirmRegistry.Dtos;

namespace ConexaoApp.FirmRegistry.Services.Interfaces;

public interface IEmpresaService 
{
    public Task<EmpresaDto> Add(EmpresaDto EmpresaDtoto);

    public Task<IEnumerable<EmpresaDto>> Get();

    public Task<EmpresaDto> GetById(string id);

    public Task Remove(string id);

    public Task<EmpresaDto> Update(string id, EmpresaDto dto);
}
