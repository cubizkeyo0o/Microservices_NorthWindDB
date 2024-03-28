using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Responses
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public int? SupplierId { get; set; }

        public int? CategoryId { get; set; }

        public string? QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        //test mapper 
        //public bool Discontinued { get; set; }

    }
}
