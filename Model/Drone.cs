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
        public Vector2 Pos { get { return pos; } }

        private Vector2 pos;
        private float dTime;

        public Drone(Vector2 startPos)
        {
            pos = startPos;
        }

        public void Update(float dTime)
        {
            this.dTime = dTime;
        }

        public void Up()
        {
            pos.Y -= dTime;
        }

        public void Down()
        {
            pos.Y += dTime;
        }

        public void Left()
        {
            pos.X -= dTime;
        }

        public void Right()
        {
            pos.X += dTime;
        }
    }
}
