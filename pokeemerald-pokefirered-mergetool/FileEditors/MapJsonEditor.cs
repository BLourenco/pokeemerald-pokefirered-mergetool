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
    static class MapJsonEditor
    {
        private const string FILE_PATH = @"\data\maps";
        private const string FILE_NAME = @"\map.json";
        private const string PREFIX = "FR_";

        private static List<Map> mapFiles = new List<Map>();

        public static void UpdateAllFireRedMapFiles(string sourceDir)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir + FILE_PATH);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDir);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            Map mj;
            for (int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].Name.Contains(PREFIX))
                {
                    mj = CreateMapObjectFromJson(dirs[i].FullName);
                    mj.UpdateJsonWithPrefixes();
                    WriteToFile(dirs[i].FullName, mj);
                }
            }
        }

        private static Map CreateMapObjectFromJson(string readDir)
        {
            string jsonString = File.ReadAllText(readDir + FILE_NAME);

            return JsonConvert.DeserializeObject<Map>(jsonString);
        }

        public static void WriteToFile(string writeDir, Map mj)
        {
            using (StreamWriter writer = new StreamWriter(writeDir + FILE_NAME))
            {
                string json = JsonConvert.SerializeObject(mj);
                writer.Write(JToken.Parse(json).ToString(Formatting.Indented));

                writer.Close();
            }
        }
    }
}
