using OrderManagement.Models.Domain;

namespace OrderManagement.Repository.Interface
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> TotalCategoriesCount();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<ProductItemDetail>> GetProductsByCategoryIdAsync(Guid categoryId);
    }
}
