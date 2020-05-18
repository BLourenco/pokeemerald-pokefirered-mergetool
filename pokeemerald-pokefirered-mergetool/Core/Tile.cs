using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokeemerald_pokefirered_mergetool.Core
{
    class Tile
    {
        const ushort MASK_TILE = 0x3ff;
        const ushort MASK_FLIP_X = 0x1;
        const ushort MASK_FLIP_Y = 0x1;
        const ushort MASK_PALETTE = 0xf;

        const ushort SHIFT_FLIP_X = 10;
        const ushort SHIFT_FLIP_Y = 11;
        const ushort SHIFT_PALETTE = 12;

        public ushort tile { get; private set; }
        public ushort flipX { get; private set; }
        public ushort flipY { get; private set; }
        public ushort palette { get; private set; }

        public Tile(ushort tile, ushort xFlip, ushort yFlip, ushort pallete)
        {
            this.tile = tile;
            this.flipX = xFlip;
            this.flipY = yFlip;
            this.palette = pallete;
        }

        public Tile(ushort tileData)
        {
            this.tile = (ushort)(tileData & MASK_TILE);
            this.flipX = (ushort)((tileData >> SHIFT_FLIP_X) & MASK_FLIP_X);
            this.flipY = (ushort)((tileData >> SHIFT_FLIP_Y) & MASK_FLIP_Y);
            this.palette = (ushort)((tileData >> SHIFT_PALETTE) & MASK_PALETTE);
        }

        public List<byte> GetBytes()
        {
            List<byte> bytes = new List<byte>();

            ushort value = (ushort)(
                (tile & MASK_TILE) +
                ((flipX & MASK_FLIP_X) << SHIFT_FLIP_X) +
                ((flipY & MASK_FLIP_Y) << SHIFT_FLIP_Y) +
                ((palette & MASK_PALETTE) << SHIFT_PALETTE));

            bytes.Add((byte)(value & 0xff));
            bytes.Add((byte)((value >> 8) & 0xff));

            return bytes;
        }
    }
}
