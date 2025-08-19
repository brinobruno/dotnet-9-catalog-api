using CatalogAPI.Domain;

namespace CatalogAPI.Repositories;

public interface ICategoriesRepository : IRepository<Category>
{
    Task<IEnumerable<Category>> GetCategoriesProductsAsync();
}