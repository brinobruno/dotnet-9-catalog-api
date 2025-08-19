using CatalogAPI.Context;
using CatalogAPI.Domain;
using CatalogAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IUnitOfWork uof, ILogger<ProductsController> logger)
        {
            _uof = uof;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            var products = await _uof.ProductRepository.GetAsync();
            if (products is null)
            {
                return NotFound("No products found");
            }
            return Ok(products);
        }
        
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            _logger.LogInformation($"Getting product {id}");

            var product = await _uof.ProductRepository.GetAsync(p => p.ProductId == id);
            
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

            var createdProduct = _uof.ProductRepository.Create(product);
            _uof.Commit();

            return new CreatedAtRouteResult("",
            new { id = createdProduct.ProductId }, createdProduct);
        }
        
        [HttpPut("{id:int:min(1)}")]
        public ActionResult Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            var updatedProduct = _uof.ProductRepository.Update(product);
            _uof.Commit();

            return Ok(updatedProduct);
        }
        
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _uof.ProductRepository.GetAsync(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound("No product found");
            }

            var deletedProduct = _uof.ProductRepository.Delete(product);
            _uof.Commit();
            
            return Ok(deletedProduct.ProductId);
        }
    }
}
