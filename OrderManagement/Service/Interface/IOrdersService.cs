﻿using OrderManagement.Models.Dto;

namespace OrderManagement.Service.Interface
{
    public interface IOrdersService
    {
        Task<int> GetTotalOrdersAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task <long> GetTotalSalesAsync();
        Task<List<OrderInformationDto>> GetRecentOrdersAsync();
        Task<List<OrderInformationDto>> GetAllOrdersAsync();
        Task<OrderInformationDto> GetOrderByIdAsync(Guid orderId);
        Task UpdateOrderStatusAsync(Guid orderId, string newStatus);
        Task<Dictionary<string, int>> GetOrderCountsByStatusAsync();
        Task<object> GetOrdersCountByMonthForLast12MonthsAsync();
        Task<object> GetOrdersCountForLastSevenDaysAsync();
        Task<int> GetTotalOrdersCountForTodayAsync();
        Task<List<object>> GetProductCountsByBrandAsync();
        Task<double> GetAverageReturnRateAsync();
    }
}
