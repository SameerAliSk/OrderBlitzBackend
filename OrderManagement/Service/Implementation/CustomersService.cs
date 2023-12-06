using OrderManagement.Models.Domain;
using OrderManagement.Repository.Implementation;
using OrderManagement.Repository.Interface;
using OrderManagement.Service.Interface;

namespace OrderManagement.Service.Implementation
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository customersRepository;
        public CustomersService(ICustomersRepository customersRepository)
        {
            this.customersRepository = customersRepository;
        }
        public async Task<int> GetAllCustomersAsync()
        {
            List<CustomerCredential> customers = await customersRepository.GetAllCustomers();
            return customers.Count;
        }
    }
}
