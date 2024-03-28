using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Commands
{
    public class InvoiceCheckoutCommand
    {
        public OrderCmd Orders { get; set; }
        public List<OrderDetailCmd> Details { get; set; }
        public List<ProductInfoCmd> ProductInfos { get; set; }
    }
    public class OrderCmd
    {
        public int OrderId { get; set; }

        public string? CustomerId { get; set; }

        public int? EmployeeId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public int? ShipVia { get; set; }

        public decimal? Freight { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }

        public string? ShipCity { get; set; }

        public string? ShipRegion { get; set; }

        public string? ShipPostalCode { get; set; }

        public string? ShipCountry { get; set; }

        //public virtual Customer? Customer { get; set; }

        //public virtual Employee? Employee { get; set; }

        public virtual List<OrderDetailCmd> OrderDetails { get; set; }
    }

    public class OrderDetailCmd
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public short Quantity { get; set; }

        public float Discount { get; set; }

        public virtual OrderCmd Orders { get; set; }
    }
    
    public class ProductInfoCmd
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string? CompanyName{ get; set; }

        public string? CategoryName{ get; set; }

        public string? QuantityPerUnit{ get; set; }

        public int? UnitsInStock{ get; set; }
    }

}
