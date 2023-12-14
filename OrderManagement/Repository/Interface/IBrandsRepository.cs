using OrderManagement.Models.Domain;

namespace OrderManagement.Repository.Interface
{
    public interface IBrandsRepository
    {
        Task<List<Brand>> GetAllBrandsCount();
    }
}
