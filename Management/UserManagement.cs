using HighSchool.Data.Repositories;
using HighSchool.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Management
{
    public class UserManagement : BaseManagement, IUserManagement
    {
        public UserManagement(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await UnitOfWork.User.GetUserByUsernameAsync(username);
        }

        public async Task<User> GetUserAsync(string password, string username)
        {
            return await UnitOfWork.User.GetUserAsync(password, username);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await UnitOfWork.User.GetUsersAsync();
        }
    }

    public interface IUserManagement
    {
        Task<User> GetUserAsync(string password, string username);
        Task<User> GetUserByUsernameAsync(string username);
        Task<List<User>> GetUsersAsync();
    }
}
