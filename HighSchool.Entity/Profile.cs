using System;
using System.Reflection;

namespace HighSchool.Entity
{
    public class Profile : BaseEntity
    {
        public Guid IdProfile { get; set; }
        public string ProfileName { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string AboutMe { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string ProfileType { get; set; }
        public Guid IdUser { get; set; }

        public override PropertyInfo[] GetEntityProperties()
        {
            PropertyInfo[] info = typeof(Profile).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return info;
        }
    }
}
