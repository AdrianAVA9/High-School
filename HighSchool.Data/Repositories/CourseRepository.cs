using HighSchool.Data.DataAccess;
using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HighSchool.Data.Repositories
{
    public class CourseRepository : Repository, ICourseRepository
    {
        public CourseRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<List<Course>> RetriveCoursesAsync()
        {
            SqlOperation sqlO = new SqlOperation() { ProcedureName = "RET_COURSES" };
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

    }
}
