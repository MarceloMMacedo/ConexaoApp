 

namespace ConexaoApp.Criptografia.Models;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter)]
public class CriptoAdEncodedAttribute : Attribute
{
    public bool EncodedRequest { get; set; } = false;
    public bool EncodedResponse { get; set; } = false;
}
