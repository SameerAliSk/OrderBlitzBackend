using Microsoft.EntityFrameworkCore;
using OrderManagement.Context;
using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;

namespace OrderManagement.Repository.Implementation
{
    public class BrandsRepository : IBrandsRepository
    {
        private readonly EcommerceContext dbContext;
        public BrandsRepository(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Brand>> GetAllBrandsCount()
        {
           return await dbContext.Brands.ToListAsync();
        }
    }
}
