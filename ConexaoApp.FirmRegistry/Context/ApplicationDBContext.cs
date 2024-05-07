using ConexaoApp.FirmRegistry.Models;
using ConexaoCoreDB.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ConexaoApp.FirmRegistry.Context;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }
    public DbSet<Empresa> Empresas { get; set; }
    //public DbSet<Atividade> Atividades { get; set; }
    //public DbSet<Qsa> Qsas { get; set; }
    //public DbSet<Extra> Extras { get; set; }
    //public DbSet<Billing> Billings { get; set; }

}
