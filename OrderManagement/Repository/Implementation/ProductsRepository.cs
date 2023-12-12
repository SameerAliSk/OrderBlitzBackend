using OrderManagement.Context;
using OrderManagement.Models.Dto;
using OrderManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace OrderManagement.Repository.Implementation
{
    public class ProductsRepository:IProductsRepository
    {
        private readonly EcommerceContext dbContext;
        public ProductsRepository(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task UpdateFromExcel(List<ProductItemDetailDTO> excelData)
        {
            try
            {
                foreach (var excelItem in excelData)
                {
                    var databaseItem = await dbContext.ProductItemDetails.FirstOrDefaultAsync(d => d.ProductItemId == excelItem.ProductItemId);

                    if (databaseItem != null)
                    {
                        databaseItem.QtyInStock = excelItem.QtyInStock;
                        databaseItem.Price = excelItem.Price;
                        databaseItem.ProductItemName = excelItem.ProductItemName;
                        databaseItem.ProductItemId = excelItem.ProductItemId;

                        await dbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
