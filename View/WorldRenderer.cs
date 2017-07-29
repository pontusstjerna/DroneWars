using DroneWars.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.View
{
    public class WorldRenderer
    {
        private World world;
        private GraphicsDevice gd;
        private Texture2D background;
        private List<Texture2D> drones;
        private Dictionary<BlockType, Texture2D> blocks;
        private float scale;

        public WorldRenderer(World world, GraphicsDevice gd, Texture2D background, Dictionary<BlockType, Texture2D> blocks, List<Texture2D> drones)
        {
            this.world = world;
            this.gd = gd;
            this.background = background;
            this.blocks = blocks;
            this.drones = drones;

            scale = Math.Min((float)gd.Viewport.Height / World.HEIGHT, (float)gd.Viewport.Width / World.WIDTH);
        }

        public void Render(SpriteBatch sb, float dTime)
        {
            RenderBackground(sb);
            RenderBlocks(sb);
            RenderDrones(sb);
        }

        private void RenderBackground(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(
                background,
                Vector2.Zero,
                null,
                Color.White,
                0,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                0);
            sb.End();
        }

        private void RenderBlocks(SpriteBatch sb)
        {
            sb.Begin();
            foreach(Block block in world.Blocks)
            {
                RenderBlock(sb, block);    
            }
            sb.End();
        }

        private void RenderBlock(SpriteBatch sb, Block block)
        {
            if (blocks.TryGetValue(block.Type, out Texture2D texture))
            {
                for (float x = 0; x < block.Rect.Width * scale; x += texture.Width)
                {
                    for (float y = 0; y < block.Rect.Height * scale; y += texture.Height)
                    {
                        RenderBlockPiece(sb, texture, x + block.Rect.X * scale, y + block.Rect.Y * scale, 
                            (int)Math.Min(texture.Width, block.Rect.Width*scale - x), 
                            (int)Math.Min(texture.Height, block.Rect.Height * scale - y));
                    }
                }
            }
        }

        private void RenderBlockPiece(SpriteBatch sb, Texture2D texture, float x, float y, int width, int height)
        {
            sb.Draw(texture, new Vector2(x, y), new Rectangle(0, 0, width, height), Color.White);
        }

        private void RenderDrones(SpriteBatch sb)
        {
            sb.Begin();
            for(int i = 0; i < world.Drones.Count; i++)
            {
                sb.Draw(drones[i], world.Drones[i].Pos * scale, Color.White);
            }
            sb.End();
        }
    } 
}
