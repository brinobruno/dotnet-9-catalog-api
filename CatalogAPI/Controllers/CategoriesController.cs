using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "Hello World!";
    }
}