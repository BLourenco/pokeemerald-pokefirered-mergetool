using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    static class TilesetHeaderIncEditor
    {
        private const string FILE_PATH = @"\data\tilesets\headers.inc";

        private static List<TilesetHeaderEntry> entryObjects = new List<TilesetHeaderEntry>();

        public static void CreateObjectsFromEntries(string readDir)
        {
            StreamReader reader = new StreamReader(readDir + FILE_PATH);
            string input = reader.ReadToEnd();

            string[] splitString = { ".align 2\ngTileset_" };
            string[] entries = input.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i] == "\n\t") continue; // Skip empty space at start of file

                entryObjects.Add(CreateObjectFromEntry(entries[i].Trim()));
            }
        }

        private static TilesetHeaderEntry CreateObjectFromEntry(string entry)
        {
            string[] split = entry.Split(
                new string[] { "\t.byte " },
                StringSplitOptions.RemoveEmptyEntries);

            string name = split[0].Replace("::\n", "");
            string isCompressed = split[1].Split(' ')[0];
            string isSecondaryTileset = split[2].Split(' ')[0];

            bool callback = !entry.Contains("0x0") && !entry.Contains("NULL");

            return new TilesetHeaderEntry(name, isCompressed, isSecondaryTileset, callback);
        }

        public static void WriteEntriesToFile(string writeDir)
        {
            StreamReader reader = new StreamReader(writeDir + FILE_PATH);
            string input = reader.ReadToEnd();
            reader.Close();

            string appendedEntries = "";

            for (int i = 0; i < entryObjects.Count; i++)
            {
                appendedEntries += entryObjects[i].GetEntryString();
            }

            using (StreamWriter writer = new StreamWriter(writeDir + FILE_PATH))
            {
                writer.Write(input + appendedEntries);

                writer.Close();
            }
        }
    }
}
