using AutoMapper;
using BusinessLogic.Interfaces;
using Common.DTO;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderItemOutDto> CreateOrderItemAsync(OrderItemInDto orderItem)
        {
            return _mapper.Map<OrderItemOutDto>(
                await _orderItemRepository.CreateOrderItemAsync(_mapper.Map<OrderItem>(orderItem)));
        }

        public async Task<OrderItemOutDto> GetOrderItemAsync(int orderItemId)
        {
            return _mapper.Map<OrderItemOutDto>(await _orderItemRepository.GetOrderItemAsync(orderItemId));
        }

        public async Task<OrderItemFilterDto> GetOrderItemFilterAsync()
        {
            var orderItems = _orderItemRepository.GetAllOrderItems();

            var filter = new OrderItemFilterDto
            {
                OrderItemNames = await orderItems.Select(oi => oi.Name).Distinct().AsNoTracking().ToListAsync(),
                OrderItemUnits = await orderItems.Select(oi => oi.Unit).Distinct().AsNoTracking().ToListAsync()
            };

            return filter;
        }

        public async Task<IEnumerable<OrderItemOutDto>> GetAllOrderItemsAsync(int orderId, List<string> names,
            List<string> units)
        {
            var orderItems = _orderItemRepository.GetAllOrderItems();

            orderItems = orderItems.Where(oi => oi.OrderId == orderId);

            if (names.Any())
            {
                orderItems = orderItems.Where(oi => names.Contains(oi.Name));
            }

            if (units.Any())
            {
                orderItems = orderItems.Where(oi => units.Contains(oi.Unit));
            }

            return _mapper.Map<IEnumerable<OrderItemOutDto>>(await orderItems.AsNoTracking().ToListAsync());
        }

        public async Task<OrderItemOutDto> UpdateOrderItemAsync(int orderItemId, OrderItemInDto orderItem)
        {
            var requestOrderItem = _mapper.Map<OrderItem>(orderItem);
            requestOrderItem.Id = orderItemId;

            return _mapper.Map<OrderItemOutDto>(await _orderItemRepository.UpdateOrderItemAsync(requestOrderItem));
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            await _orderItemRepository.DeleteOrderItemAsync(orderItemId);
        }
    }
}