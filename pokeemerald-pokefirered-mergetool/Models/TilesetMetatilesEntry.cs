using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class TilesetMetatilesEntry
    {
        private const string PREFIX = "FR_";

        // The entry format for Emerald
        private const string ENTRY_MAIN_METATILES =
            "\t.align 1\n" +
            "gMetatiles_{0}::\n";

        private const string ENTRY_MAIN_ATTRIBUTES =
            "\t.align 1\n" +
            "gMetatileAttributes_{0}::\n";

        private const string ENTRY_METATILES_PATH =
            "\t.incbin \"data/tilesets/{1}/{0}/metatiles.bin\"\n";

        private const string ENTRY_ATTRIBUTES_PATH =
            "\t.incbin \"data/tilesets/{1}/{0}/metatile_attributes.bin\"\n";

        private string entryName;
        private bool isSecondaryTileset;

        public TilesetMetatilesEntry(string entryName, bool isSecondaryTileset)
        {
            this.entryName = entryName ?? throw new ArgumentNullException(nameof(entryName));
            this.isSecondaryTileset = isSecondaryTileset;
        }

        public string GetEntryString()
        {
            string entry = ENTRY_MAIN_METATILES + ENTRY_METATILES_PATH + "\n" +
                            ENTRY_MAIN_ATTRIBUTES + ENTRY_ATTRIBUTES_PATH;

            return string.Format(entry, PREFIX + entryName, isSecondaryTileset ? "secondary" : "primary") + "\n";
        }
    }
}
