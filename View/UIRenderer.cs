using DroneWars.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private Dictionary<string, SpriteFont> fonts;

        public UIRenderer(World world, GraphicsDevice gd, ContentManager content)
        {
            this.world = world;
            this.gd = gd;
            textures = new Dictionary<string, Texture2D>
            {
                { "dashboard", content.Load<Texture2D>("ui/dashboard") }
            };

            fonts = new Dictionary<string, SpriteFont>
            {
                {"score", content.Load<SpriteFont>("fonts/score") }
            };
        }

        public void Render(SpriteBatch sb, float dTime)
        {
            RenderDashboard(sb);
            RenderScores(sb);
        }

        private void RenderDashboard(SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(textures["dashboard"], Vector2.Zero, Color.White);
            sb.End();
        }

        private void RenderScores(SpriteBatch sb)
        {
            sb.Begin();
            for(int i = 0; i < world.Drones.Count; i++)
            {
                Drone drone = world.Drones[i];
                Vector2 pos = new Vector2(50 + (i % 2) * 550, 20 + 40 * (i/2));

                sb.DrawString(fonts["score"], "Drone " + (drone.ID + 1) + ": ", pos, Color.Black);
            }
            sb.End();
        }
    }
}
