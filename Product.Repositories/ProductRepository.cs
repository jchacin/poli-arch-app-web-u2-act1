using ProductAPI.Repositories.Entities;
using ProductAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> UpdateProduct(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
