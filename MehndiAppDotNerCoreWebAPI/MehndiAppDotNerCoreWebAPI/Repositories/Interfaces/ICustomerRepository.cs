using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> AddCustomer(Customer customer);
    }
}
