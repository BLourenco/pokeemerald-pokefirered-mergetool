using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class GraphicsFileRuleEntry
    {
        private const string PREFIX = "FR_";

        // The entry format for Emerald
        private const string ENTRY_MAIN =
            "$(TILESETGFXDIR)/{1}/{0}/tiles.4bpp: %.4bpp: %.png\n" +
            "\t$(GFX) $< $@ -num_tiles {2}\n";

        private string entryName;
        private bool isSecondaryTileset;
        private int numTiles;

        public GraphicsFileRuleEntry(string entryName, bool isSecondaryTileset, int numTiles)
        {
            this.entryName = entryName ?? throw new ArgumentNullException(nameof(entryName));
            this.isSecondaryTileset = isSecondaryTileset;
            this.numTiles = numTiles;
        }

        public string GetEntryString()
        {
            return string.Format(ENTRY_MAIN, PREFIX + entryName, isSecondaryTileset ? "secondary" : "primary", numTiles) + "\n";
        }
    }
}
