using System.Linq.Expressions;

namespace ConexaoApp.FirmRegistry.Repositories;

public interface IRepository<T>
{
    //cuidado para não violar o principio ISP
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    T Delete(T entity);
}
