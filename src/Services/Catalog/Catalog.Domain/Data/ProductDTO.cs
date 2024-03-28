using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Data
{
    public class ProductDTO
    {
        public int ProductId { get; set; }

        public string? ProductName { get; set; } 

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public string? CompanyName { get; set; }

        public string? ContactName { get; set; }
        public string? Address { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }

        public string? Phone { get; set; }
    }
}
