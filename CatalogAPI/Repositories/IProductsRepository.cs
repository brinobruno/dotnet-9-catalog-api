using CatalogAPI.Domain;

namespace CatalogAPI.Repositories;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Product GetProductById(int id);
    Product CreateProduct(Product product);
    Product UpdateProductById(Product product);
    Product DeleteProductById(Product product);
}