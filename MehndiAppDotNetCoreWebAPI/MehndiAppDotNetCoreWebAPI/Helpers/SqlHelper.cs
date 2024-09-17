using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MehndiAppDotNerCoreWebAPI.Helpers
{
    public class SqlHelper
    {
        private readonly string _connectionString;

        public SqlHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public async Task<int> ExecuteNonQueryAsync(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                        command.Parameters.AddRange(parameters);

                    connection.Open();
                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task<object> ExecuteScalarAsync(string query, SqlParameter[] parameters = null)
        {
            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    await connection.OpenAsync();
                    var result = await command.ExecuteScalarAsync();
                    return result ?? throw new InvalidOperationException("ExecuteScalarAsync returned null.");
                }
            }
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(string procedureName, SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            try
            {
                await connection.OpenAsync();
                // Execute the command and return the SqlDataReader
                SqlDataReader reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (SqlException ex)
            {
                // Log or handle the error as needed
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string query, SqlParameter[] parameters = null)
        {
            var resultList = new List<T>();

            using (var connection = GetConnection())
            {
                using (var command = new SqlCommand(query, connection))
                {
                    //command.CommandType = CommandType.Text;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    await connection.OpenAsync();
                    using (
                        var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var obj = Activator.CreateInstance<T>();
                            foreach (var prop in typeof(T).GetProperties())
                            {
                                if (!Equals(reader[prop.Name], DBNull.Value))
                                {
                                    prop.SetValue(obj, reader[prop.Name], null);
                                }
                            }
                            resultList.Add(obj);
                        }
                    }
                }
            }

            return resultList;
        }


    }
}
