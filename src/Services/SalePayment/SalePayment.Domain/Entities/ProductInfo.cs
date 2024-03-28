using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePayment.Domain.Entities
{
    public class ProductInfo
    {
        public int ProductId;

        public string ProductName;

        public string? CompanyName;

        public string? CategoryName;

        public string? QuantityPerUnit;

        public int? UnitsInStock;
    }
}
