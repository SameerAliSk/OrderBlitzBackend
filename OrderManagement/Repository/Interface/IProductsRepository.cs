using OrderManagement.Models.Domain;
using OrderManagement.Models.Dto;

namespace OrderManagement.Repository.Interface
{
    public interface IProductsRepository
    {
        Task UpdateFromExcel(List<ProductItemDetailDTO> excelData);
        Task<List<ProductItemDetail>> GetAllProductsCount();
        Task<List<ProductItemDetail>> GetTopThreeLowStockProducts();
        Task<List<ProductItemDetail>> GetTopThreeMostSellingProductsAsync();
        Task<List<ProductItemDetail>> GetTopThreeLeastSellingProductsAsync();
    }
}
