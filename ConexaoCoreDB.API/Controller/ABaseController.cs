

using ConexaoCoreDB.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConexaoCoreDB.API.Controller
{
    [ApiController]
    public class ABaseController<D> : IBaseController<D>
    {
        public Task Add(D dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<D>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<D> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task Update(string id, D dto)
        {
            throw new NotImplementedException();
        }
    }


}
