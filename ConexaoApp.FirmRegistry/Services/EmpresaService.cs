using AutoMapper;
using ConexaoApp.FirmRegistry.Dtos;
using ConexaoApp.FirmRegistry.Models;
using ConexaoApp.FirmRegistry.Repositories.Interfaces;
using ConexaoApp.FirmRegistry.Services.Interfaces;

namespace ConexaoApp.FirmRegistry.Services;

public   class EmpresaService : IEmpresaService
{ 
    private readonly IEmpresaRepository _repository;

    private readonly IMapper _mapper;


    public EmpresaService(IEmpresaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EmpresaDto> Add(EmpresaDto dto)
    {
        if (dto == null) 
    throw new ArgumentNullException(nameof(dto));
    
    Empresa entity = _mapper.Map<Empresa>(dto);
    object value = await _repository.Create(entity); 
    EmpresaDto dtoReturn = _mapper.Map<EmpresaDto>(entity);
    return dtoReturn;
}

    public Task<IEnumerable<EmpresaDto>> Get()
    {
        throw new NotImplementedException();
    }

    public Task<EmpresaDto> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task Remove(string id)
    {
        throw new NotImplementedException();
    }

    public Task<EmpresaDto> Update(string id, EmpresaDto dto)
    {
        throw new NotImplementedException();
    }
}
