using OrderManagement.Models.Domain;

namespace OrderManagement.Service.Interface
{
    public interface IProductsService
    {
        Task UpdateDatabaseFromExcel(string filePath);
        Task<int> GetAllProductsCountAsync();

        Task<List<ProductItemDetail>> GetTopThreeLowStockProductsAsync();
        Task<List<ProductItemDetail>> GetTopThreeMostSellingProductsAsync();
        Task<List<ProductItemDetail>> GetTopThreeLeastSellingProductsAsync();
    }
}
