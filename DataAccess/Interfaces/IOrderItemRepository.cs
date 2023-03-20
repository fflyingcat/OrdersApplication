using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IOrderItemRepository
    {
        public Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem);
        public Task<OrderItem> GetOrderItemAsync(int orderItemId);
        public IQueryable<OrderItem> GetAllOrderItems();
        public Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem);
        public Task DeleteOrderItemAsync(int orderItemId);
    }
}