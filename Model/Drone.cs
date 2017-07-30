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
        public const int WIDTH = 54;
        public const int HEIGHT = 25;

        public Vector2 Origin { get; private set; } = new Vector2(WIDTH / 2, 0);

        public Vector2 Pos { get { return pos; } }
        public Vector2 Vel { get { return velocity; } }
        public int ID { get; private set; }
        public float Tilt { get; private set; }

        internal bool OnGround { get; set; } = true;

        private const int accVertical = 300;
        private const int accHorizontal = 250;
        private const float bounceFriction = 0.5f;
        private const float airResistance = 1f;
        private const float maxTilt = (float)(Math.PI / 8);

        private Vector2 pos;
        private Vector2 velocity;
        private float dTime;
        private float wobble = 10;
        private bool released = false;

        private Random random;

        public Drone(Vector2 startPos, int id)
        {
            pos = startPos;
            ID = id;
            random = new Random(id);
        }

        public void Update(float dTime)
        {
            this.dTime = dTime;
            pos.X += velocity.X * dTime;
            if (!OnGround || velocity.Y < 0)
                pos.Y += velocity.Y * dTime;

            velocity *= 1 - airResistance * dTime;

            if (released)
                Tilt *= 0.95f;

            Hover();
        }

        public void Release()
        {
            released = true;
        }

        public void Up()
        {
            velocity.Y -= dTime*accVertical;
        }

        public void Down()
        {
            if(!OnGround)
                velocity.Y += dTime*accVertical;
        }

        public void Left()
        {
            velocity.X -= dTime*accHorizontal;

            if (Tilt > -maxTilt)
                Tilt -= dTime;
        }

        public void Right()
        {
            velocity.X += dTime*accHorizontal;

            if (Tilt < maxTilt)
                Tilt += dTime;
        }

        internal void Bounce()
        {
            velocity.Y = -velocity.Y * bounceFriction;
        }

        private void Hover()
        {
            if(released && !OnGround)
            {
                velocity.Y += (float)random.NextDouble()*wobble/2 - wobble/4;
                velocity.X += (float)random.NextDouble()*wobble - wobble/2;
            }
        }
    }
}
