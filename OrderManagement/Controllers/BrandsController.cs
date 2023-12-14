using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Service.Interface;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsService brandsService;
        public BrandsController(IBrandsService brandsService)
        {
            this.brandsService = brandsService;
        }
        [HttpGet("brands-count")]
        public async Task<IActionResult> GetAllBrandsCountAsync()
        {
            try
            {
                var brandsCount = await brandsService.GetAllBrandsCountAsync();
                return Ok(brandsCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
