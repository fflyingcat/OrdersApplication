using Common.Exceptions;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }

        private async Task CheckOrderNumberProviderIdUnique(int? orderId, string orderNumber, int orderProviderId)
        {
            if (await _context.Orders.AnyAsync(o =>
                    o.Number == orderNumber && o.ProviderId == orderProviderId && o.Id != orderId))
            {
                throw new OrderNumberProviderIdIsNotUniqueException(
                    $"An order with number = {orderNumber} and provider = {orderProviderId} already exists.");
            }
        }

        private async Task CheckProviderId(int providerId)
        {
            if (!await _context.Providers.AnyAsync(p => p.Id == providerId))
            {
                throw new ForeignKeyException($"Provider with id = {providerId} is not exists.");
            }
        }

        private async Task CheckOrderId(int orderId)
        {
            if (!await _context.Orders.AnyAsync(o => o.Id == orderId))
            {
                throw new PrimaryKeyException($"Order with id = {orderId} is not exists.");
            }
        }

        private async Task CheckOrderNumberEqualsOrderItemName(string orderNumber)
        {
            if (await _context.OrderItems.AnyAsync(oi => oi.Name == orderNumber))
            {
                throw new OrderNumberEqualsOrderItemNameException(
                    $"An order item name = {orderNumber} already exists.");
            }
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await CheckProviderId(order.ProviderId);
            await CheckOrderNumberProviderIdUnique(null, order.Number, order.ProviderId);
            await CheckOrderNumberEqualsOrderItemName(order.Number);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            var order = await _context.Orders.Where(o => o.Id == orderId).AsNoTracking().SingleOrDefaultAsync();

            if (order == null)
            {
                throw new PrimaryKeyException($"Order with id = {orderId} is not exists.");
            }

            return order;
        }

        public IQueryable<Order> GetAllOrders()
        {
            return _context.Orders.AsNoTracking().AsQueryable();
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            await CheckOrderId(order.Id);
            await CheckProviderId(order.ProviderId);
            await CheckOrderNumberProviderIdUnique(order.Id, order.Number, order.ProviderId);
            await CheckOrderNumberEqualsOrderItemName(order.Number);

            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.Where(o => o.Id == orderId).FirstOrDefaultAsync();

            if (order == null)
            {
                throw new PrimaryKeyException($"Order with id = {orderId} is not exists.");
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}