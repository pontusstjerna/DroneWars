using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.Model
{
    public class World
    {
        public List<Drone> Drones { get; private set; }
        public List<Block> Blocks { get; private set; }
        public const int WIDTH  = 1000;
        public const int HEIGHT = 600;

        public World()
        {
            SpawnDrones();

            Blocks = new List<Block>
            {
                new Block(BlockType.STONE, new Point(0, 500), WIDTH, 100),
                new Block(BlockType.STONE, new Point(0, 0), WIDTH, 10)
            };
        }

        public void Update(float dTime)
        {
            Drones.ForEach(delegate (Drone drone)
            {
                drone.Update(dTime);
            });
        }

        public void Reset()
        {
            SpawnDrones();
        }

        private void SpawnDrones()
        {
            Drones = new List<Drone>
            {
                new Drone(new Vector2(50, 50))
            };
        }
    }
}
