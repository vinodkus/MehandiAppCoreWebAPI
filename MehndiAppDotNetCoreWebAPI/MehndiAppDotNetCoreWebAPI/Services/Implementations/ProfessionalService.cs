using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNerCoreWebAPI.Services.Interfaces;
using MehndiAppDotNetCoreWebAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MehndiAppDotNerCoreWebAPI.Services.Implementations
{
    public class ProfessionalService : IProfessionalService
    {
        private readonly IProfessionalRepository _professionalRepository;

        public ProfessionalService(IProfessionalRepository professionalRepository)
        {
            _professionalRepository = professionalRepository;
        }

        public async Task<int> SignupProfessional(Professional professional)
        {
            return await _professionalRepository.SignupProfessional(professional);
        }

        public async Task<SqlDataReader> LoginProfessional(LoginProfessionalRequest LoginProfessionalRequest)
        {
            return await _professionalRepository.LoginProfessional(LoginProfessionalRequest); 
        }

        public async Task<int> AddMehndiDesign(MehndiDesignRequest designRequest)
        {
            return await _professionalRepository.AddMehndiDesign(designRequest);

        }
    }
}
