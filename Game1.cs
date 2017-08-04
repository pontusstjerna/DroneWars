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
        private UIRenderer uiRenderer;
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
            //GraphicsDevice.Viewport = new Viewport(0, 0, 1000, 1000); //TODO
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
                new Rectangle(0, 100, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height - 100),
                Content);
            uiRenderer = new UIRenderer(world, GraphicsDevice, Content);
               
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
            uiRenderer.Render(spriteBatch, (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Draw(gameTime);
        }
    }
}
