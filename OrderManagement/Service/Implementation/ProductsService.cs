using OfficeOpenXml;
using OrderManagement.Models.Domain;
using OrderManagement.Models.Dto;
using OrderManagement.Repository.Interface;
using OrderManagement.Service.Interface;

namespace OrderManagement.Service.Implementation
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository productsRepository;
        public ProductsService(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<int> GetAllProductsCountAsync()
        {
            List<ProductItemDetail> productItemDetails = await productsRepository.GetAllProductsCount();
            return productItemDetails.Count;
        }

        public async Task<List<ProductItemDetail>> GetTopThreeLeastSellingProductsAsync()
        {
           return await productsRepository.GetTopThreeLeastSellingProductsAsync();
        }

        public async Task<List<ProductItemDetail>> GetTopThreeLowStockProductsAsync()
        {
            return await productsRepository.GetTopThreeLowStockProducts();
        }

        public async Task<List<ProductItemDetail>> GetTopThreeMostSellingProductsAsync()
        {
            return await productsRepository.GetTopThreeMostSellingProductsAsync();
        }

        public async Task UpdateDatabaseFromExcel()
        {
            try
            {
                string filePath = @"C:\Users\a98016115\OneDrive - ONEVIRTUALOFFICE\productItemDetails.xlsx"; 
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File not found.");
                }

                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    List<ProductItemDetailDTO> excelData = new List<ProductItemDetailDTO>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        ProductItemDetailDTO item = new ProductItemDetailDTO
                        {
                            ProductItemId = Guid.Parse(worksheet.Cells[row, 1].Value.ToString()),
                            QtyInStock = Convert.ToInt64(worksheet.Cells[row, 2].Value),
                            Price = Convert.ToDecimal(worksheet.Cells[row, 3].Value),
                            ProductItemName = worksheet.Cells[row, 4].Value.ToString()
                            
                        };

                        excelData.Add(item);
                    }

                    await productsRepository.UpdateFromExcel(excelData);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
