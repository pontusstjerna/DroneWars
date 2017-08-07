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
        public const int WIDTH  = 800;
        public const int HEIGHT = 380;

        private Vector2[] spawnPositions;

        public World()
        {
            Blocks = new List<Block>
            {
                new Block(BlockType.ICE, new Point(0, HEIGHT - 50), WIDTH, 100),
                new Block(BlockType.STONE, new Point(0, 0), WIDTH, 20),
                new Block(BlockType.STONE, new Point(0, 0), 20, HEIGHT),
                new Block(BlockType.STONE, new Point(WIDTH - 20, 0), 20, HEIGHT),
                new Block(BlockType.ICE, new Point(WIDTH/3, HEIGHT/3), 100, 40)

            };

            spawnPositions = new Vector2[] 
            {
                new Vector2(WIDTH / 3, Blocks[0].Rect.Y - Drone.HEIGHT),
                new Vector2(2 * WIDTH / 3, Blocks[0].Rect.Y - Drone.HEIGHT)
            };

            SpawnDrones();
        }

        public void Update(float dTime)
        {
            foreach(Drone drone in Drones)
            {
                drone.Update(dTime);
                CollideBlocks(drone);
                CollideDrones(drone);
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
                new Drone(spawnPositions[0], 0),
                new Drone(spawnPositions[1], 1)
            };
        }
        
        private void CollideDrones(Drone drone)
        {
            for(int i = 0; i < Drones.Count; i++)
            {
                if(i != drone.ID)
                {
                    Rectangle drone1 = new Rectangle((int)(drone.Pos.X - Drone.WIDTH/2), (int)(drone.Pos.Y), Drone.WIDTH, Drone.HEIGHT);
                    Rectangle drone2 = new Rectangle((int)(Drones[i].Pos.X - Drone.WIDTH/2), (int)(Drones[i].Pos.Y), Drone.WIDTH, Drone.HEIGHT);

                    if (drone.Pos.Y < Drones[i].Pos.Y && drone1.Intersects(drone2))
                    {
                        if (Drones[i].OnGround)
                        {
                            Drones[drone.ID].Destroy(spawnPositions[drone.ID]);
                            Drones[i].AddScore();
                        }
                        else
                        {
                            Drones[i].Destroy(spawnPositions[Drones[i].ID]);
                            drone.AddScore();
                        }
                    }
                }
            }
        }

        private void CollideBlocks(Drone drone)
        {
            bool onGround = false;
            foreach (Block block in Blocks)
            {
                if (IsBetweenHorizontally(drone, block))
                {
                    if (drone.Pos.Y + Drone.HEIGHT >= block.Rect.Y && drone.Pos.Y < block.Rect.Y)
                    {
                        drone.Bounce();
                        onGround = true;
                        continue;
                    }
                    
                    if (drone.Pos.Y < block.Rect.Y + block.Rect.Height && drone.Pos.Y + Drone.HEIGHT > block.Rect.Y + block.Rect.Height)
                    {
                        Drones[drone.ID].Destroy(spawnPositions[drone.ID]);
                    }
                }

                if (new Rectangle((int)(drone.Pos.X - Drone.WIDTH/2), (int)(drone.Pos.Y), Drone.WIDTH, Drone.HEIGHT).Intersects(block.Rect))
                {
                    Drones[drone.ID].Destroy(spawnPositions[drone.ID]);
                }
            }

            drone.OnGround = onGround;
        }

        private bool IsBetweenHorizontally(Drone drone, Block block)
        {
            return drone.Pos.X - Drone.WIDTH/2 < block.Rect.X + block.Rect.Width && drone.Pos.X + Drone.WIDTH/2 > block.Rect.X;
        }
    }
}
