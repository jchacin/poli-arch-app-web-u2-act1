using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using ProductAPI.Business.Interfaces;
using ProductAPI.Protos;

namespace ProductAPI
{
    public class ProductGrpcService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(IProductService productService, ILogger<ProductGrpcService> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public override async Task<ProductList> GetAll(Empty request, ServerCallContext context)
        {
            try
            {
                var products = await _productService.GetAllProducts();
                var response = new ProductList();

                foreach (var product in products)
                {
                    response.Products.Add(MapToProtoProduct(product));
                }

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los productos");
                throw new RpcException(new Status(StatusCode.Internal, "Error al obtener productos"));
            }
        }

        public override async Task<Protos.ProductModel> GetById(GetProductRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _productService.GetProductById(request.Id);

                if (product == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));
                }

                return MapToProtoProduct(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto con ID {id}", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, "Error al obtener producto"));
            }
        }

        public override async Task<Protos.ProductModel> Create(CreateProductRequest request, ServerCallContext context)
        {
            try
            {
                var productModel = new Models.ProductModel
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = (decimal)request.Price
                };

                var createdProduct = await _productService.CreateProduct(productModel);
                return MapToProtoProduct(createdProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear producto");
                throw new RpcException(new Status(StatusCode.Internal, "Error al crear producto"));
            }
        }

        public override async Task<Protos.ProductModel> Update(UpdateProductRequest request, ServerCallContext context)
        {
            try
            {
                if (request.Id <= 0)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "ID inválido"));
                }

                var productModel = new Models.ProductModel
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Description,
                    Price = (decimal)request.Price
                };

                var updatedProduct = await _productService.UpdateProduct(request.Id, productModel);

                if (updatedProduct == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));
                }

                return MapToProtoProduct(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar producto con ID {id}", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, "Error al actualizar producto"));
            }
        }

        public override async Task<DeleteProductResponse> Delete(DeleteProductRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _productService.DeleteProduct(request.Id);

                if (!result)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Producto no encontrado"));
                }

                return new DeleteProductResponse { Success = true };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar producto con ID {id}", request.Id);
                throw new RpcException(new Status(StatusCode.Internal, "Error al eliminar producto"));
            }
        }

        private Protos.ProductModel MapToProtoProduct(Models.ProductModel product)
        {
            return new Protos.ProductModel
            {
                Id = product.Id,
                Name = product.Name ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Price = product.Price != null ? (double)product.Price : 0
            };
        }
    }
}
