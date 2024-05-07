 
using ConexaoApp.FirmRegistry.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ConexaoApp.FirmRegistry.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDBContext _context;

    public Repository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public Task<T> Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return Task.FromResult(entity);
    }
    public Task<T> Update(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        _context.Set<T>().Update(entity);
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return Task.FromResult(entity);
    }
    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        //_context.SaveChanges();
        return entity;
    }
}
