using DroneWars.Model;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.View
{
    public class UIRenderer
    {
        private World world;
        private GraphicsDevice gd;
        private Dictionary<string, Texture2D> textures;

        public UIRenderer(World world, GraphicsDevice gd, Dictionary<string, Texture2D> textures)
        {
            this.world = world;
            this.gd = gd;
            this.textures = textures;
        }


    }
}
