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
    }
}
