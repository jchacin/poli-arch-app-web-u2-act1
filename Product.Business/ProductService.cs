using ProductAPI.Models; // O el namespace donde esté tu clase 'Product'
using ProductAPI.Repositories.Interfaces; // Para IProductRepository
using ProductAPI.Business.Interfaces;
using ProductAPI.Business.Utils;

using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) 
        {
            _productRepository = productRepository;
        }

        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            var entity = ProductUtils.Map(product);
            var newProduct = await _productRepository.CreateProduct(entity);
            return ProductUtils.Map(newProduct);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            return await _productRepository.DeleteProduct(id);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts()
                .ContinueWith(t => ProductUtils.Map(t.Result));
        }

        public async Task<ProductModel?> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id)
                .ContinueWith(t => 
                    t.Result != null ? ProductUtils.Map(t.Result) : null
                );
        }

        public async Task<ProductModel?> UpdateProduct(int id, ProductModel product)
        {
            var entity = ProductUtils.Map(product);
            if (entity == null) return await Task.FromResult<ProductModel?>(null);
            return await _productRepository.UpdateProduct(id, entity)
                .ContinueWith(t => 
                    t.Result != null ? ProductUtils.Map(t.Result) : null
                );
        }
    }
}
