using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Services.Implementations;

namespace MehndiAppDotNetCoreWebAPI.Services.Interfaces
{
    public interface IMehndiService
    {
        Task<int> AddMehndiDesign(MehndiDesignRequest designRequest);
        Task<int> UpdateMehndiDesign(MehndiDesignRequest designRequest);

        Task<IEnumerable<MehndiDesign>> GetDesigns(int professionalID);
        Task<int> DeleteDesign(int DesignId);

        // Mehndi Service Methods
        Task<int> AddService(MhService serviceRequest);
        Task<int> UpdateService(MhService serviceRequest);
        Task<bool> DeleteService(int serviceID);
        Task<IEnumerable<MhService>> GetServices(int professionalID);

    }
}
