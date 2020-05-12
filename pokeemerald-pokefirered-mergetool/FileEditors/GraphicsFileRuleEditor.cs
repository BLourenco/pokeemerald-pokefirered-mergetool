using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class GraphicsFileRuleEditor
    {
        private const string FILE_PATH_FR = @"\tileset_rules.mk";
        private const string FILE_PATH_E = @"\graphics_file_rules.mk";

        private static List<GraphicsFileRuleEntry> entryObjects = new List<GraphicsFileRuleEntry>();

        public static void CreateObjectsFromEntries(string readDir)
        {
            StreamReader reader = new StreamReader(readDir + FILE_PATH_FR);
            string input = reader.ReadToEnd();

            string[] splitString = { "$(TILESETGFXDIR)" };
            string[] entries = input.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < entries.Length; i++)
            {
                if (i == 0) continue; // Skip first line

                entryObjects.Add(CreateObjectFromEntry(entries[i].Trim()));
            }
        }

        private static GraphicsFileRuleEntry CreateObjectFromEntry(string entry)
        {
            string[] split = entry.Split(
                new string[] { "/" },
                StringSplitOptions.RemoveEmptyEntries);

            string name = split[1];
            bool isSecondaryTileset = entry.Contains("/secondary/");

            split = entry.Split(' ');
            int numTiles = int.Parse(split[split.Length-1]);

            return new GraphicsFileRuleEntry(name, isSecondaryTileset, numTiles);
        }

        public static void WriteEntriesToFile(string writeDir)
        {
            StreamReader reader = new StreamReader(writeDir + FILE_PATH_E);
            string input = reader.ReadToEnd();
            reader.Close();

            string appendedEntries = "";

            for (int i = 0; i < entryObjects.Count; i++)
            {
                appendedEntries += entryObjects[i].GetEntryString();
            }

            using (StreamWriter writer = new StreamWriter(writeDir + FILE_PATH_E))
            {
                writer.Write(input + appendedEntries);

                writer.Close();
            }
        }
    }
}
