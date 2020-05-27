using System;
using System.IO;
using PluginFileReader.Helper;

namespace PluginFileReader.API.Utility
{
    public static partial class Utility 
    {
        public static void DeleteFileAtPath(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception e)
            {
                Logger.Error(e, $"Unable to delete file {path}");
                Logger.Error(e, e.Message);
            }
        }
    }
}