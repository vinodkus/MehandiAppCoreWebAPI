using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Services.Interfaces
{
    public interface IProfessionalService
    {
        Task<int> SignupProfessional(Professional professional);
        Task<object> LoginProfessional(LoginProfessionalRequest LoginProfessionalRequest);
    }
}
