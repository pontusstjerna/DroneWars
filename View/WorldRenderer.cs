using DroneWars.Model;
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
        private Texture2D foreground;

        public WorldRenderer(World world, GraphicsDevice gd, Texture2D background, Texture2D foreground)
        {
            this.world = world;
            this.gd = gd;
            this.background = background;
            this.foreground = foreground;
        }

        public void Render(SpriteBatch sb, float dTime)
        {

        }

        private void RenderBackground(SpriteBatch sb)
        {

        }
    } 
}
