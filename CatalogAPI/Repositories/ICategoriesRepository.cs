using CatalogAPI.Domain;

namespace CatalogAPI.Repositories;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
}