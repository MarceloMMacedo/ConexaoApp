using ConexaoApp.FirmRegistry.Models.Converters;
using ConexaoApp.FirmRegistry.Models.Enums;
using System.Text.Json.Serialization;

namespace ConexaoApp.FirmRegistry.Dtos;

public class EmpresaListDto
{
    public string? EmpresaId { get; set; }    
    public string? NomeFantasia { get; set; }
    public string? Tipo { get; set; }
    public string? Nome { get; set; } 
    public string? Telefone { get; set; }
    [JsonConverter(typeof(EnumDescriptionConverter<TipoPessoa>))]
    public TipoPessoa? TipoPessoa { get; set; }
}
