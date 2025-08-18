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
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(AppDbContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            if (products is null)
            {
                return NotFound("No products found");
            }
            return products;
        }
        
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            _logger.LogInformation($"Getting product {id}");
            
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == id);
            
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
        
        [HttpPut("{id:int:min(1)}")]
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
        
        [HttpDelete("{id:int:min(1)}")]
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
