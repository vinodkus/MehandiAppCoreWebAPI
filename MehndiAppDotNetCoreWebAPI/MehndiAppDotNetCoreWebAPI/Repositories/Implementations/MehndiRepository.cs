using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNetCoreWebAPI.Models;
using MehndiAppDotNetCoreWebAPI.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

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
            var designJson = JsonConvert.SerializeObject(new
            {
                designRequest.ProfessionalID,
                designRequest.DesignName,
                designRequest.DesignDescription,
                designRequest.Mode,
                ImagePath = "" // We'll update this after uploading the image
            });

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
                int designID = Convert.ToInt32(result); // Explicitly cast to int

                // Upload image to a folder
                if (designRequest.Image != null && designRequest.Image.Length > 0)
                {
                    var imagePath = Path.Combine("Uploads", "MehndiDesigns", $"{designID}.jpg");
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await designRequest.Image.CopyToAsync(stream);
                    }

                    // Update the ImagePath in the database
                    string updateProcedure = "MH_AddMehndiDesign";
                    designRequest.Mode = 2;
                    designRequest.DesignID = designID;
                    designJson = JsonConvert.SerializeObject(new
                    {
                        designRequest.DesignID,
                        designRequest.ProfessionalID,
                        designRequest.DesignName,
                        designRequest.DesignDescription,
                        designRequest.Mode,
                        ImagePath = imagePath // We'll update this after uploading the image
                    });
                    var updateParameters = new SqlParameter[]
                    {
                        new SqlParameter("@InputJson", designJson)
                    };
                    //var updateParameters = new SqlParameter[]
                    //{
                    //    new SqlParameter("@ImagePath", imagePath),
                    //    new SqlParameter("@DesignID", designID),
                    //    new SqlParameter("@Mode", 2)
                    //};
                    await _sqlHelper.ExecuteNonQueryAsync(updateProcedure, updateParameters);
                }

                return designID;
            }
            catch (SqlException ex)
            {
                HandleError(ex, designJson);
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
