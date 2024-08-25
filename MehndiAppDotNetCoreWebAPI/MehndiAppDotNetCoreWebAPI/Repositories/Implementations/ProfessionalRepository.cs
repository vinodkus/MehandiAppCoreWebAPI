using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNetCoreWebAPI.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Implementations
{
    public class ProfessionalRepository : IProfessionalRepository
    {
        private readonly SqlHelper _sqlHelper;

        public ProfessionalRepository(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public async Task<int> SignupProfessional(Professional professional)
        {
            // Convert the customer object to JSON
            string professionalJson = JsonConvert.SerializeObject(professional);

            // Define the stored procedure name
            string procedureName = "MH_SignupProfessional";

            // Create SQL parameters
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@InputJson", professionalJson)
            };

            try
            {
                // Execute the stored procedure
                return await _sqlHelper.ExecuteNonQueryAsync(procedureName, parameters);
            }
            catch (SqlException ex)
            {
                // Log the error and handle it as needed
                HandleError(ex, professionalJson);
                throw;
            }
        }

        public async Task<SqlDataReader> LoginProfessional(LoginProfessionalRequest loginProfessionalRequest)
        {
            // Convert the customer object to JSON
            var loginJson = JsonConvert.SerializeObject(loginProfessionalRequest);

            // Define the stored procedure name
            string procedureName = "MH_LoginProfessional";

            // Create SQL parameters
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InputJson", loginJson)
            };

            try
            {
                // Execute the stored procedure
                var result= await _sqlHelper.ExecuteReaderAsync(procedureName, parameters);                
                return result;
            }
            catch (SqlException ex)
            {
                // Log the error and handle it as needed
                HandleError(ex, loginJson);
                throw;
            }
        }
        


        private void HandleError(SqlException ex, string inputJson)
        {
            // You can add logic here to log the error details into another table or to a file
            // For example, you might want to log this information in a database error log table

            // This is just an example of logging to console
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("Procedure: " + ex.Procedure);
            Console.WriteLine("Line Number: " + ex.LineNumber);
            Console.WriteLine("Input JSON: " + inputJson);

            // Optionally, use the SqlHelper to log this to an error table
        }
    }
}
