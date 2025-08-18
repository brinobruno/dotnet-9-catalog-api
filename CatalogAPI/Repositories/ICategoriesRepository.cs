using CatalogAPI.Domain;

namespace CatalogAPI.Repositories;

public interface ICategoriesRepository
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<IEnumerable<Category>> GetCategoriesProductsAsync();
    Task<Category> GetCategoryByIdAsync(int id);
    Category GetCategoryById(int id);
    Category CreateCategory(Category category);
    Category UpdateCategory(Category category);
    Category DeleteCategory(Category category);
}