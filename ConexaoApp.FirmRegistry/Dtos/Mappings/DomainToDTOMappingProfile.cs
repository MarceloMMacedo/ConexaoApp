using AutoMapper;
using ConexaoApp.FirmRegistry.Models;
using ConexaoApp.FirmRegistry.Models.Enums;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Reflection;

namespace ConexaoApp.FirmRegistry.Dtos.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        //CreateMap<Empresa, EmpresaDto>().ReverseMap();
        //CreateMap<Empresa, EmpresaListDto>().ReverseMap();

        {
            CreateMap<Empresa, EmpresaListDto>()
                 .ForMember(dest => dest.TipoPessoa, opt => opt.MapFrom(src => GetEnumDescription(src.TipoPessoa)));
            CreateMap<Empresa, EmpresaDto>()
                 .ForMember(dest => dest.TipoPessoa, opt => opt.MapFrom(src => GetEnumDescription(src.TipoPessoa)));

            CreateMap<EmpresaDto, Empresa>()
              .ForMember(dest => dest.TipoPessoa, opt => opt.MapFrom(src => GetEnumFromDescription<TipoPessoa>(src.TipoPessoa)));
        }
    }
    private string GetEnumDescription<TEnum>(TEnum? value) where TEnum : struct
        {
            if (value.HasValue)
            {
                var fieldInfo = value.Value.GetType().GetField(value.Value.ToString());
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                return value.Value.ToString();
            }
            return null;
        }
    private TEnum? GetEnumFromDescription<TEnum>(string description) where TEnum : struct
        {
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
                if (attributes != null && attributes.Length > 0 && attributes[0].Description == description)
                {
                    return value;
                }
            }
            return null;
        }
    }
