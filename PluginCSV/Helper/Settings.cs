using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PluginCSV.Helper
{
    public class Settings
    {
        public List<RootPathObject> RootPaths { get; set; }

        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            if (RootPaths.Count == 0)
            {
                throw new Exception("At least one RootPath must be defined");
            }

            if (!RootPathsAreDirectories())
            {
                throw new Exception("A RootPath is not a directory");
            }

            if (!FilesExistAtRootPathsAndFilters())
            {
                throw new Exception("No files in given RootPaths with given Filters");
            }
        }

        /// <summary>
        /// Gets all files from location defined by RootPath and Filters and returns in a flat list
        /// </summary>
        /// <returns></returns>
        private List<string> GetAllFiles()
        {
            var files = new List<string>();
            foreach (var rootPath in RootPaths)
            {
                files.AddRange(Directory.GetFiles(rootPath.RootPath, rootPath.Filter));
            }
            
            return files;
        }
        
        /// <summary>
        /// Gets all files from location defined by RootPath and Filters and returns in a dictionary by directory
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetAllFilesByDirectory()
        {
            var filesByDirectory = new Dictionary<string, List<string>>();
            foreach (var rootPath in RootPaths)
            {
                if (filesByDirectory.TryGetValue(rootPath.RootPath, out var existingFiles))
                {
                    existingFiles.AddRange(Directory.GetFiles(rootPath.RootPath, rootPath.Filter));
                }
                else
                {
                    var files = new List<string>();
                    files.AddRange(Directory.GetFiles(rootPath.RootPath, rootPath.Filter));
                    filesByDirectory.Add(rootPath.RootPath, files);
                }
            }
            
            return filesByDirectory;
        }
        
        /// <summary>
        /// Checks if RootPaths are directories
        /// </summary>
        /// <returns></returns>
        private bool RootPathsAreDirectories()
        {
            foreach (var rootPath in RootPaths)
            {
                if (!Directory.Exists(rootPath.RootPath))
                {
                    throw new Exception($"{rootPath.RootPath} is not a directory");
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if files exist for the root path under the filters
        /// </summary>
        /// <returns></returns>
        private bool FilesExistAtRootPathsAndFilters()
        {
            return GetAllFiles().Count != 0;
        }
    }

    public class RootPathObject
    {
        public string RootPath { get; set; }
        public string Filter { get; set; }
        public string CleanupAction { get; set; }
        public string ArchivePath { get; set; }
        
        // FLAT FILE MODE SETTINGS
        public bool HasHeader { get; set; }
        public char Delimiter { get; set; }
        
        // FIXED COLUMN WIDTH MODE SETTINGS
        public List<Column> Columns { get; set; }
    }

    public class Column
    {
        public string ColumnName { get; set; }
        public bool IsKey { get; set; }
        public int ColumnStart { get; set; }
        public int ColumnEnd { get; set; }
    }
}