using System.IO;
using PluginFileReader.API.Utility;
using PluginFileReader.Helper;

namespace PluginFileReader.DataContracts
{
    public class ConfigureReplicationFormData
    {
        public string GoldenRecordFileDirectory { get; set; }
        public string GoldenRecordFileName { get; set; }
        public string VersionRecordFileDirectory { get; set; }
        public string VersionRecordFileName { get; set; }
        public bool IncludeHeader { get; set; }
        public bool QuoteWrap { get; set; }
        public string Delimiter { get; set; }
        public string NullValue { get; set; }
        public string CustomHeader { get; set; }
        
        public char GetDelimiter()
        {
            switch (Delimiter)
            {
                case "\\t":
                    return '\t';
                default:
                    return char.Parse(Delimiter);
            }
        }

        public string GetGoldenTableName()
        {
            if (string.IsNullOrWhiteSpace(GoldenRecordFileName))
            {
                return Constants.DefaultGoldenTable;
            }
            return Path.GetFileNameWithoutExtension(GoldenRecordFileName);
        }

        public string GetGoldenFilePath()
        {
            return Path.Join(GoldenRecordFileDirectory, GoldenRecordFileName);
        }

        public string GetVersionTableName()
        {
            if (string.IsNullOrWhiteSpace(VersionRecordFileName))
            {
                return Constants.DefaultVersionTable;
            }
            return Path.GetFileNameWithoutExtension(VersionRecordFileName);
        }

        public string GetVersionFilePath()
        {
            return Path.Join(VersionRecordFileDirectory, VersionRecordFileName);
        }
    }
}