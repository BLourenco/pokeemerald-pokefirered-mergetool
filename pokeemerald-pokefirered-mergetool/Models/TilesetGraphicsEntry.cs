using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class TilesetGraphicsEntry
    {
        private const string PREFIX = "FR_";
        private const int NUM_OF_PALETTES = 16;

        // The entry format for Emerald
        private const string ENTRY_MAIN =
            "\t.align 2\n" +
            "gTilesetTiles_{0}::\n" +
            "\t.incbin  \"data/tilesets/{1}/{0}/tiles.4bpp.lz\"\n" +
            "\n" +
            "\t.align 2\n" +
            "gTilesetPalettes_{0}::\n";

        private const string ENTRY_PALETTE_PATH =
            "\t.incbin \"data/tilesets/{1}/{0}/palettes/{2}.gba.pal\n";

        private string entryName;
        private bool isSecondaryTileset;

        public TilesetGraphicsEntry(string entryName, bool isSecondaryTileset)
        {
            this.entryName = entryName ?? throw new ArgumentNullException(nameof(entryName));
            this.isSecondaryTileset = isSecondaryTileset;
        }

        public string GetEntryString()
        {
            string entry = ENTRY_MAIN;

            for (int i = 0; i < NUM_OF_PALETTES; i++)
            {
                // Just add the palette numbers from 00 to 15
                entry += string.Format(ENTRY_PALETTE_PATH, "{0}", "{1}", i.ToString("D2"));
            }

            return string.Format(entry, PREFIX + entryName, isSecondaryTileset ? "secondary" : "primary") + "\n";
        }
    }
}
