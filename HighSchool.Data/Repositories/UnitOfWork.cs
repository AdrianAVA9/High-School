using System;
using System.Data;

namespace HighSchool.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository User { get; set; }
        public IProfileRepository Profile { get; set; }
        public ICourseRepository Courses { get; set; }

        public UnitOfWork(string connection)
        {
            User = new UserRepository(connection);
            Profile = new ProfileRepository(connection);
            Courses = new CourseRepository(connection);
        }
    }
}
