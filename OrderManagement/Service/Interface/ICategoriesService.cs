using OrderManagement.Models.Domain;

namespace OrderManagement.Service.Interface
{
    public interface ICategoriesService
    {
        Task<int> TotalCategoriesCountAsync();
        Task<List<Category>> GetAllCategoriesAsync();
        Task<List<ProductItemDetail>> GetProductsByCategoryIdAsync(Guid categoryId);
    }
}
