using OrderManagement.Models.Domain;
using OrderManagement.Repository.Interface;
using OrderManagement.Service.Interface;

namespace OrderManagement.Service.Implementation
{
    public class BrandsService : IBrandsService
    {
        private readonly IBrandsRepository brandsRepository;
        public BrandsService(IBrandsRepository brandsRepository)
        {
            this.brandsRepository = brandsRepository;
        }
        public async Task<int> GetAllBrandsCountAsync()
        {
             List<Brand> BrandsCount = await brandsRepository.GetAllBrandsCount();
            return BrandsCount.Count();
        }
    }
}
