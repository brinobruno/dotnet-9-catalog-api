using CatalogAPI.Context;
using CatalogAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _context.Products.AsNoTracking().ToList();
            if (products is null)
            {
                return NotFound("No products found");
            }
            return products;
        }
        
        [HttpGet("{id:int}")]
        public ActionResult<Product>Get(int id)
        {
            var product = _context.Products.AsNoTracking().FirstOrDefault(p => p.ProductId == id);
            
            if (product is null)
            {
                return NotFound("No product found");
            }
            return product;
        }

        [HttpPost]
        public ActionResult Post(Product product)
        {
            if (product is null) return BadRequest();
            
            _context.Products.Add(product);
            _context.SaveChanges();

            return new CreatedAtRouteResult("",
            new { id = product.ProductId }, product);
        }
        
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(product);
        }
        
        [HttpDelete("{id:int}")]
        public ActionResult<Product>Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound("No product found");
            }
            
            _context.Products.Remove(product);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
