using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<int> SignupCustomer(Customer customer);
        Task<object> LoginCustomer(CustomerLoginRequest customerLoginRequest);

    }
}
