using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class EventScriptsEditor
    {
        private const string FILE_PATH = @"\data\event_scripts.s";
        private const string ENTRY_START = "\t.include \"data/maps/";
        private const string PREFIX = "FR_";

        private static List<string> entryObjects = new List<string>();

        public static void CreateObjectsFromEntries(string readDir)
        {
            string input = File.ReadAllText(readDir + FILE_PATH);

            string[] splitString = { ENTRY_START };
            string[] entries = input.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < entries.Length; i++) // Skip first split of file
            {
                if (i == entries.Length - 1) // Cut off the rest of the file on the last entry
                {
                    entries[i] = entries[i].Split('\n')[0];
                }
                
                entryObjects.Add(ENTRY_START + PREFIX + entries[i]);
            }
        }

        public static void WriteEntriesToFile(string writeDir)
        {
            string input = File.ReadAllText(writeDir + FILE_PATH);

            string appendedEntries = "\n";

            for (int i = 0; i < entryObjects.Count; i++)
            {
                appendedEntries += entryObjects[i];
            }

            using (StreamWriter writer = new StreamWriter(writeDir + FILE_PATH))
            {
                writer.Write(input + appendedEntries);

                writer.Close();
            }
        }
    }
}
