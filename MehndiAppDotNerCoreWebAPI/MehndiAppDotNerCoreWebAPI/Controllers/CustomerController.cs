using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Services.Implementations;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MehndiAppDotNerCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

            try
            {
                await _customerService.AddCustomer(customer);
                return Ok("Customer inserted successfully.");
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
