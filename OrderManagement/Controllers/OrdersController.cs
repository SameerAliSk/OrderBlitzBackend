using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models.Dto;
using OrderManagement.Service.Implementation;
using OrderManagement.Service.Interface;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;
        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }
        [HttpGet("total-orders")]
        public async Task<IActionResult> GetTotalOrders() {
            try { 
            int totalOrdes = await ordersService.GetTotalOrdersAsync();
            return Ok(totalOrdes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("total-revenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            try
            {
                decimal totalRevenue = await ordersService.GetTotalRevenueAsync();
                return Ok(totalRevenue);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("total-sales")]
        public async Task<IActionResult> GetTotalSales()
        {
            try
            {
                long totalSales = await ordersService.GetTotalSalesAsync();
                return Ok(totalSales);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("recent-orders")]
        public async Task<IActionResult> GetRecentOrders()
        {
            try
            {
                var recentOrders = await ordersService.GetRecentOrdersAsync();
                return Ok(recentOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var allOrders = await ordersService.GetAllOrdersAsync();
                return Ok(allOrders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            try
            {
                var orderInfo = await ordersService.GetOrderByIdAsync(orderId);

                if (orderInfo == null)
                {
                    return NotFound();
                }

                return Ok(orderInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ex.Message
                });
            }
        }
        [HttpPost("update-order-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusDto updateOrderStatusDto)
        {
            try
            {
                if (updateOrderStatusDto == null || string.IsNullOrWhiteSpace(updateOrderStatusDto.OrderStatus))
                {
                    return BadRequest("Invalid request body");
                }

                await ordersService.UpdateOrderStatusAsync(orderId, updateOrderStatusDto.OrderStatus);
                return Ok("Order status updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        [HttpGet("order-counts")]
        public async Task<IActionResult> GetOrderCountsByStatus()
        {
            try
            {
                var orderStatusCounts = await ordersService.GetOrderCountsByStatusAsync();
                return Ok(orderStatusCounts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

    }
}
