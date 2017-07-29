using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.Model
{
    public enum BlockType { STONE, GRASS, ICE}

    public class Block
    {
        public BlockType Type { get; private set; }
        public Rectangle Rect { get; private set; }

        public Block(BlockType type, Point pos, int width, int height)
        {
            Type = type;
            Rect = new Rectangle(pos, new Point(width, height));
        }
    }
}
