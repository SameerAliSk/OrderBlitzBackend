using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Service.Interface;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;
        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }
        [HttpGet("total-customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                int customersCount = await customersService.GetAllCustomersAsync();
                return Ok(customersCount);
            }
            catch(Exception ex) {
                return StatusCode(StatusCodes.Status500InternalServerError, (new
                {
                    ex.Message
                }));
            }
        }
    }
}
