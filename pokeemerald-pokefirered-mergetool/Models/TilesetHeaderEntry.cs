using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class TilesetHeaderEntry
    {
        private const string PREFIX = "FR_";

        // The entry format for Emerald
        private const string ENTRY_MAIN =
            "\t.align 2\n" +
            "gTileset_{0}::\n" +
            "\t.byte {1} @ is compressed\n" +
            "\t.byte {2} @ is secondary tileset\n" +
            "\t.2byte 0 @ padding\n" +
            "\t.4byte gTilesetTiles_{0}\n" +
            "\t.4byte gTilesetPalettes_{0}\n" +
            "\t.4byte gMetatiles_{0}\n" +
            "\t.4byte gMetatileAttributes_{0}\n";

        private const string ENTRY_ANIM_CALLBACK =
            "\t.4byte InitTilesetAnim_{0}\n";

        private const string ENTRY_ANIM_CALLBACK_NULL =
            "\t.4byte NULL @ animation callback\n";

        private string entryName;
        private string isCompressed;
        private string isSecondaryTileset;
        private bool callbackExists;

        public TilesetHeaderEntry(string entryName, string isCompressed, string isSecondaryTileset, bool callbackExists)
        {
            this.entryName = entryName ?? throw new ArgumentNullException(nameof(entryName));
            this.isCompressed = isCompressed ?? throw new ArgumentNullException(nameof(isCompressed));
            this.isSecondaryTileset = isSecondaryTileset ?? throw new ArgumentNullException(nameof(isSecondaryTileset));
            this.callbackExists = callbackExists;
        }

        public string GetEntryString()
        {
            string entry = ENTRY_MAIN +
                (callbackExists ? ENTRY_ANIM_CALLBACK : ENTRY_ANIM_CALLBACK_NULL);

            return string.Format(entry, PREFIX + entryName, isCompressed, isSecondaryTileset) + "\n";
        }
    }
}
