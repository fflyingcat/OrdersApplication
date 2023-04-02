using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Common.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderOutDto> CreateOrderAsync(OrderInDto order)
        {
            return _mapper.Map<OrderOutDto>(await _orderRepository.CreateOrderAsync(_mapper.Map<Order>(order)));
        }

        public async Task<OrderOutDto> GetOrderAsync(int orderId)
        {
            return _mapper.Map<OrderOutDto>(await _orderRepository.GetOrderAsync(orderId));
        }

        public async Task<OrderFilterDto> GetOrderFilterAsync()
        {
            var orderItems = _orderRepository.GetAllOrders();

            var filter = new OrderFilterDto
            {
                OrderNumbers = await orderItems.Select(o => o.Number).Distinct().AsNoTracking().ToListAsync(),
                Providers = _mapper.Map<List<ProviderDto>>(await orderItems.Include(o => o.Provider)
                    .Select(o => o.Provider).Distinct().AsNoTracking().ToListAsync())
            };

            return filter;
        }

        public async Task<IEnumerable<OrderOutDto>> GetAllOrdersAsync(DateTime? startPeriod,
            DateTime? endPeriod, List<string> numbers, List<int> providerIds)
        {
            var orders = _orderRepository.GetAllOrders();

            if (startPeriod != null)
            {
                orders = orders.Where(o => o.Date >= startPeriod);
            }

            if (endPeriod != null)
            {
                orders = orders.Where(o => o.Date <= endPeriod);
            }

            if (numbers.Any())
            {
                orders = orders.Where(o => numbers.Contains(o.Number));
            }

            if (providerIds.Any())
            {
                orders = orders.Where(o => providerIds.Contains(o.ProviderId));
            }

            return _mapper.Map<IEnumerable<OrderOutDto>>(await orders.ToListAsync());
        }

        public async Task<OrderOutDto> UpdateOrderAsync(int orderId, OrderInDto order)
        {
            var requestOrder = _mapper.Map<Order>(order);
            requestOrder.Id = orderId;

            return _mapper.Map<OrderOutDto>(await _orderRepository.UpdateOrderAsync(requestOrder));
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}