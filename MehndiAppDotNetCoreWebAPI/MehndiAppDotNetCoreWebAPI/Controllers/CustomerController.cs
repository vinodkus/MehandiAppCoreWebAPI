using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Services.Implementations;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MehndiAppDotNerCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;


        public CustomerController(ICustomerService customerService, IConfiguration configuration)
        {
            _customerService = customerService;
            _configuration = configuration;
        }
        [HttpPost("SignupCustomer")]
        public async Task<IActionResult> SignupCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer data is required.");
            }

            try
            {
                await _customerService.SignupCustomer(customer);
                return Ok(new { message = "Customer inserted successfully." });
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginCustomer([FromBody] CustomerLoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Customer login data is required.");
            }

            try
            {
                var result = await _customerService.LoginCustomer(loginRequest);

                if (result.ToString() == "Login successful")
                {
                    var token = GenerateJwtToken(loginRequest.Email);
                    return Ok(new { Token = token });
                }
                else
                    return StatusCode(500, new { message = $"{"Invalid credentials."}" });
                //return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                return StatusCode(500, new { message = $"{ex.Message}" });
                //return StatusCode(500, $"Internal server error: {ex.Message}");                

            }
        }

        private string GenerateJwtToken(string email)
        {
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration.");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                // Add more claims if needed
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
