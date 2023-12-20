using Microsoft.EntityFrameworkCore;
using OrderManagement.Context;
using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;
using OrderManagement.Service;
using System.Globalization;

namespace OrderManagement.Repository.Implementation
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly EcommerceContext dbContext;
        private readonly EmailService emailService;
        public OrdersRepository( EcommerceContext dbContext, EmailService emailService)
        {
            this.dbContext = dbContext;
            this.emailService = emailService;
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
        //this is the api to update order status
        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            var order = await dbContext.ShippingOrders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order != null)
            {
                order.OrderStatus = newStatus;
                await dbContext.SaveChangesAsync();
                var customerId = order.CustomerId;
                string customerEmail = await  GetCustomerEmailById(customerId);

                // Send email notification
                await emailService.SendOrderStatusEmailNotification(customerEmail, orderId, newStatus);
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

        public async Task<object> GetOrdersCountByMonthForLast12MonthsAsync()
        {
            var last12Months = Enumerable.Range(0, 12)
                .Select(i => DateTime.Now.AddMonths(-i).Date.Month) // Get month numbers for the last 12 months
                .ToList();

            var ordersCountByMonth = await dbContext.ShippingOrders
                .Where(order => last12Months.Contains(order.OrderDate.Month)) // Filter orders within the last 12 months
                .GroupBy(order => order.OrderDate.Month)
                .Select(group => new
                {
                    Month = group.Key,
                    Count = group.Count()
                })
                .ToDictionaryAsync(item => item.Month, item => item.Count); // Convert to dictionary for easier lookup

            var result = new List<object>();

            for (int i = 1; i <= 12; i++) // Loop through all 12 months
            {
                if (ordersCountByMonth.TryGetValue(i, out var count)) // Check if month exists in ordersCountByMonth dictionary
                {
                    result.Add(new
                    {
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                        Count = count
                    });
                }
                else // If month doesn't have any orders, add it with count zero
                {
                    result.Add(new
                    {
                        MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                        Count = 0
                    });
                }
            }

            return result;
        }

        public async Task<object> GetOrdersCountForLastSevenDaysAsync()
        {
            var lastSevenDays = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i)) // Get dates for the last 7 days
                .ToList();

            var ordersCountForLastSevenDays = await dbContext.ShippingOrders
                .Where(order => lastSevenDays.Contains(order.OrderDate.Date)) // Filter orders within the last 7 days
                .GroupBy(order => order.OrderDate.Date)
                .Select(group => new
                {
                    Day = group.Key.ToString("dddd"), // Get the day name
                    Count = group.Count()
                })
                .ToDictionaryAsync(item => item.Day, item => item.Count); // Convert to dictionary for easier lookup

            var result = new List<object>();

            var dayNames = CultureInfo.CurrentCulture.DateTimeFormat.DayNames; // Get day names for the current culture

            for (int i = 0; i < 7; i++) // Loop through last 7 days
            {
                var day = DateTime.Now.Date.AddDays(-i);
                var dayName = day.ToString("dddd");
                if (ordersCountForLastSevenDays.TryGetValue(dayName, out var count)) // Check if day exists in ordersCountForLastSevenDays dictionary
                {
                    result.Add(new
                    {
                        DayName = dayNames[(int)day.DayOfWeek], // Get the localized day name
                        Count = count
                    });
                }
                else // If day doesn't have any orders, add it with count zero
                {
                    result.Add(new
                    {
                        DayName = dayNames[(int)day.DayOfWeek], // Get the localized day name
                        Count = 0
                    });
                }
            }

            return result;
        }

        public async Task<int> GetTotalOrdersCountForTodayAsync()
        {
            DateTime todayStart = DateTime.Now.Date;
            DateTime todayEnd = DateTime.Now.Date.AddDays(1).AddTicks(-1);

            var totalOrdersCountForToday = await dbContext.ShippingOrders
                .CountAsync(order => order.OrderDate >= todayStart && order.OrderDate <= todayEnd);

            return totalOrdersCountForToday;
        }

        public async Task<List<object>> GetProductCountsByBrandAsync()
        {
            var brandsWithProductCounts = await dbContext.Brands
                .Select(brand => new
                {
                    BrandName = brand.BrandName,
                    ProductCount = brand.Products
                        .SelectMany(product => product.ProductItemDetails)
                        .SelectMany(itemDetail => itemDetail.OrderedItems)
                        .Sum(orderedItem => orderedItem.Quantity)
                })
                .ToListAsync<object>();

            return brandsWithProductCounts;
        }

        public async Task<double> GetAverageReturnRateAsync()
        {
            var totalOrdersCount = await dbContext.ShippingOrders.CountAsync();
            var returnedOrdersCount = await dbContext.ShippingOrders.CountAsync(order => order.OrderStatus == "Returned");

            if (totalOrdersCount == 0)
            {
                return 0;
            }

            return ((double)returnedOrdersCount / totalOrdersCount)*100;
        }

        public async Task<string> GetCustomerEmailById(Guid customerId)
        {
            var customer = await dbContext.CustomerCredentials.FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer != null)
            {
                return customer.EmailId;
            }

            throw new Exception("Customer not found");
        }
    }
}
