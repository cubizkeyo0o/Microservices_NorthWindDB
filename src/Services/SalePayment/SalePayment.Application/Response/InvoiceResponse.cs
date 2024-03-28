using SalePayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePayment.Application.Response
{
    public class InvoiceResponse
    {
        public Order Orders { get; set; }
        public List<OrderDetail> Details { get; set; }
        public  List<ProductInfo> ProductInfos { get; set; }
    }
}
