using ProductAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetAllProducts();
        Task<ProductModel?> GetProductById(int id);
        Task<ProductModel> CreateProduct(ProductModel product);
        Task<ProductModel?> UpdateProduct(int id, ProductModel product);
        Task<bool> DeleteProduct(int id);
    }
}
