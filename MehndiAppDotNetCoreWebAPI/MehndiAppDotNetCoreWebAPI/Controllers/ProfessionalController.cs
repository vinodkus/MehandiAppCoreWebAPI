using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Services.Implementations;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Response;
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
        [HttpPost("SignupProfessional")]
        public async Task<IActionResult> SignupProfessional([FromBody] Professional professional)
        {
            if (professional == null)
            {
                return BadRequest("Professional data is required.");
            }

            try
            {
                await _professionalService.SignupProfessional(professional);
                return Ok(new { message = "Professional inserted successfully." });
            }
            catch (Exception ex)
            {
                // Log the error if necessary
                return StatusCode(500, new { message = $"{ex.Message}" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginProfessional([FromBody] LoginProfessionalRequest loginRequest)
        {
            if (loginRequest == null)
            {
                return BadRequest("Professional login data is required.");
            }

            try
            {
                var result = await _professionalService.LoginProfessional(loginRequest);

                //if (result is ProfessionalDetails professionalDetails)
                if (result.HasRows)
                {
                    LoginResponse loginResponse = new LoginResponse();
                    Professional professional = new Professional();
                    while(result.Read())
                    {
                        loginResponse.Message = "Professional Login Successfull";
                        professional.FullName= Convert.ToString(result["FullName"]) ?? "";
                        professional.ProfessionalID = Convert.ToDecimal(result["ProfessionalID"]);
                        professional.Email = Convert.ToString(result["Email"]) ?? "";
                        professional.PhoneNumber = Convert.ToString(result["PhoneNumber"]) ?? "";
                        professional.Address = Convert.ToString(result["Address"]) ?? "";
                        professional.ExperienceYears = Convert.ToInt32(result["ExperienceYears"]);
                        professional.Specialization = Convert.ToString(result["Specialization"]) ?? "";
                        loginResponse.ProfessionalDetails = professional;
                    }
                    var token = GenerateJwtToken(loginRequest.Email);

                    return Ok(new
                    {
                        Token = token,
                        LoginResponse = loginResponse
                    });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid credentials." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}" });
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

        //[HttpPost("AddMehndiDesign")]
        //public async Task<IActionResult> AddMehndiDesign([FromForm] MehndiDesignRequest designRequest)
        //{
        //    if (designRequest == null)
        //    {
        //        return BadRequest("Design data is required.");
        //    }

        //    try
        //    {
        //        var designID = await _professionalService.AddMehndiDesign(designRequest);
        //        return Ok(new { DesignID = designID, message = "Mehndi design added successfully." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
        //    }
        //}

    }
}
