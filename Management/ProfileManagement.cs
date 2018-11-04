using HighSchool.Data.Repositories;
using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Management
{
    public class ProfileManagement : BaseManagement, IProfileManagement
    {
        public ProfileManagement(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<Profile>> GetProfilesAsync()
        {
            return await UnitOfWork.Profile.Retrive_ProfilesAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(Guid id)
        {
            return await UnitOfWork.Profile.GetProfileByIdAsync(id);
        }

        public async Task<Profile> GetProfileByIdUserAsync(Guid id)
        {
            return await UnitOfWork.Profile.GetProfileByIdUserAsync(id);
        }
    }

    public interface IProfileManagement
    {
        Task<List<Profile>> GetProfilesAsync();
        Task<Profile> GetProfileByIdAsync(Guid id);
        Task<Profile> GetProfileByIdUserAsync(Guid id);
    }
}
