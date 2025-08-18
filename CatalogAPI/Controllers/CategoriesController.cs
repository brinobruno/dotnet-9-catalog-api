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
    private readonly ICategoriesRepository  _repo;
    public CategoriesController(ICategoriesRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesProducts()
    {
        var categories = await _repo.GetCategoriesProductsAsync();
        if (categories is null)
        {
            return NotFound("No categories found");
        }
        return Ok(categories);
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
        var category = await _repo.GetCategoryByIdAsync(id);
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
            
        var newCategory = _repo.CreateCategory(category);

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

        var updatedCategory = _repo.UpdateCategory(category);

        return Ok(updatedCategory);
    }
    
    [HttpDelete("{id:int:min(1)}")]
    public ActionResult Delete(int id)
    {
        var category = _repo.GetCategoryById(id);

        if (category is null)
        {
            return NotFound("No category found");
        }
            
        _repo.DeleteCategory(category);
            
        return Ok();
    }
}