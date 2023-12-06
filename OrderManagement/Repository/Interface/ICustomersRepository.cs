using OrderManagement.Models.Domain;

namespace OrderManagement.Repository.Interface
{
    public interface ICustomersRepository
    {
        Task <List<CustomerCredential>> GetAllCustomers();
    }
}
