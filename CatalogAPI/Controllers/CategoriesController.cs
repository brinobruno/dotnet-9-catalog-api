using CatalogAPI.Context;
using CatalogAPI.Domain;
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
    
    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        var categories = _context.Categories.Include(p => p.Products).ToList();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return categories;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        var categories = _context.Categories.ToList();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return categories;
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<Category>Get(int id)
    {
        var category = _context.Categories.FirstOrDefault(p => p.CategoryId == id);
            
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
    
    [HttpPut("{id:int}")]
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
    
    [HttpDelete("{id:int}")]
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