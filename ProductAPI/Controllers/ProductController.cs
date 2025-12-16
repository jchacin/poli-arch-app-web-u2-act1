using Microsoft.AspNetCore.Mvc;
using ProductAPI.Business.Interfaces;
using ProductAPI.Models; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAll()
        {
            
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        
        [HttpPost]
        public async Task<ActionResult<ProductModel>> Create(ProductModel product)
        {
            
            var createdProduct = await _productService.CreateProduct(product);

            
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductModel>> Update(int id, ProductModel product)
        {
            if (id != product.Id)
            {
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");
            }

            var updatedProduct = await _productService.UpdateProduct(id, product);

            if (updatedProduct == null)
            {
                return NotFound();
            }

            return Ok(updatedProduct);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProduct(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}