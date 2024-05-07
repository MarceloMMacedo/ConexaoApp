using ConexaoApp.FirmRegistry.Models.Converters;
using ConexaoApp.FirmRegistry.Models.Enums;
using System.Text.Json.Serialization;

namespace ConexaoApp.FirmRegistry.Dtos;

public class EmpresaDto
{
    public string? EmpresaId { get; set; }
    public DateTime? DataSituacao { get; set; }
    public string? NomeFantasia { get; set; }
    public string? Tipo { get; set; }
    public string? Nome { get; set; }
    public DateTime? Abertura { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Bairro { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Cep { get; set; }
    public string? Municipio { get; set; }
    public string? Porte { get; set; }
    public string? NaturezaJuridica { get; set; }
    public string? Uf { get; set; }
    public string? Cnpj { get; set; }
    public DateTime? UltimaAtualizacao { get; set; }
    public string? Status { get; set; }
    public string? CapitalSocial { get; set; }
    [JsonConverter(typeof(EnumDescriptionConverter<TipoPessoa>))]
    public string? TipoPessoa { get; set; }
}
