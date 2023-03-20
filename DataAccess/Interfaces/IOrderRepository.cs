using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        public Task<Order> CreateOrderAsync(Order order);
        public Task<Order> GetOrderAsync(int orderId);
        public IQueryable<Order> GetAllOrders();
        public Task<Order> UpdateOrderAsync(Order order);
        public Task DeleteOrderAsync(int orderId);
    }
}