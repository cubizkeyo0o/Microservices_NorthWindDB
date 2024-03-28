
using SalePayment.Domain.Entities;


namespace SalePayment.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(int id);
        Task<List<OrderDetail>> GetOrderDetailByOrderId(int id);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(Order order);
        Task<Order> DeleteOrder(Order order);
    }
}
