using CatalogAPI.Context;
using CatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext _context;
    public ProductsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductId == id);
    }
    
    public Product GetProductById(int id)
    {
        return _context.Products
            .FirstOrDefault(p => p.ProductId == id);
    }
    
    public Product CreateProduct(Product product)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));

        _context.Products.Add(product);
        _context.SaveChanges();
        return product;
    }
    
    public Product UpdateProductById(Product product)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));

        _context.Entry(product).State = EntityState.Modified;
        _context.SaveChanges();
        return product;
    }
    
    public Product DeleteProductById(Product product)
    {
        if (product is null) throw new ArgumentNullException(nameof(product));

        _context.Products.Remove(product);
        _context.SaveChanges();

        return product;
    }
}