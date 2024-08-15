using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Services.Implementations;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MehndiAppDotNerCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalController : ControllerBase
    {
        private readonly IProfessionalService _professionalService;
        private readonly IConfiguration _configuration;
        public ProfessionalController(IProfessionalService professionalService, IConfiguration configuration)
        {
            _professionalService = professionalService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> AddProfessional([FromBody] Professional professional)
        {
            if (professional == null)
            {
                return BadRequest("Professional data is required.");
            }

            try
            {
                await _professionalService.AddProfessional(professional);
                return Ok("Professional inserted successfully.");
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> ProfessionalLogin([FromBody] ProfessionalLoginRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Professional login data is required.");
            }

            try
            {
                var result= await _professionalService.ProfessionalLogin(loginRequest);
                
                if(result.ToString() == "Login successful")
                {
                    var token = GenerateJwtToken(loginRequest.Email);
                    return Ok(new { Token = token });
                }
                else
                 return Unauthorized("Invalid credentialsa.");
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GenerateJwtToken(string email)
        {   var jwtKey = _configuration["Jwt:Key"]?? throw new InvalidOperationException("JWT Key not found in configuration.");
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
