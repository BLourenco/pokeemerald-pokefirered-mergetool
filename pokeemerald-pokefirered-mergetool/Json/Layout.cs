using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pokeemerald_pokefirered_mergetool.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class Layout
    {
        private const string FILE_PATH = @"\data\layouts\layouts.json";

        public string id { get; private set; }
        public string name { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }
        [JsonProperty("primary_tileset")]
        public string primary_tileset_label { get; private set; }
        [JsonProperty("secondary_tileset")]
        public string secondary_tileset_label { get; private set; }
        public string border_filepath { get; private set; }
        public string blockdata_filepath { get; private set; }

        public Layout(string id, string name, int width, int height, string primary_tileset_label, string secondary_tileset_label, string border_filepath, string blockdata_filepath)
        {
            if (id == null) return; // Empty entry, skip

            this.id = id;
            this.name = name;
            this.width = width;
            this.height = height;
            this.primary_tileset_label = primary_tileset_label;
            this.secondary_tileset_label = secondary_tileset_label;
            this.border_filepath = border_filepath;
            this.blockdata_filepath = blockdata_filepath;
        }

        public static List<Layout> CreateLayouts(string filePath)
        {
            string jsonString = File.ReadAllText(filePath + @"\data\layouts\layouts.json");
            jsonString = '[' + jsonString.Split('[', ']')[1] + ']';

            return JsonConvert.DeserializeObject<List<Layout>>(jsonString);
        }

        public static void WriteLayoutsToFile(string writePath, List<Layout> layouts)
        {
            string layoutsJson = File.ReadAllText(writePath + FILE_PATH);

            // Temporaily remove ending brackets so we can
            // write extra entries
            string endBrackets = "\n  ]\n}";
            layoutsJson = layoutsJson.Replace(endBrackets, "");

            for (int i = 0; i < layouts.Count; i++)
            {
                if (layouts[i].id == null) continue; // Empty entry, skip

                string json = JsonConvert.SerializeObject(layouts[i].GetLayoutWithPrefix());
                layoutsJson += "," + JToken.Parse(json).ToString(Formatting.Indented); // "Prettify" Json
            }

            File.WriteAllText(writePath + FILE_PATH, layoutsJson + endBrackets);
        }

        public string GetDirectoryName()
        {
            string n = name.Replace("_Layout", "").Replace("_", ""); // Remove layout postfix and existing underscores
            return MergeTool.PREFIX + (string.Concat(n.Select((x, j) => j > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower());
        }

        public Layout GetLayoutWithPrefix()
        {
            return new Layout(
                this.id.Replace("LAYOUT_", "LAYOUT_" + MergeTool.PREFIX),
                MergeTool.PREFIX + this.name,
                this.width,
                this.height,
                this.primary_tileset_label.Replace("gTileset_", "gTileset_" + MergeTool.PREFIX),
                this.secondary_tileset_label.Replace("gTileset_", "gTileset_" + MergeTool.PREFIX),
                this.border_filepath.Replace("data/layouts/", "data/layouts/" + MergeTool.PREFIX),
                this.blockdata_filepath.Replace("data/layouts/", "data/layouts/" + MergeTool.PREFIX));
        }
    }
}
