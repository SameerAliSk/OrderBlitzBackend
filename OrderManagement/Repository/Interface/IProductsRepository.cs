using OrderManagement.Models.Dto;

namespace OrderManagement.Repository.Interface
{
    public interface IProductsRepository
    {
        Task UpdateFromExcel(List<ProductItemDetailDTO> excelData);
    }
}
