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
        private readonly IRepository<Product> _repo;
        private readonly IProductsRepository _productRepo;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductsRepository repo, IProductsRepository productRepo, ILogger<ProductsController> logger)
        {
            _repo = repo;
            _productRepo = productRepo;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            var products = await _repo.GetAsync();
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

            var product = await _repo.GetAsync(p => p.ProductId == id);
            
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

            var createdProduct = _repo.Create(product);

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

            var updatedProduct = _repo.Update(product);

            return Ok(updatedProduct);
        }
        
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _repo.GetAsync(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound("No product found");
            }

            var deletedProduct = _repo.Delete(product);
            
            return Ok(deletedProduct.ProductId);
        }
    }
}
