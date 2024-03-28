using SalePayment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SalePayment.Domain.Repositories;
using SalePayment.Infrastructure.Data;
using System.Linq;
using System.Text;

namespace SalePayment.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SalePaymentContext _context;

        public OrderRepository(SalePaymentContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(int id)
        {
            
            var order = await _context.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();
            
            if (order == null)
            {
                throw new NotImplementedException();
            }
            return order;
        }

        public async Task<List<OrderDetail>> GetOrderDetailByOrderId(int id)
        {
            var orderDetail = await _context.OrderDetails.Include(o => o.Order).Where(o =>o.OrderId == id).ToListAsync();
            if (orderDetail == null)
            {
                throw new NotImplementedException();
            }
            return orderDetail;
        }

        public Task<Order> CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}