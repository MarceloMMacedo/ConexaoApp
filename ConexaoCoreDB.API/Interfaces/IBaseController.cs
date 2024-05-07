using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCoreDB.API.Interfaces;

public interface IBaseController<D>
{   
    Task<IEnumerable<D>> Get();
    Task<D> GetById(string id);
    Task Add(D dto);
    Task Update(string id, D dto);
    Task Remove(string id);

}
