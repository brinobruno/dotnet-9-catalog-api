using System.Linq.Expressions;
using CatalogAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class Repository<T>: IRepository<T> where T: class
{
    protected readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<T> GetAllByQuery()
    {
        return _context.Set<T>().ToList();
    }
    
    public async Task<IEnumerable<T>> GetAsync()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate);
    }
    
    public T Get(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>()
            .FirstOrDefault(predicate);
    }

    public T Create(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
        return entity;
    }
    
    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChanges();
        return entity;
    }
    
    public T Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
        return entity;
    }
}