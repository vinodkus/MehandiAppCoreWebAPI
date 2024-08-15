using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<int> AddCustomer(Customer customer);
    }
}
