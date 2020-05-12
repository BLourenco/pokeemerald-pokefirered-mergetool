﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    class TilesetMetatilesIncEditor
    {
        private const string FILE_PATH = @"\data\tilesets\metatiles.inc";

        private static List<TilesetMetatilesEntry> entryObjects = new List<TilesetMetatilesEntry>();

        public static void CreateObjectsFromEntries(string readDir)
        {
            StreamReader reader = new StreamReader(readDir + FILE_PATH);
            string input = reader.ReadToEnd();

            string[] splitString = { ".align 1\ngMetatiles_" };
            string[] entries = input.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i] == "\n\t" ||
                    entries[i] == "\t\n" ||
                    entries[i] == "\n" ||
                    entries[i] == "\t") continue; // Skip empty space at start of file

                entryObjects.Add(CreateObjectFromEntry(entries[i].Trim()));
            }
        }

        private static TilesetMetatilesEntry CreateObjectFromEntry(string entry)
        {
            string[] split = entry.Split(
                new string[] { "::" },
                StringSplitOptions.RemoveEmptyEntries);

            string name = split[0];
            bool isSecondaryTileset = entry.Contains("/secondary/");

            return new TilesetMetatilesEntry(name, isSecondaryTileset);
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