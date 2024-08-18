using MehndiAppDotNerCoreWebAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MehndiAppDotNerCoreWebAPI.Services.Interfaces
{
    public interface IProfessionalService
    {
        Task<int> SignupProfessional(Professional professional);
        Task<SqlDataReader> LoginProfessional(LoginProfessionalRequest LoginProfessionalRequest);

    }
}
