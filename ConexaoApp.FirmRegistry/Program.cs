using ConexaoApp.Criptografia.Interfaces;
using ConexaoApp.Criptografia.Services;
using ConexaoApp.Execption.Filters;
using ConexaoApp.FirmRegistry.Context;
using ConexaoApp.FirmRegistry.Controllers;
using ConexaoApp.FirmRegistry.Dtos;
using ConexaoApp.FirmRegistry.Dtos.Mappings;
using ConexaoApp.FirmRegistry.Models;
using ConexaoApp.FirmRegistry.Repositories;
using ConexaoApp.FirmRegistry.Repositories.Interfaces;
using ConexaoApp.FirmRegistry.Services;
using ConexaoApp.FirmRegistry.Services.Interfaces;
using ConexaoCoreDB.CrossCutting.Ioc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
})
.AddNewtonsoftJson();

builder.Services.AddControllers();
//builder.Services.AddInfrastructureAPI(builder.Configuration);

string mySqlConnection = builder.Configuration
                                .GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddControllers(options =>
//{
//    options.Filters.Insert(0, new ServiceFilterAttribute(typeof(CriptoAdEncodedFilter)));
//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


 //builder.Services.AddScoped<IEmpresaController<EmpresaDto>, EmpresasController>();
 builder.Services.AddScoped<CriptoAdEncodedFilter>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();  
builder.Services.AddScoped<IEmpresaService, EmpresaService>();  
builder.Services.AddScoped<ICriptoComponente, CriptoComponente>();
 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile));



//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add(typeof(CriptoAdEncodedFilter));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
app.UseMiddleware<DescriptografiaMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
