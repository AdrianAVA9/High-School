using HighSchool.Data.DataAccess;
using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace HighSchool.Data.Repositories
{
    public class Repository : IRepository
    {
        protected SqlDao SqlDaoInstance { get; set; }
        protected BuilderObjects BuilderObjects { get; set; }
        private string Connection { get; set; }

        public Repository(string connectionString)
        {
            Connection = connectionString;
            BuilderObjects = new BuilderObjects();
            SqlDaoInstance = SqlDao.GetInstance(connectionString);
        }

        public void SetConnection(string connection)
        {
            Connection = connection;
        }

        protected BaseEntity BuildObject(Dictionary<string, object> dict, BaseEntity obj)
        {
            return BuilderObjects.Build(dict, obj);
        }

        protected ICollection<BaseEntity> BuildObjects(List<Dictionary<string, object>> dicts, BaseEntity obj)
        {
            if (dicts == null)
                return null;

            var objectsCollections = new List<BaseEntity>();

            foreach (var dict in dicts)
            {
                objectsCollections.Add(BuilderObjects.Build(dict, obj));
            }

            return objectsCollections;
        }
    }

    public interface IRepository
    {
        void SetConnection(string connection);
    }
}
