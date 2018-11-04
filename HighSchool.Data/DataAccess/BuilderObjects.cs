using HighSchool.Entity;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HighSchool.Data.DataAccess
{
    public class BuilderObjects
    {
        private static BuilderObjects _instance { get; set; }

        public BuilderObjects GetInstance()
        {
            if (_instance == null)
                _instance = new BuilderObjects();

            return _instance;
        }

        public BaseEntity Build(Dictionary<string, object> dict, BaseEntity obj)
        {
            Dictionary<string, object>.KeyCollection keys = dict.Keys;

            foreach (PropertyInfo property in obj.GetEntityProperties())
            {
                if (dict.ContainsKey(ChangeFormat(property.Name)))
                {
                    if (property.CanWrite)
                        SetValue(obj, property, dict);
                }
            }

            return obj;
        }

        public void SetValue(BaseEntity obj, PropertyInfo property, Dictionary<string, object> dict)
        {
            if (property.PropertyType == typeof(int))
            {
                property.SetValue(obj, int.Parse(dict[ChangeFormat(property.Name)].ToString()), null);
            }
            else
            {
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(obj, dict[ChangeFormat(property.Name)].ToString(), null);
                }
                else
                {
                    if (property.PropertyType == typeof(bool))
                    {
                        property.SetValue(obj, bool.Parse(dict[ChangeFormat(property.Name)].ToString()), null);
                    }
                    else
                    {
                        if (property.PropertyType == typeof(DateTime))
                        {
                            property.SetValue(obj, DateTime.Parse(dict[ChangeFormat(property.Name)].ToString()), null);
                        }
                        else
                        {
                            if (property.PropertyType == typeof(decimal))
                            {
                                property.SetValue(obj, decimal.Parse(dict[ChangeFormat(property.Name)].ToString()), null);
                            }
                        }
                    }
                }
            }
        }


        public string ChangeFormat(string value)
        {
            StringBuilder format = new StringBuilder();
            bool firstLetter = true;

            foreach (char letter in value)
            {
                if (char.IsUpper(letter) && !firstLetter)
                {
                    format.Append(string.Concat("_" + letter));
                }
                else
                {
                    format.Append(letter);
                    firstLetter = false;
                }
            }

            return format.ToString().ToUpper();
        }
    }
}
