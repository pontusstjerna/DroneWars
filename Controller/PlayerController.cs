using DroneWars.Model;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DroneWars.Controller
{
    public class PlayerController
    {
        private World world;

        private KeyboardState oldState;

        public PlayerController(World world)
        {
            this.world = world;
        }

        public void Update()
        {
            CheckKeyboard();
        }

        private void CheckKeyboard()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.Up))
            {
                world.Drones[0].Up();
            }

            if (newState.IsKeyDown(Keys.Down))
            {
                world.Drones[0].Down();
            }

            if (newState.IsKeyDown(Keys.Right))
            {
                world.Drones[0].Right();
            }

            if (newState.IsKeyDown(Keys.Left))
            {
                world.Drones[0].Left();
            }

            if (newState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                    world.Reset();
            }

            if (newState.GetPressedKeys().Length == 0 && oldState.GetPressedKeys().Length > 0)
                world.Drones[0].Release();

            oldState = newState;
        }
    }
}
