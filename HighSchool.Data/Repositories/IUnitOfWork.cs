namespace HighSchool.Data.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IProfileRepository Profile { get; }
        ICourseRepository Courses { get; }
    }
}
