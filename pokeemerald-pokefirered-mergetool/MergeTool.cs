using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    static class MergeTool
    {
        private static string pathEmerald;
        private static string pathFireRed;
        private static string pathOutput;

        // returns a result value
        public static long StartMerge(string[] paths)
        {
            // Set the paths
            pathEmerald = paths[0];
            pathFireRed = paths[1];
            pathOutput = paths[2];

            // Copy pokeemerald to output to use as a base for the merge
            bool success = FileManager.CopyDirectory(pathEmerald, pathOutput, true);

            if (!success)
            {
                return 1;
            }

            // Edit palette count
            FieldMapEditor.EditFieldMapPaletteTotal(pathOutput);

            // Copy prefixed tileset header entries
            TilesetHeaderIncEditor.CreateObjectsFromEntries(pathFireRed);
            TilesetHeaderIncEditor.WriteEntriesToFile(pathOutput);



            return 0;
        }
    }
}
