using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Service.Interface;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService productsService;
        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }
        [HttpPost("sync-from-excel")]
        public async Task<IActionResult> SyncFromExcel()
        {
            try
            {
                string filePath = @"C:\Users\a98016115\OneDrive - ONEVIRTUALOFFICE\productItemDetails.xlsx";
                if (string.IsNullOrEmpty(filePath))
                {
                    return BadRequest("File path not provided.");
                }

                await productsService.UpdateDatabaseFromExcel();
                return Ok("Sync completed successfully.");
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest($"File not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
