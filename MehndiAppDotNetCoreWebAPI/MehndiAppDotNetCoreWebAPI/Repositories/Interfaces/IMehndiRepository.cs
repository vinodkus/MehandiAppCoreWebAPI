using MehndiAppDotNetCoreWebAPI.Models;

namespace MehndiAppDotNetCoreWebAPI.Repositories.Interfaces
{
    public interface IMehndiRepository
    {
        Task<int> AddMehndiDesign(MehndiDesignRequest designRequest);

    }
}
