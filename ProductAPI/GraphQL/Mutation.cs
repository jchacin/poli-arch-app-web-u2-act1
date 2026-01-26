using ProductAPI.Models;
using ProductAPI.Business.Interfaces;

namespace ProductAPI.GraphQL
{
    public class Mutation
    {
        // 1. Crear Producto
        public async Task<ProductModel> CreateProduct([Service] IProductService productService, ProductModel product)
        {
            return await productService.CreateProduct(product);
        }

        // 2. Actualizar Producto
        public async Task<ProductModel?> UpdateProduct([Service] IProductService productService, int id, ProductModel product)
        {
            return await productService.UpdateProduct(id, product);
        }

        // 3. Eliminar Producto
        public async Task<bool> DeleteProduct([Service] IProductService productService, int id)
        {
            return await productService.DeleteProduct(id);
        }
    }
}