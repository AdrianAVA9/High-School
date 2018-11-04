using HighSchool.Data.DataAccess;
using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HighSchool.Data.Repositories
{
    public class ProfileRepository : Repository, IProfileRepository
    {
        public ProfileRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<List<Profile>> Retrive_ProfilesAsync()
        {
            SqlOperation sqlO = new SqlOperation() { ProcedureName = "RET_PROFILE" };
            List<Profile> listProfile = null;

            try
            {
                List<Dictionary<string, object>> data = await SqlDaoInstance.ExecuteQueryProcedureAsync(sqlO);

                if (data != null)
                {
                    listProfile = new List<Profile>();

                    foreach (Dictionary<string, object> dict in data)
                    {
                        listProfile.Add((Profile)BuildObject(dict, (new Profile())));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listProfile;
        }

        public async Task<Profile> GetProfileByIdUserAsync(Guid id)
        {
            Profile profile = null;
            SqlOperation sqlO = new SqlOperation()
            {
                ProcedureName = "RET_PROFILE_BY_ID_USER",
            };

            sqlO.addParameter(id, "IdUser");

            try
            {
                List<Dictionary<string, object>> data = await SqlDaoInstance.ExecuteQueryProcedureAsync(sqlO);

                if (data != null)
                {
                    foreach (var profileData in data)
                    {
                        profile = (Profile)BuildObject(profileData, (new Profile()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return profile;
        }

        public async Task<Profile> GetProfileByIdAsync(Guid id)
        {
            Profile profile = null;
            SqlOperation sqlO = new SqlOperation()
            {
                ProcedureName = "RET_PROFILE_BY_ID",
            };

            sqlO.addParameter(id, "IdProfile");

            try
            {
                List<Dictionary<string, object>> data = await SqlDaoInstance.ExecuteQueryProcedureAsync(sqlO);

                if (data != null)
                {
                    foreach (var profileData in data)
                    {
                        profile = (Profile)BuildObject(profileData, (new Profile()));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return profile;
        }
    }
}
