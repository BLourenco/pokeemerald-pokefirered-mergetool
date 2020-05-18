using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool.Core
{
    class Tileset
    {
        #region String Constants
        const string TEMPLATE_HEADERS =
            "\n\t.align 2\n" +
            "{0}::\n" +
            "\t.byte {1} @ is compressed\n" +
            "\t.byte {2} @ is secondary\n" +
            "\t.2byte 0 @ padding\n" +
            "\t.4byte {3}\n" +
            "\t.4byte {4}\n" +
            "\t.4byte {5}\n" +
            "\t.4byte {6}\n" +
            "\t.4byte {7}\n";

        const string TEMPLATE_GRAPHICS_START =
            "\n\t.align 2\n" +
            "{0}::\n";

        const string TEMPLATE_METATILES_START =
            "\n\t.align 1\n" +
            "{0}::\n";

        const string TEMPLATE_PATH =
            "\t.incbin \"{0}\"\n";

        const string LABEL_TILESET = "gTileset_";
        const string LABEL_TILESET_TILES = "gTilesetTiles_";
        const string LABEL_TILESET_PALETTES = "gTilesetPalettes_";
        const string LABEL_METATILES = "gMetatiles_";
        const string LABEL_METATILE_ATTRIBUTES = "gMetatileAttributes_";
        const string LABEL_INIT_TILESET_ANIM = "InitTilesetAnim_";

        const string PATH_PRIMARY = @"data\tilesets\primary\";
        const string PATH_SECONDARY = @"data\tilesets\secondary\";
        const string PATH_HEADERS = @"data\tilesets\headers.inc";
        const string PATH_GRAPHICS = @"data\tilesets\graphics.inc";
        const string PATH_METATILES = @"data\tilesets\metatiles.inc";
        #endregion

        const int LIMIT_PRIMARY_TILES = 512;
        readonly Rectangle TARGET_PRIMARY_TOP = new Rectangle(0, 0, 128, 256);
        readonly Rectangle TARGET_PRIMARY_BOTTOM = new Rectangle(0, 0, 128, 64);
        readonly Rectangle SOURCE_PRIMARY_BOTTOM = new Rectangle(0, 256, 128, 64);

        // header.inc
        public string name { get; private set; }
        public string is_compressed { get; private set; }
        public string is_secondary { get; private set; }
        public string tiles_label { get; private set; }
        public string palettes_label { get; private set; }
        public string metatiles_label { get; private set; }
        public string metatile_attrs_label { get; private set; }
        public string callback_label { get; private set; }

        // graphics.inc
        public string tilesPath { get; private set; }
        public List<string> palettePaths { get; private set; }


        // metatiles.inc
        public string metatiles_path { get; private set; }
        public string metatile_attrs_path { get; private set; }

        // Data
        public Image tilesImage { get; private set; }
        public List<Metatile> metatiles { get; private set; }
        public List<Palette> palettes { get; private set; }

        public Tileset(string headerEntry)
        {
            string[] lines = headerEntry.Split('\n');

            this.name = lines[0].Substring(0, headerEntry.IndexOf(":"));
            this.is_compressed = lines[1].Contains("TRUE") ? "TRUE" : "FALSE";
            this.is_secondary = lines[2].Contains("TRUE") ? "TRUE" : "FALSE";
            this.tiles_label = lines[4].Substring(8);
            this.palettes_label = lines[5].Substring(8);
            this.metatiles_label = lines[6].Substring(8);
            this.callback_label = lines[7].Substring(8);
            this.metatile_attrs_label = lines[8].Substring(8);

            palettePaths = new List<string>();
        }

        private string GetPrefixedPath(string path)
        {
            int insertIndex = is_secondary == "FALSE" ? PATH_PRIMARY.Length : PATH_SECONDARY.Length;

            return path.Insert(insertIndex, MergeTool.PREFIX);
        }

        public static List<Tileset> CreateTilesets(string readPath)
        {
            string headerIncText = File.ReadAllText(readPath + @"\data\tilesets\headers.inc");
            string graphicsIncText = File.ReadAllText(readPath + @"\data\tilesets\graphics.inc");
            string metatilesIncText = File.ReadAllText(readPath + @"\data\tilesets\metatiles.inc");

            List<Tileset> tilesets = new List<Tileset>();

            // Split header entries
            string[] splitString = { "\n\t.align 2\n" };
            string[] headerEntries = headerIncText.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            // Split graphics entries
            string[] graphicsEntries = graphicsIncText.Split(splitString, StringSplitOptions.RemoveEmptyEntries);

            // Split metatiles entries
            string[] splitStringMetatiles = { "\n\t.align 1\n" };
            string[] metatilesEntries = metatilesIncText.Split(splitStringMetatiles, StringSplitOptions.RemoveEmptyEntries);

            // Get data from .inc files
            for (int i = 0; i < headerEntries.Length; i++)
            {
                // Create tileset from header.inc
                Tileset tileset = new Tileset(headerEntries[i]);


                // Add paths from graphics.inc
                bool setTiles = false;
                bool setPalettes = false;

                for (int j = 0; j < graphicsEntries.Length; j++)
                {
                    if (graphicsEntries[j].Contains(tileset.tiles_label))
                    {
                        tileset.tilesPath = graphicsEntries[j].Substring(
                            graphicsEntries[j].IndexOf("\"")).Replace("\"", "").Replace("\n", "");
                        setTiles = true;
                    }
                    else if (graphicsEntries[j].Contains(tileset.palettes_label))
                    {
                        string[] splitStringPalettes = { "\n\t.incbin " };
                        string[] lines = graphicsEntries[j].Split(splitStringPalettes, StringSplitOptions.RemoveEmptyEntries);

                        for (int k = 1; k < lines.Length; k++) // Skip first blank entry
                        {
                            tileset.palettePaths.Add(lines[k].Replace("\"", "").Replace("\n", ""));
                        }

                        setPalettes = true;
                    }

                    if (setTiles && setPalettes) break;
                }

                // Add paths from metatiles.inc
                bool setMetatiles = false;
                bool setMetatilesAttributes = false;

                for (int j = 0; j < metatilesEntries.Length; j++)
                {
                    if (metatilesEntries[j].Contains(tileset.metatiles_label))
                    {
                        tileset.metatiles_path = metatilesEntries[j].Substring(
                            metatilesEntries[j].IndexOf("\"")).Replace("\"", "").Replace("\n", "");
                        setMetatiles = true;
                    }
                    else if (metatilesEntries[j].Contains(tileset.metatile_attrs_label))
                    {
                        tileset.metatile_attrs_path = metatilesEntries[j].Substring(
                            metatilesEntries[j].IndexOf("\"")).Replace("\"", "").Replace("\n", "");

                        setMetatilesAttributes = true;
                    }

                    if (setMetatiles && setMetatilesAttributes) break;
                }

                // WORKAROUND: Some entries are not in graphics.inc but in graphics.c, just add them using the same path structure
                if (!setTiles)
                {
                    tileset.tilesPath = tileset.metatiles_path.Replace("metatiles.bin", "tiles.png");
                }
                if (!setPalettes)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        tileset.palettePaths.Add(tileset.metatiles_path.Replace("metatiles.bin", "palettes/" + j.ToString("D2") + ".pal"));
                    }
                }

                // Assign image of tiles
                //if (tileset.tilesPath != null) /// TODO: Add fallback, either read from graphics.c or generate paths
                {
                    tileset.tilesImage = Image.FromFile(readPath + @"\" + tileset.tilesPath.Replace("4bpp", "png").Replace(".lz", ""));

                    // Assign palettes
                    tileset.palettes = new List<Palette>();
                    for (int j = 0; j < tileset.palettePaths.Count; j++)
                    {
                        tileset.palettes.Add(new Palette(File.ReadAllText(readPath + @"\" + tileset.palettePaths[j].Replace("gbapal", "pal"))));
                    }
                }

                // Create metatiles
                List<byte> metatileBytes = File.ReadAllBytes(readPath + @"\" + tileset.metatiles_path).ToList<byte>();
                List<byte> metatileAttributeBytes = File.ReadAllBytes(readPath + @"\" + tileset.metatile_attrs_path).ToList<byte>();

                tileset.metatiles = Metatile.CreateMetatiles(metatileBytes, metatileAttributeBytes);

                tilesets.Add(tileset);
            }

            return tilesets;
        }

        public static void WriteTilesetsToIncFiles(string writePath, List<Tileset> tilesets)            
        {
            string headersText = File.ReadAllText(writePath + "\\" + PATH_HEADERS);
            string graphicsText = File.ReadAllText(writePath + "\\" + PATH_GRAPHICS);
            string metatilesText = File.ReadAllText(writePath + "\\" + PATH_METATILES);

            for (int i = 0; i < tilesets.Count; i++)
            {
                // .inc text
                headersText += tilesets[i].GetTilesetHeadersText(true);
                graphicsText += tilesets[i].GetTilesetGraphicsText(true);
                metatilesText += tilesets[i].GetTilesetMetatilesText(true);

                // metatiles and metatile_attirbutes
                List<byte> metatileBytes = new List<byte>();
                List<byte> metatileAttributeBytes = new List<byte>();
                for (int j = 0; j < tilesets[i].metatiles.Count; j++)
                {
                    metatileBytes.AddRange(tilesets[i].metatiles[j].GetMetatileBytes());
                    metatileAttributeBytes.AddRange(tilesets[i].metatiles[j].GetMetatileAttributeBytes(MergeTool.Version.Emerald));
                }

                File.WriteAllBytes(writePath + "\\" + tilesets[i].GetPrefixedPath(tilesets[i].metatiles_path), metatileBytes.ToArray());
                File.WriteAllBytes(writePath + "\\" + tilesets[i].GetPrefixedPath(tilesets[i].metatile_attrs_path), metatileAttributeBytes.ToArray());

                // tiles.png
                tilesets[i].tilesImage.Save(writePath + "\\" + tilesets[i].GetPrefixedPath(tilesets[i].tilesPath), ImageFormat.Png);
            }

            File.WriteAllText(writePath + "\\" + PATH_HEADERS, headersText);
            File.WriteAllText(writePath + "\\" + PATH_GRAPHICS, graphicsText);
            File.WriteAllText(writePath + "\\" + PATH_METATILES, metatilesText);
        }

        public List<Metatile> SliceMetatiles()
        {
            List<Metatile> slicedMetaTiles = new List<Metatile>();

            for (int i = LIMIT_PRIMARY_TILES; i < this.metatiles.Count; i++)
            {
                slicedMetaTiles.Add(metatiles[i]);
            }

            metatiles.RemoveRange(LIMIT_PRIMARY_TILES, metatiles.Count - LIMIT_PRIMARY_TILES);

            return slicedMetaTiles;
        }

        public Image SliceTilesImage()
        {
            Bitmap topHalf;
            using (Bitmap primary128x256 = new Bitmap(TARGET_PRIMARY_TOP.Width, TARGET_PRIMARY_TOP.Height))
            {
                using (Graphics g = Graphics.FromImage(primary128x256))
                {
                    g.DrawImage(this.tilesImage, TARGET_PRIMARY_TOP, TARGET_PRIMARY_TOP, GraphicsUnit.Pixel);
                }

                topHalf = new Bitmap(primary128x256);
            }

            Bitmap bottomHalf;
            using (Bitmap primary128x64 = new Bitmap(TARGET_PRIMARY_BOTTOM.Width, TARGET_PRIMARY_BOTTOM.Height))
            {
                using (Graphics g = Graphics.FromImage(primary128x64))
                {
                    g.DrawImage(this.tilesImage, TARGET_PRIMARY_BOTTOM, SOURCE_PRIMARY_BOTTOM, GraphicsUnit.Pixel);
                }

                bottomHalf = new Bitmap(primary128x64);
            }

            this.tilesImage = topHalf;
            return bottomHalf;
        }

        public void StitchMetatiles(List<Metatile> metatiles)
        {
            this.metatiles.InsertRange(0, metatiles);
        }

        public void StitchTilesImage(Image slicedImage)
        {
            int combinedHeight = this.tilesImage.Height + slicedImage.Height;

            using (Bitmap secondary128x = new Bitmap(128, combinedHeight))
            {
                using (Graphics g = Graphics.FromImage(secondary128x))
                {
                    g.DrawImage(slicedImage, TARGET_PRIMARY_BOTTOM, TARGET_PRIMARY_BOTTOM, GraphicsUnit.Pixel);
                    g.DrawImage(this.tilesImage, 
                        new Rectangle(0,64, this.tilesImage.Width, this.tilesImage.Height),
                        new Rectangle(0, 0, this.tilesImage.Width, this.tilesImage.Height), 
                        GraphicsUnit.Pixel);
                }

                this.tilesImage = new Bitmap(secondary128x);
            }
        }

        private string GetTilesetHeadersText(bool usePrefix)
        {
            return string.Format(TEMPLATE_HEADERS,
                usePrefix ? name.Insert(LABEL_TILESET.Length, MergeTool.PREFIX) : name,
                is_compressed,
                is_secondary,
                usePrefix ? tiles_label.Insert(LABEL_TILESET_TILES.Length, MergeTool.PREFIX) : tiles_label,
                usePrefix ? palettes_label.Insert(LABEL_TILESET_PALETTES.Length, MergeTool.PREFIX) : palettes_label,
                usePrefix ? metatiles_label.Insert(LABEL_METATILES.Length, MergeTool.PREFIX) : metatiles_label,
                usePrefix ? metatile_attrs_label.Insert(LABEL_METATILE_ATTRIBUTES.Length, MergeTool.PREFIX) : metatile_attrs_label,
                callback_label == "0x0" ? "NULL" : usePrefix ? callback_label.Insert(LABEL_INIT_TILESET_ANIM.Length, MergeTool.PREFIX) : callback_label);
        }

        private string GetTilesetGraphicsText(bool usePrefix)
        {
            // Tileset Tiles
            string output = string.Format(TEMPLATE_GRAPHICS_START, usePrefix ? tiles_label.Insert(LABEL_TILESET_TILES.Length, MergeTool.PREFIX) : tiles_label);
            output += string.Format(TEMPLATE_PATH, usePrefix ? tilesPath.Insert(is_secondary == "FALSE" ? PATH_PRIMARY.Length : PATH_SECONDARY.Length, MergeTool.PREFIX) : tilesPath);

            // Tileset Palette
            output += string.Format(TEMPLATE_GRAPHICS_START, usePrefix ? palettes_label.Insert(LABEL_TILESET_PALETTES.Length, MergeTool.PREFIX) : palettes_label);

            for (int i = 0; i < palettePaths.Count; i++)
            {
                output += string.Format(TEMPLATE_PATH, usePrefix ? palettePaths[i].Insert(is_secondary == "FALSE" ? PATH_PRIMARY.Length : PATH_SECONDARY.Length, MergeTool.PREFIX) : palettePaths[i]);
            }

            return output;            
        }

        private string GetTilesetMetatilesText(bool usePrefix)
        {
            // Tileset Tiles
            string output = string.Format(TEMPLATE_METATILES_START, usePrefix ? metatiles_label.Insert(LABEL_METATILES.Length, MergeTool.PREFIX) : metatiles_label);
            output += string.Format(TEMPLATE_PATH, usePrefix ? metatiles_path.Insert(is_secondary == "FALSE" ? PATH_PRIMARY.Length : PATH_SECONDARY.Length, MergeTool.PREFIX) : metatiles_path);

            // Tileset Palette
            output += string.Format(TEMPLATE_METATILES_START, usePrefix ? metatile_attrs_label.Insert(LABEL_METATILE_ATTRIBUTES.Length, MergeTool.PREFIX) : metatile_attrs_label);
            output += string.Format(TEMPLATE_PATH, usePrefix ? metatile_attrs_path.Insert(is_secondary == "FALSE" ? PATH_PRIMARY.Length : PATH_SECONDARY.Length, MergeTool.PREFIX) : metatile_attrs_path);
            
            return output;
        }
    }
}
