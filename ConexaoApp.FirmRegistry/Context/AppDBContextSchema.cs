using ConexaoApp.FirmRegistry.Models;
using ConexaoCoreDB.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace ConexaoApp.FirmRegistry.Context;

public class AppDBContextSchema : DbContext
{
    public string Schema { get; set; }

    public AppDBContextSchema(string schema)
    {
        Schema = schema;
    }
    public DbSet<Empresa> Empresas { get; set; }
    //public DbSet<Atividade> Atividades { get; set; }
    //public DbSet<Qsa> Qsas { get; set; }
    //public DbSet<Extra> Extras { get; set; }
    //public DbSet<Billing> Billings { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql($"Server=localhost;Database={Schema};Uid=root;Pwd=Hw8vup5e;",
                new MySqlServerVersion(new Version(8, 0, 0)),
                options => options.SchemaBehavior(MySqlSchemaBehavior.Ignore)); // Substitua pela versão correta do servidor MySQL
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        base.OnModelCreating(modelBuilder);
    }

}
