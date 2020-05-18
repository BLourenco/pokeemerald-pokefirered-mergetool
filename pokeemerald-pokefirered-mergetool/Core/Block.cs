using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool.Core
{
    class Block
    {
        const ushort MASK_METATILE = 0x3ff;
        const ushort MASK_COLLISION = 0x3;
        const ushort MASK_ELEVATION = 0xf;

        const ushort SHIFT_COLLISION = 10;
        const ushort SHIFT_ELEVATION = 12;

        ushort metatile;
        ushort collision;
        ushort elevation;
        
        public Block(ushort tile, ushort collision, ushort elevation)
        {
            this.metatile = tile;
            this.collision = collision;
            this.elevation = elevation;
        }

        public Block(ushort blockValue)
        {
            metatile = (ushort)(blockValue & MASK_METATILE);
            collision = (ushort)((blockValue >> SHIFT_COLLISION) & MASK_COLLISION);
            elevation = (ushort)((blockValue >> SHIFT_ELEVATION) & MASK_ELEVATION);
        }

        public static List<Block> CreateBlocks(List<byte> blockData)
        {
            List<Block> blocks = new List<Block>();

            for (int i = 0; i < blockData.Count;)
            {
                ushort s = blockData[i++];
                s += (ushort)(blockData[i++] << 8);
                blocks.Add(new Block(s));
            }

            return blocks;
        }

        public ushort GetRawValue()
        {
            return (ushort)(
                (metatile & MASK_METATILE) +
                ((collision & MASK_COLLISION) << SHIFT_COLLISION) +
                ((elevation & MASK_ELEVATION) << SHIFT_ELEVATION));
        }
    }
}
