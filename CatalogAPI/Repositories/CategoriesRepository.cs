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

    public IEnumerable<Category> GetCategories()
    {
        return _context.Categories.AsNoTracking();
    }
}