using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool.Core
{
    class Palette
    {
        string jasc;
        string unknown;
        string numOfColors;
        List<Color> colors;

        public Palette (string paletteText)
        {
            string[] splits = paletteText.Replace("\r","").Split('\n');
            jasc = splits[0];
            unknown = splits[1];
            numOfColors = splits[2];

            colors = new List<Color>();

            for (int i = 3; i < splits.Length; i++) // Skip the first few lines
            {
                string[] rgb = splits[i].Split(' ');

                if (rgb.Length == 1) continue; // Skip blank/last lines

                colors.Add(
                    Color.FromArgb(
                        int.Parse(rgb[0]), 
                        int.Parse(rgb[1]), 
                        int.Parse(rgb[2])));
            }
        }

        public string GetPaletteText()
        {
            string output =
                jasc + "\n" +
                unknown + "\n" +
                numOfColors + "\n";

            for (int i = 0; i < colors.Count; i++)
            {
                output += 
                    colors[i].R + " " +
                    colors[i].G + " " +
                    colors[i].B + "\n";
            }

            return output;
        }
    }
}
