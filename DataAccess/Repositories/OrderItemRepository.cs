using Common.Exceptions;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DataContext _context;

        public OrderItemRepository(DataContext context)
        {
            _context = context;
        }

        private async Task CheckOrderId(int orderId)
        {
            if (!await _context.Orders.AnyAsync(o => o.Id == orderId))
            {
                throw new ForeignKeyException($"Order with id = {orderId} is not exists.");
            }
        }

        private async Task CheckOrderItemId(int orderId)
        {
            if (!await _context.OrderItems.AnyAsync(oi => oi.Id == orderId))
            {
                throw new PrimaryKeyException($"Order item with id = {orderId} is not exists.");
            }
        }

        private async Task CheckOrderNumberEqualsOrderItemName(string orderItemName)
        {
            if (await _context.Orders.AnyAsync(o => o.Number == orderItemName))
            {
                throw new OrderNumberEqualsOrderItemNameException(
                    $"An order number = {orderItemName} already exists.");
            }
        }

        public async Task<OrderItem> CreateOrderItemAsync(OrderItem orderItem)
        {
            await CheckOrderId(orderItem.OrderId);
            await CheckOrderNumberEqualsOrderItemName(orderItem.Name);

            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task<OrderItem> GetOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.Where(oi => oi.Id == orderItemId).AsNoTracking()
                .SingleOrDefaultAsync();

            if (orderItem == null)
            {
                throw new PrimaryKeyException($"Order item with id = {orderItemId} is not exists.");
            }

            return orderItem;
        }

        public IQueryable<OrderItem> GetAllOrderItems()
        {
            return _context.OrderItems.AsNoTracking().AsQueryable();
        }

        public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
        {
            await CheckOrderItemId(orderItem.Id);
            await CheckOrderId(orderItem.OrderId);
            await CheckOrderNumberEqualsOrderItemName(orderItem.Name);

            _context.Entry(orderItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return orderItem;
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.Where(oi => oi.Id == orderItemId).FirstOrDefaultAsync();

            if (orderItem == null)
            {
                throw new PrimaryKeyException($"Order item with id = {orderItemId} is not exists.");
            }

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}