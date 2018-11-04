using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace HighSchool.Data.DataAccess
{
    public class SqlOperation
    {
        public string ProcedureName { get; set; }
        public List<SqlParameter> parameters { get; set; }

        public SqlOperation()
        {
            parameters = new List<SqlParameter>();
        }

        public void addParameter(string value, string parameterName)
        {
            addParameterToListParameters(Convert.ToString(value), SqlDbType.NVarChar, parameterName);
        }

        public void addParameter(Guid value, string parameterName)
        {
            addParameterToListParameters(Convert.ToString(value), SqlDbType.NVarChar, parameterName);
        }

        public void addParameter(int value, string parameterName)
        {
            addParameterToListParameters(Convert.ToString(value), SqlDbType.Int, parameterName);
        }

        public void addParameter(double value, string parameterName)
        {
            addParameterToListParameters(Convert.ToString(value), SqlDbType.Float, parameterName);
        }

        public void addParameter(bool value, string parameterName)
        {
            addParameterToListParameters(Convert.ToString(value), SqlDbType.Bit, parameterName);
        }

        public void addParameter(DateTime value, string parameterName)
        {
            addParameterToListParameters(Convert.ToString(value), SqlDbType.DateTime, parameterName);
        }

        private void addParameterToListParameters(string value, SqlDbType type, string parameterName)
        {
            parameters.Add(new SqlParameter()
            {
                ParameterName = String.Concat("@" + GetNewFormatString(parameterName)),
                Value = value,
                SqlDbType = type,
            });
        }

        public static string GetNewFormatString(string valueToTransform)
        {
            StringBuilder newFormat = new StringBuilder();
            bool IsTheFirstLetter = true;

            foreach (char letter in valueToTransform)
            {
                if (char.IsUpper(letter) && !IsTheFirstLetter)
                {
                    newFormat.Append(string.Concat("_" + letter));
                }
                else
                {
                    newFormat.Append(letter);
                    IsTheFirstLetter = false;
                }
            }

            return newFormat.ToString().ToUpper();
        }
    }


    public class SqlOperationV2
    {
        public List<SqlParameter> Parameters { get; set; }
        public string Query { get; set; }

        public SqlOperationV2()
        {
            Parameters = new List<SqlParameter>();
        }

        public void AddParameter(string nameProperty, object obj, Type type)
        {
            if (type == typeof(int))
                AttachParameter(nameProperty, obj.ToString(), SqlDbType.Int);
            else
                if (type == typeof(string))
                AttachParameter(nameProperty, obj.ToString(), SqlDbType.VarChar);
            else
                    if (type == typeof(bool))
                AttachParameter(nameProperty, obj.ToString(), SqlDbType.Bit);
            else
                        if (type == typeof(DateTime))
                AttachParameter(nameProperty, obj.ToString(), SqlDbType.DateTime);
            else
                            if (type == typeof(double))
                AttachParameter(nameProperty, obj.ToString(), SqlDbType.Float);
            else
                                if (type == typeof(decimal))
                AttachParameter(nameProperty, obj.ToString(), SqlDbType.Decimal);
        }

        public void AttachParameter(string parameterName, string value, SqlDbType type)
        {
            Parameters.Add(new SqlParameter(string.Concat("@" + parameterName), type) { Value = value });
        }

        public void AddParameter(string parameterName, string value)
        {
            AttachParameter(parameterName, Convert.ToString(value), SqlDbType.VarChar);
        }

        public void AddParameter(string parameterName, DateTime value)
        {
            AttachParameter(parameterName, Convert.ToString(value), SqlDbType.DateTime);
        }

        public void AddParameter(string parameterName, int value)
        {
            AttachParameter(parameterName, Convert.ToString(value), SqlDbType.Int);
        }

        public void AddParameter(string parameterName, bool value)
        {
            AttachParameter(parameterName, Convert.ToString(value), SqlDbType.Bit);
        }

        public void AddParameter(string parameterName, double value)
        {
            AttachParameter(parameterName, Convert.ToString(value), SqlDbType.Float);
        }

        public void AddParameter(string parameterName, decimal value)
        {
            AttachParameter(parameterName, Convert.ToString(value), SqlDbType.Decimal);

        }
    }
}
