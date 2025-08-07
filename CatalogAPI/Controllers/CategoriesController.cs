using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }
}