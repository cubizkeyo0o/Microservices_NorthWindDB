
using Catalog.Domain.Data;
using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<List<ProductGrpcService>> GetProductGrpc(List<int> listId);
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProductByName(string name);
        Task<List<ProductDTO>> GetProductByNameSupplier(string name);
        Task<Product> CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
