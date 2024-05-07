using Microsoft.AspNetCore.Mvc;

namespace ConexaoApp.FirmRegistry.Controllers;

public interface IEmpresaController<D>
{
    Task<ActionResult<IEnumerable<D>>> GetAll();

      Task<ActionResult<D>> Add(D dto);         

      Task<ActionResult<D>> GetById(string id);

      Task Remove(string id);

      Task<ActionResult<D>> Update(string id, D dto);

}
 
