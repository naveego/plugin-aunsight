using Naveego.Sdk.Plugins;
using PluginAunsight.API.Utility;
using PluginAunsight.DataContracts;

namespace PluginAunsight.API.Replication
{
    public static partial class Replication
    {
        public static ReplicationTable GetVersionReplicationTable(Schema schema, string safeSchemaName, string safeVersionTableName)
        {
            var versionTable = ConvertSchemaToReplicationTable(schema, safeSchemaName, safeVersionTableName);
            versionTable.Columns.Add(new ReplicationColumn
            {
                ColumnName = Constants.ReplicationVersionRecordId,
                DataType = $"VARCHAR({int.MaxValue})",
                PrimaryKey = true
            });
            versionTable.Columns.Add(new ReplicationColumn
            {
                ColumnName = Constants.ReplicationRecordId,
                DataType = $"VARCHAR({int.MaxValue})",
                PrimaryKey = false
            });
            // versionTable.Columns.Add(new ReplicationColumn
            // {
            //     ColumnName = Constants.ReplicationLineNumber,
            //     DataType = $"VARCHAR({int.MaxValue})",
            //     PrimaryKey = false,
            // });

            return versionTable;
        }
    }
}