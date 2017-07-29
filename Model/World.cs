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

        public World()
        {
            Drones = new List<Drone>();
            Drones.Add(new Drone(new Vector2(50, 50)));
        }

        public void Update(float dTime)
        {
            Drones.ForEach(delegate (Drone drone)
            {
                drone.Update(dTime);
            });
        }
    }
}
