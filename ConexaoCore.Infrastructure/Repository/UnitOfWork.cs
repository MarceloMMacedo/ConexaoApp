using ConexaoCoreDB.Infrastructure.Context;
using ConexaoCoreDB.Infrastructure.Interfaces;

namespace ConexaoCoreDB.Infrastructure.Repository;

public abstract class UnitOfWork : IUnitOfWorks
{
    public AppDbContext _context;

    protected UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
