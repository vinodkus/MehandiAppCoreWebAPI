using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNetCoreWebAPI.Services.Interfaces;

namespace MehndiAppDotNetCoreWebAPI.Services.Implementations
{
    public class MehndiService: IMehndiService
    {
        private readonly IMehndiRepository _mehndiRepository;
        public MehndiService(IMehndiRepository mehndiRepository)
        {
            _mehndiRepository = mehndiRepository;
        }
        public async Task<int> AddMehndiDesign(MehndiDesignRequest designRequest)
        {
            return await _mehndiRepository.AddMehndiDesign(designRequest);
        }
        public async Task<int> DeleteDesign(int DesignId)
        {
            return await _mehndiRepository.DeleteDesign(DesignId);
        }
        public async Task<IEnumerable<MehndiDesign>> GetDesigns(int professionalID)
        {
            return await _mehndiRepository.GetDesigns(professionalID);
        }

        public async Task<int> UpdateMehndiDesign(MehndiDesignRequest designRequest)
        {
            return await _mehndiRepository.UpdateMehndiDesign(designRequest);
        }
        // Mehndi Service Methods
        public async Task<int> AddService(MhService serviceRequest)
        {
            // Call to the repository to add a new service
            return await _mehndiRepository.AddService(serviceRequest);
        }

        public async Task<int> UpdateService(MhService serviceRequest)
        {
            // Call to the repository to update the service
            return await _mehndiRepository.UpdateService(serviceRequest);
        }
        public async Task<bool> DeleteService(int serviceID)
        {
            // Call to the repository to delete a service
            return await _mehndiRepository.DeleteService(serviceID);
        }

        public async Task<IEnumerable<MhService>> GetServices(int professionalID)
        {
            // Call to the repository to fetch the list of services by ProfessionalID
            return await _mehndiRepository.GetServices(professionalID);
        }
    }
}
