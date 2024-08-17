using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Repositories.Implementations;
using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;

namespace MehndiAppDotNerCoreWebAPI.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
       private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }    

        public async Task<int> SignupCustomer(Customer customer)
        {
            return await _customerRepository.SignupCustomer(customer);            
        }
        public async Task<object> LoginCustomer(CustomerLoginRequest customerLoginRequest)
        {
            return await _customerRepository.LoginCustomer(customerLoginRequest);
        }
    }
}
