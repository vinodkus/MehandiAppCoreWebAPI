using MehndiAppDotNerCoreWebAPI.Services.Implementations;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Models.DTO;
using MehndiAppDotNetCoreWebAPI.Repositories.Interfaces;
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
        private readonly IFileService _fileService;

        private readonly IConfiguration _configuration;
        public MehndiController(IMehndiService mehndiService, IConfiguration configuration, IFileService fs)
        {
            _mehndiService= mehndiService;
            _configuration= configuration;
            _fileService= fs;
        }
        [HttpPost("AddMehndiDesign")]
        public async Task<IActionResult> AddMehndiDesign([FromForm]MehndiDesignRequest designRequest)
        {
            var status = new Status();

            try
            {
                if (!ModelState.IsValid)
                {
                    status.StatusCode = 0;
                    status.Message = "Please pass the valid data";
                    return Ok(status);
                }
                if (designRequest.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(designRequest.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        designRequest.DesignImageName = fileResult.Item2;
                    }
                }
                //var mehndiResult = Convert.ToInt16(_mehndiService.AddMehndiDesign(designRequest)) >0 ? true : false;
                var mehndiResult = await _mehndiService.AddMehndiDesign(designRequest) > 0;
                if (mehndiResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Design Added Successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding design";
                }
                return Ok(status);
            }
            catch (Exception ex)
            {
                status.StatusCode = -1;
                status.Message = ex.Message;
                return Ok(ex);
            }
           
           
        }

        [HttpPost("UpdateMehndiDesign")]
        public async Task<IActionResult> UpdateMehndiDesign([FromForm] MehndiDesignRequest designRequest)
        {
            var status = new Status();

            try
            {
                if (!ModelState.IsValid)
                {
                    status.StatusCode = 0;
                    status.Message = "Please pass the valid data";
                    return Ok(status);
                }
                if (designRequest.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(designRequest.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        designRequest.DesignImageName = fileResult.Item2;
                    }
                }
                //var mehndiResult = Convert.ToInt16(_mehndiService.AddMehndiDesign(designRequest)) >0 ? true : false;
                var mehndiResult = await _mehndiService.UpdateMehndiDesign(designRequest) > 0;
                if (mehndiResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Design Updated Successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on updating design";
                }
                return Ok(status);
            }
            catch (Exception ex)
            {
                status.StatusCode = -1;
                status.Message = ex.Message;
                return Ok(ex);
            }


        }
        [HttpGet("GetDesigns")]
        public async Task<IActionResult> GetDesigns(int professionalID)
        {
            try
            {
                var designs = await _mehndiService.GetDesigns(professionalID);
                return Ok(designs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("DeleteDesign")]
        public async Task<IActionResult> DeleteDesign(int designId)
        {
            var status = new Status();

            if (designId<=0)
            {
                return BadRequest("Invalid design ID.");
            }
            try
            {
                var result = await _mehndiService.DeleteDesign(designId)>0;
                if(result)
                {
                    status.StatusCode = 1;
                    status.Message = "Design Deleted Successfully";
                }
                else
                {
                    status.StatusCode = 1;
                    status.Message = "Error on deleting design";
                }  
                return Ok(status);

            }
            catch (Exception ex)
            {
                status.StatusCode = -1;
                status.Message = ex.Message;
                return Ok(ex);
            }
        }

        [HttpGet("GetServices")]
        public async Task<IActionResult> GetServices(int professionalID)
        {

            try
            {
                var services = await _mehndiService.GetServices(professionalID);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }

           
        }

        [HttpPost("AddService")]
        public async Task<IActionResult> AddService([FromBody] MhService serviceRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            var result = await _mehndiService.AddService(serviceRequest);
            return Ok(new { message = result > 0 ? "Service added successfully" : "Failed to add service" });

        }

        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService([FromBody] MhService serviceRequest)
        {
            var result = await _mehndiService.UpdateService(serviceRequest);
            return Ok(new { message = result > 0 ? "Service updated successfully" : "Failed to update service" });
        }

        [HttpDelete("DeleteService")]
        public async Task<IActionResult> DeleteService(int serviceID)
        {
            var result = await _mehndiService.DeleteService(serviceID);
            return Ok(new { message = result ? "Service deleted successfully" : "Failed to delete service" });
        }
    }
}
