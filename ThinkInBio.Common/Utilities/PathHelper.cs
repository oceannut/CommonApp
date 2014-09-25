using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ThinkInBio.Common.Utilities
{

    public static class PathHelper
    {

        public static string RootFileNameAndEnsureTargetFolderExists(string fileName)
        {
            string rootedFileName = fileName;
            if (!Path.IsPathRooted(rootedFileName))
            {
                rootedFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootedFileName);
            }

            string directory = Path.GetDirectoryName(rootedFileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return rootedFileName;
        }

        public static string EnsureTargetFolderExists(string dir, DateTime timeStamp)
        {
            if (timeStamp == DateTime.MinValue)
            {
                throw new ArgumentNullException();
            }
            string rootDir = dir;
            if (!Path.IsPathRooted(rootDir))
            {
                rootDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, rootDir);
            }
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            rootDir = Path.Combine(rootDir, timeStamp.ToString("yyyy"));
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            rootDir = Path.Combine(rootDir, timeStamp.ToString("MM"));
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }
            rootDir = Path.Combine(rootDir, timeStamp.ToString("dd"));
            if (!Directory.Exists(rootDir))
            {
                Directory.CreateDirectory(rootDir);
            }

            return rootDir;
        }

    }

}
