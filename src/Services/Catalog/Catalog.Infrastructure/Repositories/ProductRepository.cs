using System.Linq;
using Catalog.Domain.Data;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions.EFCore;
using AutoMapper;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, ISupplierRepository
    {
        private readonly CatalogContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(CatalogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ProductGrpcService>> GetProductGrpc(List<int> listId)
        {

            FormattableString rawquery = $"SELECT P.ProductID, P.ProductName, S.CompanyName, C.CategoryName, P.QuantityPerUnit, P.UnitsInStock FROM Products P JOIN Suppliers S ON P.SupplierID = S.SupplierID JOIN Categories C ON P.CategoryID = C.CategoryID WHERE ProductID IN ({string.Join(",", listId.Select(id => id.ToString()))})";
            //var produc = (_context.Products.WhereBulkContains(listId))
            var product = await (from p in (_context.Products.WhereBulkContains(listId))
                          join s in _context.Suppliers on p.SupplierId equals s.SupplierId
                          join c in _context.Categories on p.CategoryId equals c.CategoryId
                          select new Catalog.Domain.Entities.ProductGrpcService
                          {
                              ProductId = p.ProductId,
                              ProductName = p.ProductName,
                              CompanyName = s.CompanyName,
                              CategoryName = c.CategoryName,
                              QuantityPerUnit = p.QuantityPerUnit,
                              UnitsInStock = (int)p.UnitsInStock,

                          }).ToListAsync();
            if ( product == null )
            {
                throw new NotImplementedException();
            }
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return (await _context.Products.ToListAsync());
        }

        public Task<Product> CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                return product;
            }
        }

        public async Task<List<Product>> GetProductByName(string name)
        {
            var product = await _context.Products.Where(p => p.ProductName == name).ToListAsync();
            if(product == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                return product;
            }
            
        }

        public Task<bool> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductDTO>> GetProductByNameSupplier(string name)
        {

            FormattableString rawquery = $"SELECT P.ProductID, P.ProductName,P.SupplierID, P.CategoryID, P.QuantityPerUnit,P.UnitPrice,s.CompanyName,s.ContactName,s.Address ,s.City,s.Country,s.Phone AS TAB FROM Products P RIGHT JOIN Suppliers S ON P.SupplierID = S.SupplierID WHERE CompanyName = {0}";
            var productList =await _context.Database.SqlQuery<ProductDTO>($"SELECT P.ProductID, P.ProductName,P.SupplierID, P.CategoryID, P.QuantityPerUnit,P.UnitPrice,s.CompanyName,s.ContactName,s.Address ,s.City,s.Country,s.Phone FROM Products P RIGHT JOIN Suppliers S ON P.SupplierID = S.SupplierID WHERE CompanyName = {name}").ToListAsync();
            return productList;
            
        }

        public async Task<List<Supplier>> GetAllSupplier()
        {
            return (await _context.Suppliers.ToListAsync());
            
        }

        public Task<Supplier> GetSupplierById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Supplier> GetSupplierByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}