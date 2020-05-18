using pokeemerald_pokefirered_mergetool.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class SlicedPrimaryData
    {
        public string primaryTilesetName { get; private set; }
        public List<string> secondaryTilesetNames { get; private set; }
        public Image slicedTiles { get; private set; }
        public List<Metatile> slicedMetatiles { get; private set; }

        public SlicedPrimaryData(string primaryTilesetName, Image slicedTiles, List<Metatile> slicedMetatiles)
        {
            this.primaryTilesetName = primaryTilesetName ?? throw new ArgumentNullException(nameof(primaryTilesetName));
            this.slicedTiles = slicedTiles ?? throw new ArgumentNullException(nameof(slicedTiles));
            this.slicedMetatiles = slicedMetatiles ?? throw new ArgumentNullException(nameof(slicedMetatiles));
        }
        public static void SetPairedSecondaryTilesets(List<SlicedPrimaryData> slicedPrimaryData, List<Layout> layouts)
        {
            for (int i = 0; i < layouts.Count; i++)
            {
                for (int j = 0; j < slicedPrimaryData.Count; j++)
                {
                    if (slicedPrimaryData[j].primaryTilesetName == layouts[i].primary_tileset_label)
                    {
                        if (!slicedPrimaryData[j].secondaryTilesetNames.Contains(layouts[i].secondary_tileset_label))
                        {
                            slicedPrimaryData[j].secondaryTilesetNames.Add(layouts[i].secondary_tileset_label);
                        }

                        break;
                    }
                }
            }
        }
    }
}
