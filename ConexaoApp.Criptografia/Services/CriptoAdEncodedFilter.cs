
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ConexaoApp.Criptografia.Interfaces;
using ConexaoApp.Criptografia.Models;
using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ConexaoApp.Criptografia.Services;

public class CriptoAdEncodedFilter : IAsyncActionFilter
{
    private readonly ICriptoComponente _cripto;



    public CriptoAdEncodedFilter(ICriptoComponente cripto)
    {
        _cripto = cripto;
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var criptoAdEncodedAttribute = (CriptoAdEncodedAttribute)context.ActionDescriptor.EndpointMetadata
            .FirstOrDefault(m => m.GetType() == typeof(CriptoAdEncodedAttribute));

        var request = context.HttpContext.Request;
        var hash = string.Empty;

        //token
        if (!string.IsNullOrEmpty(request.Headers["Authorization"]) && request.Headers["Authorization"].ToString().StartsWith("Bearer "))
        {
            string token = request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
            // extrair claims hash

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);

            hash = jsonToken.Claims.First(claim => claim.Type == "Hash").Value;
        }

        if (criptoAdEncodedAttribute != null && criptoAdEncodedAttribute.EncodedRequest && hash != null)
        {
            // Ler o corpo da solicitação
            //  string corpoCriptograf//ado = await new StreamReader(context.HttpContext.Request.Body).ReadToEndAsync();
            using var reader = new StreamReader(request.Body);
            var corpoCriptografado = await reader.ReadToEndAsync();



            // Descriptografar o corpo da solicitação
            var corpoDescriptografado = _cripto.Descriptografar(corpoCriptografado, Convert.FromBase64String(hash));

            // Substituir o corpo da solicitação pelo corpo descriptografado
            request.Body = new MemoryStream(Encoding.UTF8.GetBytes(corpoDescriptografado));
        }

        // Chamar o próximo filtro/método de ação na cadeia
        var resultContext = await next();

        if (criptoAdEncodedAttribute != null && criptoAdEncodedAttribute.EncodedResponse && hash != null && resultContext.Result is ObjectResult)
        {
            var response = context.HttpContext.Response;
            var json = JsonConvert.SerializeObject(resultContext.Result);
            var originalBodyStream = response.Body;

            // Criptografar o corpo da resposta
            var corpoRespostaCriptografado = _cripto.Criptografar(json, Convert.FromBase64String(hash));
            //corpoRespostaCriptografado = Convert.ToBase64String(Encoding.UTF8.GetBytes(corpoRespostaCriptografado));

            // Substituir o corpo da resposta pelo corpo criptografado
            var encryptedStream = new MemoryStream(Encoding.UTF8.GetBytes(corpoRespostaCriptografado));
            await encryptedStream.CopyToAsync(originalBodyStream);
        }
    }

}
