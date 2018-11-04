using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HighSchool.Entity
{
    public class User:BaseEntity
    {
        public Guid IdUser { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public override PropertyInfo[] GetEntityProperties()
        {
            PropertyInfo[] info = typeof(User).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            return info;
        }
    }
}
