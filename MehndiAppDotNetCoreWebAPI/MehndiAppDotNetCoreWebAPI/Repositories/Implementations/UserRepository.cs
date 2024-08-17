using MehndiAppDotNerCoreWebAPI.Helpers;
using MehndiAppDotNerCoreWebAPI.Models;
using MehndiAppDotNerCoreWebAPI.Repositories.Interfaces;
using System.Data;

namespace MehndiAppDotNerCoreWebAPI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlHelper _sqlHelper;

        public UserRepository(SqlHelper sqlHelper)
        {
            _sqlHelper = sqlHelper;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var query = "SELECT * FROM Mh_Users";
            var dataTable = _sqlHelper.ExecuteQuery(query);
            var users = new List<User>();
            foreach (DataRow row in dataTable.Rows)
            {
                users.Add(new User
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Name = row["Name"].ToString(),
                    Email = row["Email"].ToString()
                });
            }
            return users;
        }

        void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.AddUser(User user)
        {
            throw new NotImplementedException();
        }

        void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        User GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        User IUserRepository.GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        void IUserRepository.UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        // Implement other methods...
    }
}
