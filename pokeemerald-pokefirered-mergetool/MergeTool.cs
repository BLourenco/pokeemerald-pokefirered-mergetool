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
            FileManager.CopyDirectory(pathEmerald, pathOutput, true);

            // Edit palette count
            FieldMapEditor.EditFieldMapPaletteTotal(pathOutput);

            // Copy prefixed tileset header entries
            TilesetHeaderIncEditor.CreateObjectsFromEntries(pathFireRed);
            TilesetHeaderIncEditor.WriteEntriesToFile(pathOutput);

            // Copy prefixed tileset graphics entries
            TilesetGraphicsIncEditor.CreateObjectsFromEntries(pathFireRed);
            TilesetGraphicsIncEditor.WriteEntriesToFile(pathOutput);

            // Copy prefixed tileset metatiles entries
            TilesetMetatilesIncEditor.CreateObjectsFromEntries(pathFireRed);
            TilesetMetatilesIncEditor.WriteEntriesToFile(pathOutput);

            // Copy prefixed graphics rule entries
            GraphicsFileRuleEditor.CreateObjectsFromEntries(pathFireRed);
            GraphicsFileRuleEditor.WriteEntriesToFile(pathOutput);

            // Copy prefixed layout entries
            LayoutsJsonEditor.CreateObjectsFromEntries(pathFireRed);
            LayoutsJsonEditor.WriteEntriesToFile(pathOutput);

            // Copy prefixed event script entries
            EventScriptsEditor.CreateObjectsFromEntries(pathFireRed);
            EventScriptsEditor.WriteEntriesToFile(pathOutput);

            // Copy re-numbers map groups with prefixed entries
            MapGroupsEditor.CreateObjectsFromEntries(pathFireRed);
            MapGroupsEditor.WriteEntriesToFile(pathOutput);

            return 0;
        }
    }
}
