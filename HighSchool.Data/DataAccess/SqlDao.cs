using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace HighSchool.Data.DataAccess
{
    public class SqlDao
    {
        private static SqlDao Instance_ { get; }
        private string ConnectionString { get; set; }

        private SqlDao() { }

        public static SqlDao GetInstance(string connectionString)
        {
            return Instance_ ?? (new SqlDao() { ConnectionString = connectionString });
        }

        public async void ExecuteProcedureAsync(SqlOperation operation)
        {
            using (var conn = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(operation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var parameter in operation.parameters)
                {
                    command.Parameters.Add(parameter);
                }

                conn.Open();
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<Dictionary<string, object>>> ExecuteQueryProcedureAsync(SqlOperation operation)
        {
            List<Dictionary<string, object>> listData = null;

            using (var conn = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(operation.ProcedureName, conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                foreach (var parameter in operation.parameters)
                {
                    command.Parameters.Add(parameter);
                }

                conn.Open();
                var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    listData = new List<Dictionary<string, object>>();
                    Dictionary<string, object> data;

                    while (reader.Read())
                    {
                        data = new Dictionary<string, object>();

                        for (int cantValue = 0; cantValue < reader.VisibleFieldCount; cantValue++)
                        {
                            data.Add(reader.GetName(cantValue), reader.GetSqlValue(cantValue));
                        }

                        listData.Add(data);
                    }
                }


            }

            return listData;
        }
    }
}
