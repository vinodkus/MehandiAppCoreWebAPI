using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Repositories.Interfaces;
using MehndiAppDotNetCoreWebAPI.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MehndiAppDotNetCoreWebAPI.Repositories.Implementations
{
    public class MehndiRepository:IMehndiRepository
    {
        private readonly SqlHelper _sqlHelper;

        public MehndiRepository(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;

        }
        public async Task<int> AddMehndiDesign(MehndiDesignRequest designRequest)
        {
            // Convert to JSON            
            var designJson = JsonConvert.SerializeObject(designRequest);

            // Define the stored procedure name
            string procedureName = "MH_AddMehndiDesign";

            // Create SQL parameters
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InputJson", designJson)
            };
            try
            {
                // Execute the stored procedure and get the DesignID
                var result = await _sqlHelper.ExecuteScalarAsync(procedureName, parameters);
                if (result != null)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException ex)
            {
                HandleError(ex, designJson);
                throw;
            }
        }

        // Mehndi Service Methods
        public async Task<int> AddService(MhService serviceRequest)
        {
            var serviceJson = JsonConvert.SerializeObject(serviceRequest);
            //string procedureName = "MH_AddService";
            string procedureName = "MH_CUDService";            

            SqlParameter[] parameters = {
            new SqlParameter("@InputJson", serviceJson)
        };

            var result = await _sqlHelper.ExecuteScalarAsync(procedureName, parameters);
            return Convert.ToInt32(result);
        }

       

        public async Task<int> DeleteDesign(int designId)
        {
            MehndiDesignRequest designRequest=new MehndiDesignRequest();
            designRequest.DesignID= designId;
            // Convert to JSON            
            var designJson = JsonConvert.SerializeObject(designRequest);
            // Define the stored procedure name
            string procedureName = "MH_DeleteMehndiDesign";
            // Create SQL parameters
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InputJson", designJson)
            };
            try
            {
                // Execute the stored procedure and get the DesignID
                var result = await _sqlHelper.ExecuteNonQueryAsync(procedureName, parameters);
                if (result != null)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException ex)
            {
                HandleError(ex, designJson);
                throw;
            }
        }

        //public async Task<bool> DeleteService(int serviceID)
        //{
        //    string procedureName = "MH_DeleteService";

        //    SqlParameter[] parameters = {
        //    new SqlParameter("@ServiceID", serviceID)
        //    };

        //    var result = await _sqlHelper.ExecuteScalarAsync(procedureName, parameters);

        //    // Convert the result to a boolean (1 -> true, 0 -> false)
        //    return Convert.ToInt32(result) == 1;
        //}
        public async Task<int> DeleteService(MhService serviceRequest)
        {
            var serviceJson = JsonConvert.SerializeObject(serviceRequest);
            //string procedureName = "MH_DeleteService";
            string procedureName = "MH_CUDService";

            

            SqlParameter[] parameters = {
            new SqlParameter("@InputJson", serviceJson)
            };

            var result = await _sqlHelper.ExecuteScalarAsync(procedureName, parameters);
            return Convert.ToInt32(result);  // Expecting an integer result from the stored procedure
        }

        public async Task<IEnumerable<MehndiDesign>> GetDesigns(int professionalID)
        {
            MehndiDesignRequest mehndiDesignRequest = new MehndiDesignRequest();
            mehndiDesignRequest.ProfessionalID = Convert.ToInt32( professionalID);

            // Convert to JSON            
            var designJson = JsonConvert.SerializeObject(mehndiDesignRequest);

            // Define the stored procedure name
            string procedureName = "MH_ShowMehndiDesign";

            // Create SQL parameters
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InputJson", designJson)
            };
            //var designs = await _sqlHelper.ExecuteReaderAsync<MehndiDesign>(procedureName, parameters);
            //return designs;
            try
            {
                var designs = await _sqlHelper.ExecuteReaderAsync<MehndiDesign>(procedureName, parameters);
                return designs;
            }
            catch (Exception ex)
            {
                // HandleError(ex, designJson);
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }

        //public async Task<IEnumerable<MehndiService>> GetServices(int professionalID)
        //{
        //    string procedureName = "MH_GetServices";

        //    SqlParameter[] parameters = {
        //        new SqlParameter("@ProfessionalID", professionalID)
        //    };

        //    return await _sqlHelper.ExecuteReaderAsync<MehndiService>(procedureName, parameters);
        //}
        public async Task<IEnumerable<MhService>> GetServices(int professionalID)
        {
            MehndiServiceRequest request = new MehndiServiceRequest
            {
                ProfessionalID = professionalID
            };
            var serviceJson = JsonConvert.SerializeObject(request);
            string procedureName = "MH_GetServices";

            SqlParameter[] parameters = {
            new SqlParameter("@InputJson", serviceJson)
            };

            return await _sqlHelper.ExecuteReaderAsync<MhService>(procedureName, parameters);
        }

        public async Task<int> UpdateMehndiDesign(MehndiDesignRequest designRequest)
        {

            // Convert to JSON            
            var designJson = JsonConvert.SerializeObject(designRequest);

            // Define the stored procedure name
            string procedureName = "MH_AddMehndiDesign";

            // Create SQL parameters
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@InputJson", designJson)
            };
            try
            {
                // Execute the stored procedure and get the DesignID
                var result = await _sqlHelper.ExecuteScalarAsync(procedureName, parameters);
                if (result != null)
                {
                    return 1;
                }
                return 0;
            }
            catch (SqlException ex)
            {
                HandleError(ex, designJson);
                throw;
            }
        }

        public async Task<int> UpdateService(MhService serviceRequest)
        {
            var serviceJson = JsonConvert.SerializeObject(serviceRequest);
            //string procedureName = "MH_UpdateService";
            string procedureName = "MH_CUDService";

            

            SqlParameter[] parameters = {
            new SqlParameter("@InputJson", serviceJson)
            };

            var result = await _sqlHelper.ExecuteScalarAsync(procedureName, parameters);
            return Convert.ToInt32(result);  // Expecting an integer result from the stored procedure
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
