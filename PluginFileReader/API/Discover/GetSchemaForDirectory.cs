using System.Collections.Generic;
using System.IO;
using System.Linq;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginFileReader.API.Factory;
using PluginFileReader.API.Utility;
using PluginFileReader.DataContracts;
using PluginFileReader.Helper;

namespace PluginFileReader.API.Discover
{
    public static partial class Discover
    {
        public static IEnumerable<Schema> GetSchemasForDirectory(IImportExportFactory factory, RootPathObject rootPath, List<string> paths,
            int sampleSize = 5)
        {
            if (paths.Count == 0)
            {
                return new List<Schema>();
            }
            
            if (sampleSize == 0)
            {
                sampleSize = 5;
            }
            
            if (factory.CustomDiscover)
            {
                return factory.MakeDiscoverer().DiscoverSchemas(factory, rootPath, paths, sampleSize);
            }
            
            var schemaName = Constants.SchemaName;
            var tableName = string.IsNullOrWhiteSpace(rootPath.Name)
                ? new DirectoryInfo(rootPath.RootPath).Name
                : rootPath.Name;
            var schemaId = $"[{schemaName}].[{tableName}]";
            var publisherMetaJson = new SchemaPublisherMetaJson
            {
                RootPath = rootPath
            };
            
            var conn = Utility.Utility.GetSqlConnection(Constants.DiscoverDbPrefix);

            Utility.Utility.LoadDirectoryFilesIntoDb(factory, conn, rootPath, tableName, schemaName, paths, sampleSize, 1);
            
            var schema = new Schema
            {
                Id = schemaId,
                Name = tableName,
                DataFlowDirection = Schema.Types.DataFlowDirection.Read,
                Query = $"SELECT * FROM {schemaId}",
                Properties = {},
            };

            schema = GetSchemaForQuery(schema, sampleSize, rootPath?.ModeSettings?.FixedWidthSettings?.Columns);
            schema.PublisherMetaJson = JsonConvert.SerializeObject(publisherMetaJson);

            return new List<Schema>
            {
                schema
            };
        }
    }
}