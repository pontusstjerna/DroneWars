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
        public const int NUM_FRAMES = 4;

        private World world;
        private GraphicsDevice gd;
        private Rectangle surface;
        private Texture2D background;
        private List<List<Texture2D>> drones;
        private Dictionary<BlockType, Texture2D> blocks;

        private float scale;
        private int[] frames;
        private float[] frameTimes;
        private float[] onGroundTimes;

        public WorldRenderer(World world, GraphicsDevice gd, Rectangle surface, Texture2D background, Dictionary<BlockType, Texture2D> blocks, List<List<Texture2D>> drones)
        {
            this.world = world;
            this.gd = gd;
            this.background = background;
            this.blocks = blocks;
            this.drones = drones;
            this.surface = surface;

            frames = new int[drones.Count];
            frameTimes = new float[drones.Count];
            onGroundTimes = new float[drones.Count];
            for (int i = 0; i < onGroundTimes.Length; i++)
                onGroundTimes[i] = 10000;

            scale = Math.Min((float)surface.Height / World.HEIGHT, (float)surface.Width / World.WIDTH);
        }

        public void Render(SpriteBatch sb, float dTime)
        {
            UpdateFrames(dTime);
            RenderBackground(sb);
            RenderBlocks(sb);
            RenderDrones(sb);
        }

        private void RenderBackground(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(
                background,
                surface,
                null, 
                Color.White
                );
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
            sb.Draw(texture, new Vector2(surface.X + x, surface.Y + y), new Rectangle(0, 0, width, height), Color.White);
        }

        private void RenderDrones(SpriteBatch sb)
        {
            sb.Begin();
            for(int i = 0; i < world.Drones.Count; i++)
            {
                Drone drone = world.Drones[i];
                Vector2 scaledPos = new Vector2(drone.Pos.X * scale + surface.X, drone.Pos.Y * scale + surface.Y);
                sb.Draw(drones[i][frames[i]], scaledPos, null, Color.White, drone.Tilt, drone.Origin*scale, 1, SpriteEffects.None, 0);
            }
            sb.End();
        }

        private void UpdateFrames(float dTime)
        {
            for(int i = 0; i < world.Drones.Count; i ++)
            {
                frameTimes[i] += dTime;
                if (world.Drones[i].OnGround)
                    onGroundTimes[i] += frameTimes[i] * 0.03f;
                else
                    onGroundTimes[i] = 0;

                if(frameTimes[i] > 0.01 + onGroundTimes[i])
                {
                    frames[i] = (frames[i] + 1) % NUM_FRAMES;
                    frameTimes[i] = 0;
                }
            }
        }
    } 
}
