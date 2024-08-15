using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Interfaces
{
    public interface IProfessionalRepository
    {
        Task<int> AddProfessional(Professional professional);
        Task<object> ProfessionalLogin(ProfessionalLoginRequest professionalLoginRequest);



    }
}
