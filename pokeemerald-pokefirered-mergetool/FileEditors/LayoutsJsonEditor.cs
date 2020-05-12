using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class LayoutsJsonEditor
    {
        private const string FILE_PATH = @"\data\layouts\layouts.json";

        private static List<LayoutsJsonEntry> layoutEntries;

        public static void CreateObjectsFromEntries(string readDir)
        {
            string jsonString = File.ReadAllText(readDir + FILE_PATH);
            jsonString = '[' + jsonString.Split('[', ']')[1] + ']';

            layoutEntries = JsonConvert.DeserializeObject<List<LayoutsJsonEntry>>(jsonString);
            
        }

        public static void WriteEntriesToFile(string writeDir)
        {
            StreamReader reader = new StreamReader(writeDir + FILE_PATH);
            string input = reader.ReadToEnd();
            reader.Close();

            // Temporaily remove ending brackets
            string endBrackets = "\n  ]\n}";
            input = input.Replace(endBrackets, "");

            string appendedEntries = "";

            for (int i = 0; i < layoutEntries.Count; i++)
            {
                if (layoutEntries[i].id == null) continue; // Empty entry, skip

                layoutEntries[i].ApplyPrefix();

                string json = JsonConvert.SerializeObject(layoutEntries[i]);
                appendedEntries += "," + JToken.Parse(json).ToString(Formatting.Indented);
            }

            using (StreamWriter writer = new StreamWriter(writeDir + FILE_PATH))
            {
                writer.Write(input + appendedEntries + endBrackets);

                writer.Close();
            }
        }
    }
}
