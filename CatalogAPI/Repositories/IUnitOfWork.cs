namespace CatalogAPI.Repositories;

public interface IUnitOfWork
{
    IProductsRepository ProductRepository { get; }
    ICategoriesRepository CategoryRepository { get; }
    void Commit();
    Task CommitAsync();
    void Dispose();
    ValueTask DisposeAsync();
}