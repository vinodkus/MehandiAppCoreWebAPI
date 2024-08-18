using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNetCoreWebAPI.Response
{
    public class LoginResponse
    {
        public string Message { get; set; } = "";
        public Professional? ProfessionalDetails { get; set; } 
    }
}
