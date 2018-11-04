using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HighSchool.Data.Repositories
{
    public interface IProfileRepository
    {
        Task<Profile> GetProfileByIdAsync(Guid id);
        Task<Profile> GetProfileByIdUserAsync(Guid id);
        Task<List<Profile>> Retrive_ProfilesAsync();
    }
}