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
    private readonly IUnitOfWork _uof;
    public CategoriesController(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProducts()
    {
        var categories = await _uof.CategoryRepository.GetCategoriesProductsAsync();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return Ok(categories);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> Get()
    {
        var categories = await _uof.CategoryRepository.GetAsync();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return Ok(categories);
    }
    
    [HttpGet("{id:int:min(1)}")]
    public async Task<ActionResult<Category>> GetAsync(int id)
    {
        var category = await _uof.CategoryRepository.GetAsync(c => c.CategoryId == id);
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
            
        var newCategory = _uof.CategoryRepository.Create(category);
        _uof.Commit();

        return new CreatedAtRouteResult("",
            new { id = newCategory.CategoryId }, newCategory);
    }
    
    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, Category category)
    {
        if (id != category.CategoryId)
        {
            return BadRequest();
        }

        var updatedCategory = _uof.CategoryRepository.Update(category);
        _uof.Commit();

        return Ok(updatedCategory);
    }
    
    [HttpDelete("{id:int:min(1)}")]
    public async Task<ActionResult> Delete(int id)
    {
        var category = await _uof.CategoryRepository.GetAsync(c => c.CategoryId == id);

        if (category is null)
        {
            return NotFound("No category found");
        }
            
        _uof.CategoryRepository.Delete(category);
        _uof.Commit();
            
        return Ok();
    }
}