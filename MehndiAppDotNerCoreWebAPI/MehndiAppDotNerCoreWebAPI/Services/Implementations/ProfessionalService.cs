using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;

namespace MehndiAppDotNerCoreWebAPI.Services.Implementations
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;

        public ProfessionalService(IProfessionalRepository professionalRepository)
        {
            _professionalRepository = professionalRepository;
        }

        public async Task<int> AddProfessional(Professional professional)
        {
            return await _professionalRepository.AddProfessional(professional);
        }

        public async Task<object> ProfessionalLogin(ProfessionalLoginRequest professionalLoginRequest)
        {
            //var result= await _professionalRepository.ProfessionalLogin(professionalLoginRequest);
            //return result;
            return await _professionalRepository.ProfessionalLogin(professionalLoginRequest); ;
        }
    }
}
