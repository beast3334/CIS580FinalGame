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
        public Player player;
        BackgroundTileModel backgroundTileModel;
        Background background;
        public int Score;
        public int Wave;
        SpriteFont mainFont;
        int playerHearts;
        int playerNukes;
        Texture2D heart;
        Texture2D nuke;

        List<BulletSpawner> BulletSpawners = new List<BulletSpawner>();
        //List<Enemy> Enemies;
        //EnemySpawner EnemySpawner;
        Director director;


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
            Score = 0;
            Wave = 1;
            playerHearts = 3;
            playerNukes = 3;
            heart = Content.Load<Texture2D>("Heart");
            nuke = Content.Load<Texture2D>("Nuke");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            player.LoadContent(Content);
            //Background
            backgroundTileModel.LoadContent(Content);
            background = new Background(this, backgroundTileModel);


            //Enemies
            //EnemySpawner = new EnemySpawner(this);
            // EnemySpawner.LoadContent(Content);
            director = new Director(this);
            director.LoadContent(Content);
            mainFont = Content.Load<SpriteFont>("mainFont");

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
            Collision.CheckAll(director.enemySpawner.Enemies, BulletSpawners, player);
            //remove dead enemies
            //EnemySpawner.Update(gameTime);
            director.Update(gameTime);
            
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
            //EnemySpawner.Draw(spriteBatch);
            director.Draw(spriteBatch);

            // draw score
            spriteBatch.DrawString(mainFont, "SCORE", new Vector2(40, 20), Color.Red);
            spriteBatch.DrawString(mainFont, Score.ToString(), new Vector2(141, 20), Color.Red);

            // draw wave
            spriteBatch.DrawString(mainFont, "WAVE", new Vector2(1790, 20), Color.Red);
            spriteBatch.DrawString(mainFont, Wave.ToString(), new Vector2(1875, 20), Color.Red);

            // draw hearts
            spriteBatch.DrawString(mainFont, "HEARTS", new Vector2(40, 1000), Color.Red);
            for(int i = 1; i <= playerHearts; i++)
            {
                spriteBatch.Draw(heart, new BoundingRectangle(120 + (i * 40), 995, 35, 35), Color.White);
            }

            // draw nukes
            spriteBatch.DrawString(mainFont, "NUKES", new Vector2(40, 1040), Color.Red);
            for (int i = 1; i <= playerNukes; i++)
            {
                spriteBatch.Draw(nuke, new BoundingRectangle(120 + (i * 40), 1035, 35, 35), Color.White);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
