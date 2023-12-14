using OrderManagement.Models.Domain;

namespace OrderManagement.Repository.Interface
{
    public interface IOrdersRepository
    {
        Task<List<OrderedItem>> TotalOrders();
        Task<List<OrderedItem>> TotalRevenue();
        Task<List<OrderedItem>> TotalSales();
        Task<List<ShippingOrder>> GetRecentOrders();
        Task<List<ShippingOrder>> GetAllOrders();
        Task<ShippingOrder> GetOrderById(Guid orderId);
        Task UpdateOrderStatusAsync(Guid orderId, string newStatus);
        Task<Dictionary<string, int>> GetOrderCountsByStatusAsync();
        Task<object> GetOrdersCountByMonthForLast12MonthsAsync();
        Task<object> GetOrdersCountForLastSevenDaysAsync();
        Task<int> GetTotalOrdersCountForTodayAsync();
        Task<List<object>> GetProductCountsByBrandAsync();
        Task<double> GetAverageReturnRateAsync();
    }
}
