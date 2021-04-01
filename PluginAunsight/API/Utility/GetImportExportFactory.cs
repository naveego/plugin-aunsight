using PluginAunsight.API.Factory;
using PluginAunsight.API.Factory.Implementations.AS400;
using PluginAunsight.API.Factory.Implementations.Delimited;
using PluginAunsight.API.Factory.Implementations.Excel;
using PluginAunsight.API.Factory.Implementations.FixedWidthColumns;
using PluginAunsight.API.Factory.Implementations.XML;

namespace PluginAunsight.API.Utility
{
    public static partial class Utility
    {
        /// <summary>
        /// Gets an instance of the correct IImportExportFactory
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static IImportExportFactory GetImportExportFactory(string mode)
        {
            switch (mode)
            {
                case Constants.ModeDelimited:
                    return new DelimitedImportExportFactory();
                case Constants.ModeFixedWidth:
                    return new FixedWidthColumnsFactory();
                case Constants.ModeExcel:
                    return new ExcelImportExportFactory();
                case Constants.ModeAS400:
                    return new AS400ImportExportFactory();
                case Constants.ModeXML:
                    return new XmlImportExportFactory();
                default:
                    return null;
            }
        }
    }
}