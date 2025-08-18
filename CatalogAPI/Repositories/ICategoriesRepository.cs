using CatalogAPI.Domain;

namespace CatalogAPI.Repositories;

public interface ICategoriesRepository
{
    IEnumerable<Category> GetCategories();
}