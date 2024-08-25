using MehndiAppDotNetCoreWebAPI.Models;

namespace MehndiAppDotNetCoreWebAPI.Services.Interfaces
{
    public interface IMehndiService
    {
        Task<int> AddMehndiDesign(MehndiDesignRequest designRequest);

    }
}
