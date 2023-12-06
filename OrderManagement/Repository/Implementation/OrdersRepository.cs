using Microsoft.EntityFrameworkCore;
using OrderManagement.Context;
using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;

namespace OrderManagement.Repository.Implementation
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly EcommerceContext dbContext;
        public OrdersRepository( EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ShippingOrder>> GetAllOrders()
        {
            return await dbContext.ShippingOrders
                .Include(order => order.OrderedItems)
                    .ThenInclude(item => item.ProductItem)
                .OrderByDescending(order => order.OrderDate)
                .ToListAsync(); ;
        }

        public async Task<ShippingOrder> GetOrderById(Guid orderId)
        {
            return await dbContext.ShippingOrders
                .Include(order => order.OrderedItems)
                    .ThenInclude(item => item.ProductItem)
                .FirstOrDefaultAsync(order => order.OrderId == orderId);
        }

        public async Task<List<ShippingOrder>> GetRecentOrders()
        {
            return await dbContext.ShippingOrders
                .Include(order => order.OrderedItems)
                    .ThenInclude(item => item.ProductItem)
                .OrderByDescending(order => order.OrderDate)
                .Take(5)
                .ToListAsync();
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            var order = await dbContext.ShippingOrders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                order.OrderStatus = newStatus;
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<OrderedItem>> TotalOrders()
        {
            return await dbContext.OrderedItems.ToListAsync();
        }

        public async Task<List<OrderedItem>> TotalRevenue()
        {
            return await dbContext.OrderedItems.ToListAsync();
        }

        public async Task<List<OrderedItem>> TotalSales()
        {
            return await dbContext.OrderedItems.ToListAsync();  
        }
        public async Task<Dictionary<string, int>> GetOrderCountsByStatusAsync()
        {
            var orderStatusCounts = await dbContext.ShippingOrders
                .GroupBy(o => o.OrderStatus)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            return orderStatusCounts;
        }
    }
}
