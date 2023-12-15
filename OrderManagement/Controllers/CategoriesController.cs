using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.Service.Interface;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService categoriesService;
        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;  
        }
        [HttpGet("categories-count")]
        public async Task<IActionResult> GetCategoriesCount()
        {
            try
            {
               var categoriesCount = await categoriesService.TotalCategoriesCountAsync();
                return Ok(categoriesCount);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
            }
        }
        [HttpGet("all-categories-names")]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await categoriesService.GetAllCategoriesAsync();

                var categoryResponse = new List<object>();
                foreach (var category in categories)
                {
                    categoryResponse.Add(new
                    {
                        CategoryId = category.CategoryId,
                        CategoryName = category.CategoryName
                    });
                }

                return Ok(categoryResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(Guid categoryId)
        {
            try
            {
                var products = await categoriesService.GetProductsByCategoryIdAsync(categoryId);

                if (products == null || !products.Any())
                {
                    return NoContent();
                }

                var response = products.Select(p => new
                {
                    ProductItemName = p.ProductItemName,
                    QtyInStock = p.QtyInStock,
                    Price = p.Price,
                    Brand = p.Product.Brand.BrandName,
                    CategoryName = p.Product.Category.CategoryName
                });

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
