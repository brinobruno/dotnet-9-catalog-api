using System.Linq.Expressions;

namespace CatalogAPI.Repositories;

public interface IRepository<T>
{
    IEnumerable<T> GetAllByQuery();
    Task<IEnumerable<T>> GetAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> predicate);
    T Get(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);
}