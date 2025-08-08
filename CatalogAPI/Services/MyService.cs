namespace CatalogAPI.Services;

public class MyService  : IMyService
{
    public string Greeting(string name)
    {
        return $"Hello {name}! {DateTime.UtcNow}";
    }
}