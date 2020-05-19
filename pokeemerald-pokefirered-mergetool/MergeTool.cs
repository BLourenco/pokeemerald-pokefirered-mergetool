using pokeemerald_pokefirered_mergetool.Core;
using pokeemerald_pokefirered_mergetool.FileEditors;
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

        public const string PREFIX = "FR_";

        public enum Version
        {
            FireRed,
            Emerald
        }

        // returns a result value
        public static long StartMerge(string[] paths)
        {
            // Set the paths
            pathEmerald = paths[0];
            pathFireRed = paths[1];
            pathOutput = paths[2];

            //BitmapEditor.SliceAndStitchTiles();

            /// TODO: Check if output path is empty before copying to it

            // Copy pokeemerald to output to use as a base for the merge
            DirectoryUtil.CopyDirectory(pathEmerald, pathOutput, true);

            // Copy tilesets, layouts, and maps directories, and add prefix
            DirectoryUtil.CopyDirectory(pathFireRed + DirectoryUtil.DIR_TILESETS_PRIMARY,
                                        pathOutput + DirectoryUtil.DIR_TILESETS_PRIMARY,
                                        true,
                                        false,
                                        true);

            DirectoryUtil.CopyDirectory(pathFireRed + DirectoryUtil.DIR_TILESETS_SECONDARY,
                                        pathOutput + DirectoryUtil.DIR_TILESETS_SECONDARY,
                                        true,
                                        false,
                                        true);

            DirectoryUtil.CopyDirectory(pathFireRed + DirectoryUtil.DIR_LAYOUTS,
                                        pathOutput + DirectoryUtil.DIR_LAYOUTS,
                                        true,
                                        false,
                                        true);

            DirectoryUtil.CopyDirectory(pathFireRed + DirectoryUtil.DIR_MAPS,
                                        pathOutput + DirectoryUtil.DIR_MAPS,
                                        true,
                                        false,
                                        true);

            // Edit palette count
            //FieldMapEditor.EditFieldMapPaletteTotal(pathOutput);

            // Read tileset headers/graphics/metatiles entries and layouts
            List<Tileset> tilesetsFireRed = Tileset.CreateTilesets(pathFireRed);
            List<Layout> layoutsFireRed = Layout.CreateLayouts(pathFireRed);

            // Convert tileset data to Emerald Format
            TilesetConverter.StitchSlicedPrimaryDataToSecondaries(
                tilesetsFireRed,
                TilesetConverter.SlicePrimaryTilesetData(tilesetsFireRed),
                layoutsFireRed);

            // Write tilesets and layouts
            Tileset.WriteTilesetsToFiles(pathOutput, tilesetsFireRed);
            Layout.WriteLayoutsToFile(pathOutput, layoutsFireRed);

            // Copy prefixed graphics rule entries
            GraphicsFileRuleEditor.CreateObjectsFromEntries(pathFireRed);
            GraphicsFileRuleEditor.WriteEntriesToFile(pathOutput);

            // Copy prefixed event script entries
            EventScriptsEditor.CreateObjectsFromEntries(pathFireRed);       // TODO: Adjust num of tiles to account for sliced/stitched tiles
            EventScriptsEditor.WriteEntriesToFile(pathOutput);

            // Copy re-numbered map groups with prefixed entries
            MapGroupsEditor.CreateObjectsFromEntries(pathFireRed);
            MapGroupsEditor.WriteEntriesToFile(pathOutput);

            // Go through the moved maps and update their files
            MapJsonEditor.UpdateAllFireRedMapFiles(pathOutput);         // TODO: Fix issue where empty scripts are being prefixed

            return 0;
        }
    }
}
