using Microsoft.AspNetCore.Mvc;
using ProductAPI.Business.Interfaces;
using ProductAPI.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<ProductModel>> Get()
        {
            return await _productService.GetAllProducts();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> Get(int id)
        {
            var product = await _productService.GetProductById(id);
            if (product != null) return Ok(product);
            return NotFound();
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductModel model)
        {
            var newProduct = await _productService.CreateProduct(model);
            return CreatedAtAction(nameof(Get), new { id = newProduct.Id }, newProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductModel model)
        {
            var updatedProduct = await _productService.UpdateProduct(id, model);
            if (updatedProduct != null) return Ok();
            return NotFound();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteProduct(id);
            if (deleted) return Ok();
            return NotFound();
        }
    }
}
