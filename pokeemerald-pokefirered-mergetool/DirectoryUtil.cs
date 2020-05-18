using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    static class DirectoryUtil
    {
        public const string DIR_TILESETS_PRIMARY = @"\data\tilesets\primary";
        public const string DIR_TILESETS_SECONDARY = @"\data\tilesets\secondary";
        public const string DIR_LAYOUTS = @"\data\layouts";
        public const string DIR_MAPS = @"\data\maps";

        public static bool CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs, bool copyRootFiles = true, bool addPrefixToFolders = false)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            if (copyRootFiles)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, addPrefixToFolders ? MergeTool.PREFIX + subdir.Name : subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, copySubDirs);
                }
            }

            return true;
        }

    }
}
