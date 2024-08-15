using MehndiAppDotNerCoreWebAPI.Models;
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

        public async Task<int> AddCustomer(Customer customer)
        {
            return await _customerRepository.AddCustomer(customer);            
        }
    }
}
