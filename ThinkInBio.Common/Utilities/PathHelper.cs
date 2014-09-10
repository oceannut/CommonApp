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

    }

}
