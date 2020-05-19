using pokeemerald_pokefirered_mergetool.Core;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace pokeemerald_pokefirered_mergetool.FileEditors
{
    static class TilesetConverter
    {
        public static List<SlicedPrimaryData> SlicePrimaryTilesetData(List<Tileset> tilesets)
        {
            List<SlicedPrimaryData> spdList = new List<SlicedPrimaryData>();

            for (int i = 0; i < tilesets.Count; i++)
            {
                if (tilesets[i].is_secondary == "TRUE")
                    continue;

                SlicedPrimaryData spd = new SlicedPrimaryData(
                    tilesets[i].name,
                    tilesets[i].SliceTilesImage(),
                    tilesets[i].SliceMetatiles(),
                    tilesets[i].palettes[6]);

                spdList.Add(spd);
            }

            return spdList;
        }

        public static void StitchSlicedPrimaryDataToSecondaries(List<Tileset> tilesets, List<SlicedPrimaryData> slicedPrimaryData, List<Layout> layouts)
        {
            SlicedPrimaryData.SetPairedSecondaryTilesets(slicedPrimaryData, layouts);

            for (int i = 0; i < slicedPrimaryData.Count; i++)
            {
                for (int j = 0; j < slicedPrimaryData[i].secondaryTilesetNames.Count; j++)
                {
                    string secondaryName = slicedPrimaryData[i].secondaryTilesetNames[j];

                    for (int k = 0; k < tilesets.Count; k++)
                    {
                        if (tilesets[k].name != secondaryName)
                            continue;

                        tilesets[k].StitchMetatiles(slicedPrimaryData[i].slicedMetatiles);
                        tilesets[k].StitchTilesImage(slicedPrimaryData[i].slicedTiles);
                        tilesets[k].palettes[6] = slicedPrimaryData[i].slicedPalette;
                    }                    
                }
            }
        }
    }
}
