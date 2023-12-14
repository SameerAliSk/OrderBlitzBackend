using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;
using OrderManagement.Service.Interface;

namespace OrderManagement.Service.Implementation
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository categoriesRepository;
        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await categoriesRepository.GetAllCategoriesAsync();
        }

        public async Task<List<ProductItemDetail>> GetProductsByCategoryIdAsync(Guid categoryId)
        {
            return await categoriesRepository.GetProductsByCategoryIdAsync(categoryId);
        }

        public async Task<int> TotalCategoriesCountAsync()
        {
            List<Category> categories = await categoriesRepository.TotalCategoriesCount();
            return categories.Count();
        }
    }
}
