using ConexaoApp.Criptografia.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace ConexaoApp.Criptografia.Services;

public class DescriptografiaMiddleware : IDescriptografiaMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;


    public DescriptografiaMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)

    {
        

        using (var scope = _serviceProvider.CreateScope())
        {
            var _cripto = scope.ServiceProvider.GetRequiredService<ICriptoComponente>();

            string hash = null;
            //pegar o token Bearer a partir do header Authorition

            if (!string.IsNullOrEmpty(context.Request.Headers["Authorization"]) && context.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
            {
                string token = context.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
                // extrair claims hash

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken(token);

                hash = jsonToken.Claims.First(claim => claim.Type == "hash").Value;

            }

            if (hash != null)
            {
                string corpoCriptografado = await new StreamReader(context.Request.Body).ReadToEndAsync();
                string corpoDescriptografado = _cripto.Descriptografar(corpoCriptografado, Convert.FromBase64String(hash));

                // Substitua o corpo da solicitação pelo corpo descriptografado
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(corpoDescriptografado));
            }
            // Chame o próximo middleware na cadeia
            await _next(context);
            if (hash != null)
            {
                // Criptografe o corpo da resposta
                string corpoResposta = await new StreamReader(context.Response.Body).ReadToEndAsync();
                string corpoRespostaCriptografado = _cripto.Criptografar(corpoResposta, Convert.FromBase64String(hash));

                // Substitua o corpo da resposta pelo corpo criptografado
                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(corpoRespostaCriptografado));
            }
        }
    }
}
