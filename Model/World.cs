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
            Blocks = new List<Block>
            {
                new Block(BlockType.STONE, new Point(0, 500), WIDTH, 100),
                new Block(BlockType.STONE, new Point(0, 0), WIDTH, 50),
                new Block(BlockType.STONE, new Point(0, 0), 20, HEIGHT),
                new Block(BlockType.STONE, new Point(WIDTH - 20, 0), 20, HEIGHT),

            };

            SpawnDrones();
        }

        public void Update(float dTime)
        {
            for(int i = 0; i < Drones.Count; i++)
            {
                Drones[i].Update(dTime);
                CollideBlocks(Drones[i]);
                CollideDrones(Drones[i]);
            }
        }

        public void Reset()
        {
            SpawnDrones();
        }

        private void SpawnDrones()
        {
            Drones = new List<Drone>
            {
                SpawnDrone(0),
                SpawnDrone(1)
            };
        }

        private Drone SpawnDrone(int droneId)
        {
            float spawnY = Blocks[0].Rect.Y - Drone.HEIGHT;
            switch (droneId)
            {
                case 0:
                    return new Drone(new Vector2(WIDTH / 3, spawnY), droneId);
                case 1:
                    return new Drone(new Vector2(2 * WIDTH / 3, spawnY), droneId);
                default:
                    return null;
            }
        }

        private void CollideDrones(Drone drone)
        {
            for(int i = 0; i < Drones.Count; i++)
            {
                if(i != drone.ID)
                {
                    Rectangle drone1 = new Rectangle((int)(drone.Pos.X), (int)(drone.Pos.Y), Drone.WIDTH, Drone.HEIGHT);
                    Rectangle drone2 = new Rectangle((int)(Drones[i].Pos.X), (int)(Drones[i].Pos.Y), Drone.WIDTH, Drone.HEIGHT);

                    if (drone.Pos.Y < Drones[i].Pos.Y && drone1.Intersects(drone2))
                    {
                        Drones[i] = SpawnDrone(i);
                    }
                }
            }
        }

        private void CollideBlocks(Drone drone)
        {
            foreach (Block block in Blocks)
            {
                if (IsBetweenHorizontally(drone, block))
                {
                    if (drone.Pos.Y + Drone.HEIGHT >= block.Rect.Y && drone.Pos.Y < block.Rect.Y)
                    {
                        drone.Bounce();
                        continue;
                    }

                    if (drone.Pos.Y < block.Rect.Y + block.Rect.Height && drone.Pos.Y + Drone.HEIGHT > block.Rect.Y + block.Rect.Height)
                    {
                        Drones[drone.ID] = SpawnDrone(drone.ID);
                    }
                }

                if (new Rectangle((int)(drone.Pos.X), (int)(drone.Pos.Y), Drone.WIDTH, Drone.HEIGHT).Intersects(block.Rect))
                {
                    Drones[drone.ID] = SpawnDrone(drone.ID);
                }
            }
        }

        private bool IsBetweenHorizontally(Drone drone, Block block)
        {
            return drone.Pos.X < block.Rect.X + block.Rect.Width && drone.Pos.X + Drone.WIDTH > block.Rect.X;
        }
    }
}
