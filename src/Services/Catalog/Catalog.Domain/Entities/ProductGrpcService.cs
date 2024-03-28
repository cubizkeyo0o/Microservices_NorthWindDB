using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{
    public partial class ProductGrpcService
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } 

        public string CompanyName { get; set; }

        public string CategoryName { get; set; }

        public string QuantityPerUnit { get; set; }

        public int UnitsInStock { get; set; }

        
    }
}
