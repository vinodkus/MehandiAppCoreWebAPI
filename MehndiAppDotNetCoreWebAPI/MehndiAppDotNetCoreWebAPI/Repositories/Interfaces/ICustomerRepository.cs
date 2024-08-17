using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<int> SignupCustomer(Customer customer);
        Task<object> LoginCustomer(CustomerLoginRequest customerLoginRequest);

    }
}
