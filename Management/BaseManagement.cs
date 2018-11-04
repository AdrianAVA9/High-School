using HighSchool.Data.Repositories;

namespace Management
{
    public abstract class BaseManagement
    {
        public IUnitOfWork UnitOfWork { get; set; }

        protected BaseManagement(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
