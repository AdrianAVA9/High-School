using System;
using System.Reflection;

namespace HighSchool.Entity
{
    public class Course : BaseEntity
    {
        public Guid IdCourse { get; set; }
        public string CourseName { get; set; }
        public string Code { get; set; }
        public int Cost { get; set; }
        public int Credits { get; set; }

        public override PropertyInfo[] GetEntityProperties()
        {
            PropertyInfo[] info = typeof(Course).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return info;
        }
    }
}
