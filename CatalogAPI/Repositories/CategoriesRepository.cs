using CatalogAPI.Context;
using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class CategoriesRepository : Repository<Category>, ICategoriesRepository
{
    private readonly AppDbContext _context;
    public CategoriesRepository(AppDbContext context) : base(context) // Inherits from DI in Repository
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Category>> GetCategoriesProductsAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(p => p.Products)
            .ToListAsync();
    }
}