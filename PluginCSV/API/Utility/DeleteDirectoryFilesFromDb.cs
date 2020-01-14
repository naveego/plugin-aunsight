using System;
using PluginCSV.Helper;
using SQLDatabase.Net.SQLDatabaseClient;

namespace PluginCSV.API.Utility
{
    public static partial class Utility
    {
        public static void DeleteDirectoryFilesFromDb(SqlDatabaseConnection conn, string tableName, string schemaName)
        {
            try
            {
                var cmd = new SqlDatabaseCommand
                {
                    Connection = conn,
                    CommandText =
                        $@"DELETE FROM [{schemaName}].[{tableName}]"
                };
            
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
        }
    }
}