using System.Collections.Generic;

namespace PluginAunsight.DataContracts
{
    public class ReplicationTable
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public List<ReplicationColumn> Columns { get; set; }
    }
}