using System.Collections.Generic;
using System.Threading.Tasks;
using HighSchool.Entity;

namespace HighSchool.Data.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string password, string username);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetUsersAsync();
        Task<List<Course>> RetriveUsersAsync();
    }
}