using MehndiAppDotNerCoreWebAPI.Models;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Interfaces
{
    public interface IProfessionalRepository
    {
        Task<int> SignupProfessional(Professional professional);
        Task<object> LoginProfessional(LoginProfessionalRequest LoginProfessionalRequest);



    }
}
