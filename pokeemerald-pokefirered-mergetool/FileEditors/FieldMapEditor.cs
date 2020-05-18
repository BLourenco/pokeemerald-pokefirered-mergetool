using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool
{
    static class FieldMapEditor
    {
        private const string FILE_PATH = @"\include\fieldmap.h";
        private const string NUM_PALS_TOTAL_FIND = "#define NUM_PALS_TOTAL 13";
        private const string NUM_PALS_TOTAL_REPLACE = "#define NUM_PALS_TOTAL 14";

        public static void EditFieldMapPaletteTotal(string dir)
        {
            string input = File.ReadAllText(dir + FILE_PATH);
            File.WriteAllText(dir + FILE_PATH, input.Replace(NUM_PALS_TOTAL_FIND, NUM_PALS_TOTAL_REPLACE));
        }
    }
}
