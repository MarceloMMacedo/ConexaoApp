using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoCoreDB.Application.Mappings;

internal class DomainToDTOMappingProfile<E, D> : AutoMapper.Profile where E : class where D : class
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<E, D>().ReverseMap();
    }
}
