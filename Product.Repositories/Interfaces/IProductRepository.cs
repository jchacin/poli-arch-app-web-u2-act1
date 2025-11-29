using ProductAPI.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product?> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product product);
        Task<bool> DeleteProduct(int id);
    }
}
