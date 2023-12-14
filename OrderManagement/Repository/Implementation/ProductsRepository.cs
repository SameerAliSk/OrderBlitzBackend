using OrderManagement.Context;
using OrderManagement.Models.Dto;
using OrderManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Models.Domain;

namespace OrderManagement.Repository.Implementation
{
    public class ProductsRepository:IProductsRepository
    {
        private readonly EcommerceContext dbContext;
        public ProductsRepository(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ProductItemDetail>> GetAllProductsCount()
        {
            return await dbContext.ProductItemDetails.ToListAsync();
        }

        public async Task<List<ProductItemDetail>> GetTopThreeLeastSellingProductsAsync()
        {
            return await dbContext.ProductItemDetails
                .OrderBy(p => p.OrderedItems.Sum(o => o.Quantity))
                .Take(3)
                .ToListAsync();
        }

        public async Task<List<ProductItemDetail>> GetTopThreeLowStockProducts()
        {
            return await dbContext.ProductItemDetails.Where(p => p.QtyInStock < 10).OrderBy(p=>p.QtyInStock).Take(3).ToListAsync();
        }

        public async Task<List<ProductItemDetail>> GetTopThreeMostSellingProductsAsync()
        {
            return await dbContext.ProductItemDetails
                .OrderByDescending(p => p.OrderedItems.Sum(o => o.Quantity))
                .Take(3)
                .ToListAsync();
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
