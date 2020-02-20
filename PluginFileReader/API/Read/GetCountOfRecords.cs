using Naveego.Sdk.Plugins;
using SQLDatabase.Net.SQLDatabaseClient;

namespace PluginFileReader.API.Read
{
    public static partial class Read
    {
        public static Count GetCountOfRecords(Schema schema, string dbFilePrefix)
        {
            var conn = Utility.Utility.GetSqlConnection(dbFilePrefix);

            var cmd = new SqlDatabaseCommand
            {
                Connection = conn,
                CommandText = $"SELECT COUNT(*) AS count FROM ({schema.Query}) AS Q"
            };

            var reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return new Count
                    {
                        Kind = Count.Types.Kind.Exact,
                        Value = reader.GetInt32(0)
                    };
                }
            }

            return new Count
            {
                Kind = Count.Types.Kind.Unavailable
            };
        }
    }
}