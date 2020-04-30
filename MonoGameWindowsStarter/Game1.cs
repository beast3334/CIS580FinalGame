using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using System.Collections.Generic;


namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        BackgroundTileModel backgroundTileModel;
        Background background;

        List<BulletSpawner> BulletSpawners = new List<BulletSpawner>();
        //List<Enemy> Enemies;
        EnemySpawner EnemySpawner;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            player = new Player(this);
            backgroundTileModel = new BackgroundTileModel();

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            player.LoadContent(Content);
            //Background
            backgroundTileModel.LoadContent(Content);
            background = new Background(this, backgroundTileModel);


            //Enemies
            EnemySpawner = new EnemySpawner(this);
            EnemySpawner.LoadContent(Content);

            VisualDebugging.LoadContent(Content);

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

            player.Update(gameTime);

            background.Update(gameTime);
            base.Update(gameTime);
            //Check all collisions
            Collision.CheckAll(EnemySpawner.Enemies, BulletSpawners, player);
            //remove dead enemies
            EnemySpawner.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            background.Draw(spriteBatch);

            if (player.Alive)
            {
                player.Draw(spriteBatch);
            }
            EnemySpawner.Draw(spriteBatch);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
