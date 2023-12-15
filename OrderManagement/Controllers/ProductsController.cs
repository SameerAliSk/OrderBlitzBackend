using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Models.Domain;
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
        [HttpGet("products-count")]
        public async Task<IActionResult> GetAllProductsCountAsync()
        {
            try
            {
                var ProductsCount = await productsService.GetAllProductsCountAsync();
                return Ok(ProductsCount);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("low-stock-products")]
        public async Task<IActionResult> GetLowStockProducts()
        {
            try
            {
                var lowStockProducts = await productsService.GetTopThreeLowStockProductsAsync();

                if (lowStockProducts.Any())
                {
                    return Ok(lowStockProducts.Select(p => new { ProductItemImage = p.ProductItemImage.Split(',')[0], p.ProductItemName, p.QtyInStock }));
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("top-three-most-selling-products")]
        public async Task<IActionResult> GetTopThreeMostSellingProducts()
        {
            try
            {
                var topThreeMostSellingProducts = await productsService.GetTopThreeMostSellingProductsAsync();

                if (topThreeMostSellingProducts.Any())
                {
                    var productDetails = topThreeMostSellingProducts.Select(p => new
                    {
                        ProductItemImage = p.ProductItemImage.Split(',')[0],
                        ProductItemName = p.ProductItemName,
                        Price = p.Price
                    });

                    return Ok(productDetails);
                }
                else
                {
                    return NoContent(); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpGet("top-three-least-selling-products")]
        public async Task<IActionResult> GetTopThreeLeastSellingProducts()
        {
            try
            {
                var topThreeLeastSellingProducts = await productsService.GetTopThreeLeastSellingProductsAsync();

                if (topThreeLeastSellingProducts.Any())
                {
                    var productDetails = topThreeLeastSellingProducts.Select(p => new
                    {
                        ProductItemImage = p.ProductItemImage.Split(',')[0],
                        ProductItemName = p.ProductItemName,
                        Price = p.Price
                    });

                    return Ok(productDetails);
                }
                else
                {
                    return NoContent(); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
    }
}
