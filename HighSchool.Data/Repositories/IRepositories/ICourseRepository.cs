using System.Collections.Generic;
using System.Threading.Tasks;
using HighSchool.Entity;

namespace HighSchool.Data.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> RetriveCoursesAsync();
    }
}