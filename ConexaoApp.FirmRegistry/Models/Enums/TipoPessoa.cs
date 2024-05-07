using System.ComponentModel;

namespace ConexaoApp.FirmRegistry.Models.Enums;

public enum TipoPessoa
{
    [Description("Pessoa Física")]
    Fisica = 1,

    [Description("Pessoa Juridica")]
    Juridica = 2,

    [Description("Outro Tipo")]
    OutroTipo = 3
}
