using ProductAPI.Models;
using ProductAPI.Business.Interfaces;

namespace ProductAPI.GraphQL
{
    public class Query
    {
        // 1. Obtener todos los productos
        // El atributo [Service] inyecta tu IProductService automáticamente
        public async Task<IEnumerable<ProductModel>> GetProducts([Service] IProductService productService)
        {
            return await productService.GetAllProducts();
        }

        // 2. Obtener un producto por ID
        public async Task<ProductModel?> GetProductById([Service] IProductService productService, int id)
        {
            return await productService.GetProductById(id);
        }
    }
}