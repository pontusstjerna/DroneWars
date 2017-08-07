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
            CheckKeyboardPlayer1();
            CheckKeyboardPlayer2();
        }

        private void CheckKeyboardPlayer1()
        {
            KeyboardState newState = Keyboard.GetState();
            bool pressed = false;

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

            if (newState.IsKeyDown(Keys.Enter))
            {
                if (!oldState.IsKeyDown(Keys.Enter))
                    world.Reset();
            }

            if(!pressed) //TODO: Complete
                world.Drones[0].Release();

            oldState = newState;
        }

        private void CheckKeyboardPlayer2()
        {
            KeyboardState newState = Keyboard.GetState();

            if (newState.IsKeyDown(Keys.W))
            {
                world.Drones[1].Up();
            }
            else if (newState.IsKeyDown(Keys.S))
            {
                world.Drones[1].Down();
            }
            else if (newState.IsKeyDown(Keys.D))
            {
                world.Drones[1].Right();
            }
            else if (newState.IsKeyDown(Keys.A))
            {
                world.Drones[1].Left();
            }
            else
            {
                world.Drones[1].Release();
            }

            oldState = newState;
        }
    }
}
