using OrderManagement.Models.Domain;

namespace OrderManagement.Service.Interface
{
    public interface ICustomersService
    {
        Task<int> GetAllCustomersAsync();
    }
}
