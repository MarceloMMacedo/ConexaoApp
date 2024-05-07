
using ConexaoApp.FirmRegistry.Models;
using ConexaoApp.FirmRegistry.Pagination;
using System.Linq.Expressions;
using X.PagedList;

namespace ConexaoApp.FirmRegistry.Repositories.Interfaces
{
    public interface IEmpresaRepository:IRepository<Empresa>
    { 
        Task<IPagedList<Empresa>> GetEmpresasAsync(EmpresasParameters  empresasParameters);
        Task<IPagedList<Empresa>> GetEmpresaFiltroNomeAsync(EmpresasFiltroNome  empresasFiltroNome);
        Task  GetEmpresCnpjAsync(Expression<Func<Empresa, bool>> predicate);
    }
}
