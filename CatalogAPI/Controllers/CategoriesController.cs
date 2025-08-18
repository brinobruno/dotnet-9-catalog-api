using CatalogAPI.Context;
using CatalogAPI.Domain;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ICategoriesRepository  _repo;
    public CategoriesController(AppDbContext context, ICategoriesRepository  repo)
    {
        _context = context;
        _repo = repo;
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
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = await _repo.GetCategoriesAsync();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return Ok(categories);
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