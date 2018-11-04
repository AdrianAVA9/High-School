using HighSchool.Data.DataAccess;
using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HighSchool.Data.Repositories
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<User> GetUserAsync(string password, string username)
        {
            try
            {
                User user = null;
                SqlOperation sqlO = new SqlOperation()
                {
                    ProcedureName = "RET_USER_BY_CREDENTIALS",
                };

                sqlO.addParameter(password, "Password");
                sqlO.addParameter(username, "Username");

                List<Dictionary<string, object>> data = await SqlDaoInstance.ExecuteQueryProcedureAsync(sqlO);

                if (data != null)
                {
                    foreach (var UserData in data)
                    {
                        user = (User)BuildObject(UserData, (new User()));
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            User user = null;
            SqlOperation sqlO = new SqlOperation()
            {
                ProcedureName = "RET_USER_BY_USERNAME",
            };

            sqlO.addParameter(username, "Username");

            try
            {
                List<Dictionary<string, object>> data = await SqlDaoInstance.ExecuteQueryProcedureAsync(sqlO);

                if (data != null)
                {
                    foreach (var UserData in data)
                    {
                        user = (User)BuildObject(UserData, (new User()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return user;
        }

        public async Task<List<Course>> RetriveUsersAsync()
        {
            SqlOperation sqlO = new SqlOperation() { ProcedureName = "RET_USER" };
            List<Course> listWM = new List<Course>();
            BaseEntity wm = new Course();

            try
            {

                List<Dictionary<string, object>> data = await SqlDaoInstance.ExecuteQueryProcedureAsync(sqlO);

                foreach (Dictionary<string, object> dict in data)
                {

                    listWM.Add((Course)BuildObject(dict, (wm = new Course())));

                }

            }
            catch (Exception ex)
            {
                throw ex;

            }

            return listWM;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            try
            {
                List<User> users = null;

                var operation = new SqlOperation
                {
                    ProcedureName = "RET_ALL_USERS",
                };

                var data = await SqlDaoInstance.ExecuteQueryProcedureAsync(operation);

                if (data != null)
                {
                    users = new List<User>();

                    foreach (Dictionary<string, object> dict in data)
                    {
                        users.Add((User)BuildObject(dict, (new User())));
                    }
                }

                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
