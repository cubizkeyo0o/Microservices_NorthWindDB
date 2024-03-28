using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Repositories
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllSupplier();
        Task<Supplier> GetSupplierById(int id);
        Task<Supplier> GetSupplierByName(string name);
        
    }
}
