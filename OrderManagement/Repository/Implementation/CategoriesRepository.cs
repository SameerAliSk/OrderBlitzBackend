using Microsoft.EntityFrameworkCore;
using OrderManagement.Context;
using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;

namespace OrderManagement.Repository.Implementation
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly EcommerceContext dbContext;
        public CategoriesRepository(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<List<ProductItemDetail>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await dbContext.ProductItemDetails
                 .Include(p => p.Product)
                     .ThenInclude(p => p.Brand)
                 .Include(p => p.Product)
                     .ThenInclude(p => p.Category)
                 .Where(p => p.Product.CategoryId == categoryId)
                 .ToListAsync();
        }

        public async Task<List<Category>> TotalCategoriesCount()
        {
            return await dbContext.Categories.ToListAsync();
        }
    }
}
