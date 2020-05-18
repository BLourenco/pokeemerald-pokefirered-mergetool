using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool.Core
{
    class Metatile
    {
        public const int LAYERS = 2;
        public const int TILES_PER_LAYER = 4;

        // The tiles that make up a metatile
        public List<Tile> tiles;

        // Metatile attributes
        public ushort behavior;
        public ushort layerType;
        public ushort encounterType;
        public ushort terrainType;

        public string label;

        public Metatile()
        {
            tiles = new List<Tile>();
        }

        public List<byte> GetMetatileBytes()
        {
            List<byte> bytes = new List<byte>();

            for (int i = 0; i < LAYERS * TILES_PER_LAYER; i++)
            {
                List<byte> tileBytes = this.tiles[i].GetBytes();

                for (int j = 0; j < tileBytes.Count; j++)
                {
                    bytes.Add(tileBytes[j]);
                }
            }

            return bytes;
        }

        public List<byte> GetMetatileAttributeBytes(MergeTool.Version version)
        {
            List<byte> bytes = new List<byte>();

            if (version == MergeTool.Version.FireRed)
            {
                bytes.Add((byte)(this.behavior));
                bytes.Add((byte)((this.behavior >> 8) |
                            (byte)(this.terrainType << 1)));
                bytes.Add((byte)(0));
                bytes.Add((byte)((this.encounterType) |
                            (byte)(this.layerType << 5)));
            }
            else if (version == MergeTool.Version.Emerald)
            {
                bytes.Add((byte)(this.behavior));
                bytes.Add((byte)((this.layerType << 4) & 0xF0));
            }

            return bytes;
        }

        public static List<Metatile> CreateMetatiles(List<byte> metatileBytes, List<byte> metatileAttributeBytes)
        {
            // Create Metatiles
            int numMetatiles = metatileBytes.Count / 16;

            List<Metatile> metatiles = new List<Metatile>();
            for (int i = 0; i < numMetatiles; i++)
            {
                Metatile metatile = new Metatile();
                int index = i * (2 * TILES_PER_LAYER * LAYERS);
                for (int j = 0; j < TILES_PER_LAYER * LAYERS; j++)
                {
                    int word = metatileBytes[index++] & 0xff;
                    word += (metatileBytes[index++] & 0xff) << 8;
                    Tile tile = new Tile((ushort)word);

                    metatile.tiles.Add(tile);
                }
                metatiles.Add(metatile);
            }

            // Set metatile attributes
            int numMetatileAttrs = metatileAttributeBytes.Count / 4;

            for (int i = 0; i < numMetatileAttrs; i++)
            {
                int value = ((metatileAttributeBytes[i * 4 + 3]) << 24) |
                               ((metatileAttributeBytes[i * 4 + 2]) << 16) |
                                  ((metatileAttributeBytes[i * 4 + 1]) << 8) |
                                     ((metatileAttributeBytes[i * 4 + 0]));
                metatiles[i].behavior = (ushort)(value & 0x1FF);
                metatiles[i].terrainType = (ushort)((value & 0x3E00) >> 9);
                metatiles[i].encounterType = (ushort)((value & 0x7000000) >> 24);
                metatiles[i].layerType = (ushort)((value & 0x60000000) >> 29);
            }

            return metatiles;
        }
    }
}
