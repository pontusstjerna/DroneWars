using DroneWars.Controller;
using DroneWars.Model;
using DroneWars.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DroneWars
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private World world;
        private WorldRenderer renderer;
        private PlayerController controller;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = new World();
            controller = new PlayerController(world);
            renderer = new WorldRenderer(world, GraphicsDevice,
                Content.Load<Texture2D>("track1_background"),
                LoadBlocks(),
                LoadDrones()
                );
               
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            controller.Update();
            world.Update(dTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            renderer.Render(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Draw(gameTime);
        }

        private Dictionary<BlockType, Texture2D> LoadBlocks()
        {
            Dictionary<BlockType, Texture2D> blocks = new Dictionary<BlockType, Texture2D>();

            foreach(BlockType type in Enum.GetValues(typeof(BlockType)))
            {
                blocks.Add(type, Content.Load<Texture2D>("blocks/" + type.ToString()));
            }

            return blocks;
        }

        private List<Texture2D> LoadDrones()
        {
            List<Texture2D> drones = new List<Texture2D>();
            for(int i = 1; i <= world.Drones.Count; i++)
            {
                drones.Add(Content.Load<Texture2D>("drones/drone" + i));
            }

            return drones;
        }
    }
}
