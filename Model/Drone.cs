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
        public int ID { get; private set; }

        private const int hoverLimit = 30;
        private const int accVertical = 200;
        private const int accHorizontal = 150;
        private const float bounceFriction = 0.5f;
        private const float airResistance = 1f;

        private Vector2 pos;
        private Vector2 velocity;
        private float dTime;

        private Random random;

        public Drone(Vector2 startPos, int id)
        {
            pos = startPos;
            ID = id;
            random = new Random();
        }

        public void Update(float dTime)
        {
            this.dTime = dTime;
            pos += velocity * dTime;

            velocity *= 1 - airResistance * dTime;

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

        internal void Bounce()
        {
            velocity.Y = -velocity.Y*bounceFriction;
        }

        private void Hover()
        {
            if(velocity.Length() < hoverLimit)
            {
                velocity.Y += (float)random.NextDouble()*10 - 0.5f*10;
                velocity.X += (float)random.NextDouble()*10 - 0.5f*10;
            }
        }
    }
}
