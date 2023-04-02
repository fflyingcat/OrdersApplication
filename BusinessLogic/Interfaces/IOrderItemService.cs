using Common.DTO;

namespace BusinessLogic.Interfaces
{
    public interface IOrderItemService
    {
        public Task<OrderItemOutDto> CreateOrderItemAsync(OrderItemInDto orderItem);
        public Task<OrderItemOutDto> GetOrderItemAsync(int orderItemId);
        public Task<OrderItemFilterDto> GetOrderItemFilterAsync();
        public Task<IEnumerable<OrderItemOutDto>> GetAllOrderItemsAsync(int orderId, List<string> names, List<string> units);
        public Task<OrderItemOutDto> UpdateOrderItemAsync(int orderItemId, OrderItemInDto orderItem);
        public Task DeleteOrderItemAsync(int orderItemId);
    }
}
