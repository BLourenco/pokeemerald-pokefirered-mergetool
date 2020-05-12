using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class LayoutsJsonEntry
    {
        private const string PREFIX = "FR_";

        public string id { get; private set; }
        public string name { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }
        public string primary_tileset { get; private set; }
        public string secondary_tileset { get; private set; }
        public string border_filepath { get; private set; }
        public string blockdata_filepath { get; private set; }

        public LayoutsJsonEntry(string id, string name, int width, int height, string primary_tileset, string secondary_tileset, string border_filepath, string blockdata_filepath)
        {
            this.id = id;
            this.name = name;
            this.width = width;
            this.height = height;
            this.primary_tileset = primary_tileset;
            this.secondary_tileset = secondary_tileset;
            this.border_filepath = border_filepath;
            this.blockdata_filepath = blockdata_filepath;
        }

        public void ApplyPrefix()
        {
            if (id == null) return; // Empty entry, skip

            id = id.Replace("LAYOUT_", "LAYOUT_" + PREFIX);
            name = PREFIX + name;
            primary_tileset = primary_tileset.Replace("gTileset_", "gTileset_" + PREFIX);
            secondary_tileset = secondary_tileset.Replace("gTileset_", "gTileset_" + PREFIX);
            border_filepath = border_filepath.Replace("data/layouts/", "data/layouts/" + PREFIX);
            blockdata_filepath = blockdata_filepath.Replace("data/layouts/", "data/layouts/" + PREFIX);
        }
    }
}
