using Naveego.Sdk.Plugins;
using PluginAunsight.API.Utility;
using PluginAunsight.DataContracts;

namespace PluginAunsight.API.Replication
{
    public static partial class Replication
    {
        public static ReplicationTable GetGoldenReplicationTable(Schema schema, string safeSchemaName, string safeGoldenTableName)
        {
            var goldenTable = ConvertSchemaToReplicationTable(schema, safeSchemaName, safeGoldenTableName);
            goldenTable.Columns.Add(new ReplicationColumn
            {
                ColumnName = Constants.ReplicationRecordId,
                DataType = $"VARCHAR({int.MaxValue})",
                PrimaryKey = true
            });
            goldenTable.Columns.Add(new ReplicationColumn
            {
                ColumnName = Constants.ReplicationVersionIds,
                DataType = $"VARCHAR({int.MaxValue})",
                PrimaryKey = false,
                Serialize = true
            });
            // goldenTable.Columns.Add(new ReplicationColumn
            // {
            //     ColumnName = Constants.ReplicationLineNumber,
            //     DataType = $"VARCHAR({int.MaxValue})",
            //     PrimaryKey = false,
            // });

            return goldenTable;
        }
    }
}