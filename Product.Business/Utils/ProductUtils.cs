using ProductAPI.Models;
using ProductAPI.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductAPI.Business.Utils
{
    internal static class ProductUtils
    {
        internal static ProductModel? Map(Product entity) 
        {
            if (entity == null) return null;

            return new ProductModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Price = entity.Price
            };
        }

        internal static Product? Map(ProductModel model)
        {
            if (model == null) return null;

            return new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        internal static IEnumerable<ProductModel> Map(IEnumerable<Product> entities)
        {
            var models = new List<ProductModel>();
            foreach (var entity in entities)
            {
                var model = Map(entity);
                if (model != null)
                {
                    models.Add(model);
                }
            }
            return models;
        }
    }
}
