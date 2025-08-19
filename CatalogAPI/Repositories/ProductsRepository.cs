using CatalogAPI.Context;
using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class ProductsRepository : Repository<Product>, IProductsRepository
{
    private readonly AppDbContext _context;
    public ProductsRepository(AppDbContext context) : base(context) // Inherits from DI in Repository
    {
        _context = context;
    }

    public IEnumerable<Product> GetProductsByCategoryAsync(int id)
    {
        return GetAllByQuery().Where(c => c.CategoryId == id);
    }
}