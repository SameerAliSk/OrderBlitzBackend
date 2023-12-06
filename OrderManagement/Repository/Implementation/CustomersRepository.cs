using Microsoft.EntityFrameworkCore;
using OrderManagement.Context;
using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;

namespace OrderManagement.Repository.Implementation
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly EcommerceContext dbContext;
        public CustomersRepository(EcommerceContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<CustomerCredential>> GetAllCustomers()
        {
            return await dbContext.CustomerCredentials.ToListAsync();
        }
    }
}
