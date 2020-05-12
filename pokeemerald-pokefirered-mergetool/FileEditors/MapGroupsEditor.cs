using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace pokeemerald_pokefirered_mergetool
{
    class MapGroupsEditor
    {
        private const string FILE_PATH = @"\data\maps\map_groups.json";
        private const string PREFIX = "FR_";
        private const int NEW_MAP_GROUP_START_INDEX = 34;

        private static List<string[]> mapGroupContents = new List<string[]>();

        public static void CreateObjectsFromEntries(string readDir)
        {
            string jsonString = File.ReadAllText(readDir + FILE_PATH);

            int startIndex = jsonString.IndexOf("],\n  ");
            jsonString = jsonString.Remove(0, startIndex+5);

            string[] s = jsonString.Split(new string[1] { "\n  ],\n  " }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < s.Length-1; i++) // Ignore last entry of connections
            {
                s[i] = s[i].Replace("\"", "");

                int index = s[i].IndexOf("    ");
                s[i] = s[i].Substring(index + 4);

                mapGroupContents.Add(s[i].Split(new string[1] { ",\n    " }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        public static void WriteEntriesToFile(string writeDir)
        {
            StreamReader reader = new StreamReader(writeDir + FILE_PATH);
            string fileText = reader.ReadToEnd();
            reader.Close();

            string[] fileTextChunks = new string[3];

            int indexEndOfGroupOrder = fileText.IndexOf("\n  ],");
            int indexEndOfMapGroups = fileText.LastIndexOf("],");

            fileTextChunks[0] = fileText.Substring(0, indexEndOfGroupOrder);
            fileTextChunks[1] = fileText.Substring(indexEndOfGroupOrder + 5, indexEndOfMapGroups - indexEndOfGroupOrder);
            fileTextChunks[2] = fileText.Substring(indexEndOfMapGroups + 2);

            string groupOrderNewGroups = "";
            string newMapGroups = "";

            // Add to group order list, and add new map groups
            for (int i = 0; i < mapGroupContents.Count; i++)
            {
                // Add to group order list
                groupOrderNewGroups += ",\n    \"gMapGroup" + (NEW_MAP_GROUP_START_INDEX + i) + "\"";

                //add new map groups
                newMapGroups += "\n" +
                    "  \"gMapGroup" + (NEW_MAP_GROUP_START_INDEX + i) + "\": [\n";

                for (int j = 0; j < mapGroupContents[i].Length; j++)
                {
                    newMapGroups += "    \"" + PREFIX + mapGroupContents[i][j] + "\"\n";
                }

                newMapGroups += "  ],";
            }

            groupOrderNewGroups += "\n  ],";

            using (StreamWriter writer = new StreamWriter(writeDir + FILE_PATH))
            {
                writer.Write(fileTextChunks[0] + groupOrderNewGroups + fileTextChunks[1] + newMapGroups + fileTextChunks[2]);

                writer.Close();
            }
        }
    }
}
