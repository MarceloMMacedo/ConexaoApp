using ConexaoApp.Criptografia.Interfaces;
using ConexaoApp.Criptografia.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.PortableExecutable;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
        context.Request.EnableBuffering();
        ICriptoComponente _cripto;
        var body = string.Empty;
        JObject jsonObject = null;
        var hash = await GetHashFromAuthorizationHeader(context);

        var criptoAdEncodedAttribute = (CriptoAdEncodedAttribute)context.GetEndpoint().Metadata
            .FirstOrDefault(m => m.GetType() == typeof(CriptoAdEncodedAttribute));

        if (criptoAdEncodedAttribute == null || hash == null)
        {
            await _next(context);
            return;
        }

        using (var scope = _serviceProvider.CreateScope())
        {
            _cripto = scope.ServiceProvider.GetRequiredService<ICriptoComponente>();
        }

        if (criptoAdEncodedAttribute.EncodedRequest)
        {
            body = await ProcessRequestBody(context, _cripto, hash);
        }

        if (criptoAdEncodedAttribute.EncodedResponse)
        {
            await ProcessResponseBody(context, _cripto, hash);
        }
        else
        {
            await _next(context);
        }
    }

    private async Task<string> ProcessRequestBody(HttpContext context, ICriptoComponente _cripto, string hash)
    {
        var body = string.Empty;
        JObject jsonObject = null;

        if (context.Request.Body.CanRead)
        {
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                byte[] responseBodyBytes = Convert.FromBase64String(body);
                string manipulatedBody = Encoding.UTF8.GetString(responseBodyBytes);

                body = _cripto.Descriptografar(manipulatedBody, Convert.FromBase64String(hash));

                jsonObject = JObject.Parse(body);
                body = jsonObject.ToString();
            }

            if (!string.IsNullOrWhiteSpace(body))
            {
                context.Request.ContentType = "application/json";
                context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
            }
        }

        return body;
    }

    private async Task ProcessResponseBody(HttpContext context, ICriptoComponente _cripto, string hash)
    {
        var originalBodyStream = context.Response.Body;

        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body = originalBodyStream;

            var bodyText = await ReadResponseBody(responseBody);

            JObject jsonObject = JObject.Parse(bodyText);

            string jsonObjectString = jsonObject.ToString();

            string manipulatedBody = _cripto.Criptografar(jsonObjectString, Convert.FromBase64String(hash));

            byte[] responseBodyBytes = Encoding.UTF8.GetBytes(manipulatedBody);

            string base64String = Convert.ToBase64String(responseBodyBytes);

            context.Response.ContentLength = null;

            await context.Response.WriteAsync(base64String);
        }
    }

    private async Task<string> GetHashFromAuthorizationHeader(HttpContext context)
    {
        if (!string.IsNullOrEmpty(context.Request.Headers["Authorization"]) && context.Request.Headers["Authorization"].ToString().StartsWith("Bearer "))
        {
            string token = context.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length);
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            return jsonToken.Claims.First(claim => claim.Type == "Hash").Value;
        }
        return null;
    }

    private async Task<string> ReadResponseBody(MemoryStream responseBody)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        var bodyText = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);

        return bodyText;
    }
}
