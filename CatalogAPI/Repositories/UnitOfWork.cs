using CatalogAPI.Context;

namespace CatalogAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private IProductsRepository _productRepo;
    
    private ICategoriesRepository _categoryRepo;

    public AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IProductsRepository ProductRepository
    {
        get
        {
            return _productRepo = _productRepo ?? new ProductsRepository(_context);
        }
    }
    
    public ICategoriesRepository CategoryRepository
    {
        get
        {
            return _categoryRepo = _categoryRepo ?? new CategoriesRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
    
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
    
    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}