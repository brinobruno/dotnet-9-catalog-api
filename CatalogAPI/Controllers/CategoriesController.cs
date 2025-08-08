using CatalogAPI.Context;
using CatalogAPI.Domain;
using CatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    public CategoriesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("/usingFromService/{name}")]
    public ActionResult<string> GetGreetingFromServices([FromServices] IMyService myService, string name)
    {
        return myService.Greeting(name);
    }
    
    [HttpGet("/notUsingFromService/{name}")]
    public ActionResult<string> GetGreetingNotFromServices(IMyService myService, string name)
    {
        return myService.Greeting(name);
    }
    
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProductsAsync()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Include(p => p.Products)
            .ToListAsync();

        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return categories;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
    {
        var categories = await _context.Categories.AsNoTracking().ToListAsync();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return categories;
    }
    
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Category>> GetAsync(int id)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.CategoryId == id);
            
        if (category is null)
        {
            return NotFound("No category found");
        }
        return category;
    }
    
    [HttpPost]
    public ActionResult Post(Category category)
    {
        if (category is null) return BadRequest();
            
        _context.Categories.Add(category);
        _context.SaveChanges();

        return new CreatedAtRouteResult("",
            new { id = category.CategoryId }, category);
    }
    
    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        _context.Entry(category).State = EntityState.Modified;
        _context.SaveChanges();

        return Ok(category);
    }
    
    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);

        if (category is null)
        {
            return NotFound("No category found");
        }
            
        _context.Categories.Remove(category);
        _context.SaveChanges();
            
        return Ok();
    }
}