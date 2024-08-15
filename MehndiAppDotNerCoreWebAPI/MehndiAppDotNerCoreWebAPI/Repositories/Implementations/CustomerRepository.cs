using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Implementations
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly SqlHelper _sqlHelper;

        public CustomerRepository(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        
        public async Task<int> AddCustomer(Customer customer)
        {
            // Convert the customer object to JSON
            string customerJson = JsonConvert.SerializeObject(customer);

            // Define the stored procedure name
            string procedureName = "MH_InsertCustomer";

            // Create SQL parameters
            SqlParameter[] parameters = new SqlParameter[]
            {
            new SqlParameter("@InputJson", customerJson)
            };

            try
            {
                // Execute the stored procedure
                int x= await _sqlHelper.ExecuteNonQuery(procedureName, parameters);
                return x;
            }
            catch (SqlException ex)
            {
                // Log the error and handle it as needed
                HandleError(ex, customerJson);
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
