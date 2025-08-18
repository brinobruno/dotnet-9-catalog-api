using CatalogAPI.Context;
using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly AppDbContext _context;
    public CategoriesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Category>> GetCategoriesProductsAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(p => p.Products)
            .ToListAsync();
    }
    
    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.CategoryId == id);
    }
    
    public Category GetCategoryById(int id)
    {
        return _context.Categories.FirstOrDefault(p => p.CategoryId == id);
    }
    
    public Category CreateCategory(Category category)
    {
        if (category is null) throw new ArgumentNullException(nameof(category));

        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }
    
    public Category UpdateCategory(Category category)
    {
        if (category is null) throw new ArgumentNullException(nameof(category));

        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();
        return category;
    }
    
    public Category DeleteCategory(Category category)
    {
        if (category is null) throw new ArgumentNullException(nameof(category));

        _context.Categories.Remove(category);
        _context.SaveChanges();
        return category;
    }
}