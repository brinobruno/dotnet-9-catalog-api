using CatalogAPI.Domain;

namespace CatalogAPI.Repositories;

public interface IProductsRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductsByCategoryAsync(int id);
}