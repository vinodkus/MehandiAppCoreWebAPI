using MehndiAppDotNerCoreWebAPI.Services.Implementations;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MehndiAppDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MehndiController : ControllerBase
    {
        private readonly IMehndiService _mehndiService;

        private readonly IConfiguration _configuration;
        public MehndiController(IMehndiService mehndiService, IConfiguration configuration)
        {
            _mehndiService= mehndiService;
            _configuration= configuration;
        }
        [HttpPost("AddMehndiDesign")]
        public async Task<IActionResult> AddMehndiDesign([FromForm] MehndiDesignRequest designRequest)
        {
            if (designRequest == null)
            {
                return BadRequest("Design data is required.");
            }
            try
            {
                var designID = await _mehndiService.AddMehndiDesign(designRequest);
                return Ok(new { DesignID = designID, message = "Mehndi design added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
