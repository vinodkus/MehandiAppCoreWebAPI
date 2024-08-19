using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Interfaces
{
    public interface IProfessionalRepository
    {
        Task<int> SignupProfessional(Professional professional);
        Task<SqlDataReader> LoginProfessional(LoginProfessionalRequest LoginProfessionalRequest);
        Task<int> AddMehndiDesign(MehndiDesignRequest designRequest);




    }
}
