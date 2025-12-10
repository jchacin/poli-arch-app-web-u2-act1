using Microsoft.AspNetCore.Mvc;
using ProductAPI.Business.Interfaces;
using ProductAPI.Models; // Aquí debe estar tu clase ProductModel
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

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAll()
        {
            // Coincide con GetAllProducts() de tu interfaz
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetById(int id)
        {
            // Coincide con GetProductById(id) de tu interfaz
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<ProductModel>> Create(ProductModel product)
        {
            // Coincide con CreateProduct(product) de tu interfaz
            var createdProduct = await _productService.CreateProduct(product);

            // Retorna 201 Created y la ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        // PUT: api/product/5 (Opcional: Agregado porque lo vi en tu interfaz)
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

        // DELETE: api/product/5 (Opcional: Agregado porque lo vi en tu interfaz)
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