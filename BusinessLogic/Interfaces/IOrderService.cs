using Common.DTO;

namespace BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderOutDto> CreateOrderAsync(OrderInDto order);
        public Task<OrderOutDto> GetOrderAsync(int orderId);
        public Task<OrderFilterDto> GetOrderFilterAsync();

        public Task<IEnumerable<OrderOutDto>> GetAllOrdersAsync(DateTime? startPeriod,
            DateTime? endPeriod, List<string> numbers, List<int> providerIds);

        public Task<OrderOutDto> UpdateOrderAsync(int orderId, OrderInDto order);
        public Task DeleteOrderAsync(int orderId);
    }
}