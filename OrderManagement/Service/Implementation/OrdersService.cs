using OrderManagement.Models.Domain;
using OrderManagement.Models.Dto;
using OrderManagement.Repository.Implementation;
using OrderManagement.Repository.Interface;
using OrderManagement.Service.Interface;

namespace OrderManagement.Service.Implementation
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrdersRepository ordersRepository;
        public OrdersService(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task<List<OrderInformationDto>> GetAllOrdersAsync()
        { 
            List<OrderInformationDto> orderdInfo  =  new List<OrderInformationDto>();
            var allOrders =await  ordersRepository.GetAllOrders();
            foreach (var order in allOrders)
            {
                OrderInformationDto orderInformationDto = new OrderInformationDto
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate.ToString("dd-MM-yyyy"),
                    ExpectedDeliveryDate = order.OrderDate.AddDays(7).ToString("dd-MM-yyyy"),
                    OrderStatus = order.OrderStatus,
                    DeliveryAddress = order.ShippingAddress,
                    TotalOrderAmount = order.OrderedItems.Sum(item => item.Quantity * item.Price),
                    Products = order.OrderedItems.Select(item =>
                        new ProductDto
                        {
                            Name = item.ProductItem.ProductItemName,
                            Quantity = item.Quantity
                        }
                    ).ToList()
                };
                orderdInfo.Add(orderInformationDto);
            }
            return orderdInfo;
        }

        public async Task<OrderInformationDto> GetOrderByIdAsync(Guid orderId)
        {
            var order = await ordersRepository.GetOrderById(orderId);

            if (order == null)
            {
                return null;
            }
            OrderInformationDto orderInformationDto = new OrderInformationDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate.ToString("dd-MM-yyyy"),
                ExpectedDeliveryDate = order.OrderDate.AddDays(7).ToString("dd-MM-yyyy"),
                OrderStatus = order.OrderStatus,
                DeliveryAddress = order.ShippingAddress,
                TotalOrderAmount = order.OrderedItems.Sum(item => item.Quantity * item.Price),
                Products = order.OrderedItems.Select(item =>
                    new ProductDto
                    {
                        Name = item.ProductItem.ProductItemName,
                        Quantity = item.Quantity
                    }
                ).ToList()
            };

            return orderInformationDto;
        }

        public async Task<List<OrderInformationDto>> GetRecentOrdersAsync()
        {
            List<OrderInformationDto> orderInformationList = new List<OrderInformationDto>();

            var recentOrders = await ordersRepository.GetRecentOrders();
            foreach (var order in recentOrders)
            {
                OrderInformationDto orderInfo = new OrderInformationDto
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate.ToString("dd-MM-yyyy"),
                    ExpectedDeliveryDate = order.OrderDate.AddDays(7).ToString("dd-MM-yyyy"), 
                    OrderStatus = order.OrderStatus,
                    DeliveryAddress = order.ShippingAddress,
                    TotalOrderAmount = order.OrderedItems.Sum(item => item.Quantity * item.Price),
                    Products = order.OrderedItems.Select(item =>
                        new ProductDto
                        {
                            Name = item.ProductItem.ProductItemName,
                            Quantity = item.Quantity
                        }
                    ).ToList()
                };

                orderInformationList.Add(orderInfo);
            }

            return orderInformationList;

        }

        public async Task<int> GetTotalOrdersAsync()
        {
            List<OrderedItem> orders = await ordersRepository.TotalOrders();
            int distinctOrdersCount = orders.Select(o => o.OrderId).Distinct().Count();
            return distinctOrdersCount;
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            List<OrderedItem> orders = await ordersRepository.TotalRevenue();
            decimal totalRevenue = orders.Sum(o => o.Quantity * o.Price);
            return totalRevenue;
        }

        public async Task<long> GetTotalSalesAsync()
        {
            List<OrderedItem> orders = await ordersRepository.TotalSales();
            long totalSales = orders.Sum(o => o.Quantity);
            return totalSales;
        }

       
        public async Task<Dictionary<string, int>> GetOrderCountsByStatusAsync()
        {
            var orderStatusCounts = await ordersRepository.GetOrderCountsByStatusAsync();
            return orderStatusCounts;
        }

        public async Task UpdateOrderStatusAsync(Guid orderId, string newStatus)
        {
            await ordersRepository.UpdateOrderStatusAsync(orderId, newStatus);
        }
    }
}
