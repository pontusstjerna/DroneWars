using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.Model
{
    public class Drone
    {
        public const int WIDTH = 50;
        public const int HEIGHT = 20;

        public Vector2 Pos { get { return pos; } }

        private const int hoverLimit = 10;
        private const int accVertical = 50;
        private const int accHorizontal = 30;

        private Vector2 pos;
        private Vector2 velocity;
        private float dTime;

        private Random random;

        public Drone(Vector2 startPos)
        {
            pos = startPos;
            random = new Random();
        }

        public void Update(float dTime)
        {
            this.dTime = dTime;
            pos += velocity * dTime;

            Hover();
        }

        public void Up()
        {
            velocity.Y -= dTime*accVertical;
        }

        public void Down()
        {
            velocity.Y += dTime*accVertical;
        }

        public void Left()
        {
            velocity.X -= dTime*accHorizontal;
        }

        public void Right()
        {
            velocity.X += dTime*accHorizontal;
        }

        private void Hover()
        {
            if(velocity.Length() < hoverLimit)
            {
                velocity.Y += (float)random.NextDouble() - 0.5f;
                velocity.X += (float)random.NextDouble() - 0.5f;
            }
        }
    }
}
