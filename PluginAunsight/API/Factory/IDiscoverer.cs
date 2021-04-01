using System.Collections.Generic;
using Grpc.Core;
using Naveego.Sdk.Plugins;
using PluginAunsight.Helper;

namespace PluginAunsight.API.Factory
{
    public interface IDiscoverer
    {
        public IEnumerable<Schema> DiscoverSchemas(ServerCallContext context, IImportExportFactory factory, RootPathObject rootPath, List<string> paths, int sampleSize = 5);
    }
}