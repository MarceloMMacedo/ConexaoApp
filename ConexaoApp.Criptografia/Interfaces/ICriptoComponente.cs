using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConexaoApp.Criptografia.Interfaces;

public interface ICriptoComponente
{
    byte[] GerarChaveCriptografia();
    string Criptografar(string texto, byte[] chave);
    string Descriptografar(string textoCriptografado, byte[] chave);
    string GerarChaveCriptografiaToString();

}