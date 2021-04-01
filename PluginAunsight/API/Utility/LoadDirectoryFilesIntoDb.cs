using System.Collections.Generic;
using System.Linq;
using PluginAunsight.API.Factory;
using PluginAunsight.Helper;
using SQLDatabase.Net.SQLDatabaseClient;

namespace PluginAunsight.API.Utility
{
    public static partial class Utility
    {
        public static void LoadDirectoryFilesIntoDb(
            IImportExportFactory factory, SqlDatabaseConnection conn, RootPathObject rootPath,
            string tableName, string schemaName, List<string> paths, long recordLimit = long.MaxValue, int fileLimit = int.MaxValue
            )
        {
            DeleteDirectoryFilesFromDb(conn, tableName, schemaName);

            foreach (var path in paths.Take(fileLimit))
            {
                ImportRecordsForPath(factory, conn, rootPath, tableName, schemaName, path,
                    recordLimit);
            }
        }
    }
}