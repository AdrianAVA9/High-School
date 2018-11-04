using HighSchool.Data.Repositories;
using HighSchool.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Management
{
    public class CourseManagement : BaseManagement, ICourseManagement
    {
        public CourseManagement(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<Course>> GetCourses()
        {
            return await UnitOfWork.Courses.RetriveCoursesAsync();
        }
    }

    public interface ICourseManagement
    {
        Task<List<Course>> GetCourses();
    }
}
