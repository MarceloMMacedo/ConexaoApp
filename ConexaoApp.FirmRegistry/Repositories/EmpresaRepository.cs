using ConexaoApp.FirmRegistry.Context;
using ConexaoApp.FirmRegistry.Models;
using ConexaoApp.FirmRegistry.Pagination;
using ConexaoApp.FirmRegistry.Repositories.Interfaces;
using System.Linq.Expressions;
using X.PagedList;

namespace ConexaoApp.FirmRegistry.Repositories;
 
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(ApplicationDBContext context ): base(context) { }

        public Task<IPagedList<Empresa>> GetEmpresaFiltroNomeAsync(EmpresasFiltroNome empresasFiltroNome)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedList<Empresa>> GetEmpresasAsync(EmpresasParameters empresasParameters)
        {
            throw new NotImplementedException();
        }

        public Task GetEmpresCnpjAsync(Expression<Func<Empresa, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
