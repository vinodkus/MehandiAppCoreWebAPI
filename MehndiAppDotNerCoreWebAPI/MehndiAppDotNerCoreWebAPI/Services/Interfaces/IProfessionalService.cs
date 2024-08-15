using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Services.Interfaces
{
    public interface IProfessionalService
    {
        Task<int> AddProfessional(Professional professional);
        Task<object> ProfessionalLogin(ProfessionalLoginRequest professionalLoginRequest);
    }
}
