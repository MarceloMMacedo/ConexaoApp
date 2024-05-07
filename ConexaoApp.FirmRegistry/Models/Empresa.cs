using ConexaoApp.FirmRegistry.Models.Converters;
using ConexaoApp.FirmRegistry.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ConexaoApp.FirmRegistry.Models;

public class Empresa
{
    [Key]
    public Guid EmpresaId { get; set; }

    [DataType(DataType.Date)]
    public DateTime?  DataSituacao { get; set; }

    [StringLength(250)]
    public string?  NomeFantasia { get; set; }

    [StringLength(100)]
    public string?  Tipo { get; set; }

    [StringLength(250)]
    public string?  Nome { get; set; }

    [DataType(DataType.Date)]
    public DateTime? Abertura { get; set; }

    [StringLength(250)]
    public string?  Telefone { get; set; }

    [StringLength(250)]
    public string?  Email { get; set; }

    [StringLength(150)]
    public string?  Bairro { get; set; }

    [StringLength(250)]
    public string?  Logradouro { get; set; }

    [StringLength(150)]
    public string?  Numero { get; set; }

    [StringLength(10)]
    public string?  Cep { get; set; }

    [StringLength(150)]
    public string?  Municipio { get; set; }

    [StringLength(250)]
    public string?  Porte { get; set; }

    [JsonConverter(typeof(EnumDescriptionConverter<TipoPessoa>))]
    public TipoPessoa?  TipoPessoa { get; set; }
    
    public string?  NaturezaJuridica { get; set; }

    [StringLength(4)]
    public string?  Uf { get; set; }

    [StringLength(25)]
    public string?  Cnpj { get; set; }

    [DataType(DataType.Date)]
    public DateTime? UltimaAtualizacao { get; set; }

    [StringLength(25)]
    public string?  Status { get; set; } 
    public string?  CapitalSocial { get; set; }


    
}


