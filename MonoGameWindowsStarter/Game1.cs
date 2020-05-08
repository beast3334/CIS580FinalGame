using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using System.Collections.Generic;
using MonoGameWindowsStarter.PlayerNamespace;

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
        public Random random = new Random();
        public int Wave;
        SpriteFont mainFont;
        Texture2D heart;
        Texture2D nuke;

        List<BulletSpawner> BulletSpawners = new List<BulletSpawner>();
        //List<Enemy> Enemies;
        //EnemySpawner EnemySpawner;
        Director director;
        public UpgradeMenu upgradeMenu;

        //particles
        public ParticleSystem particleSystem;
        public ParticleSystem playerParticle;
        Texture2D particleTexture;


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


            //particles
            particleTexture = Content.Load<Texture2D>("Particle");
            particleSystem = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            playerParticle = new ParticleSystem(this.GraphicsDevice, 1000, particleTexture);
            //particleSystem.Emitter = new Vector2(100, 100);
            particleSystem.SpawnPerFrame = 20;
            particleSystem.SpawnParticle = (ref Particle particle) =>
            {
                MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(mouse.X, mouse.Y);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.Gold;
                particle.Scale = 1f;
                particle.Life = .3f;
            };

            // Set the UpdateParticle method
            particleSystem.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };


            playerParticle.SpawnPerFrame = 1;
            playerParticle.SpawnParticle = (ref Particle particle) =>
            {
                MouseState mouse = Mouse.GetState();
                particle.Position = new Vector2(player.Bounds.X + player.Bounds.Width/2, player.Bounds.Y + player.Bounds.Height/2);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                    MathHelper.Lerp(0, 100, (float)random.NextDouble()) // Y between 0 and 100
                    );
                particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.LightYellow;
                particle.Scale = .5f;
                particle.Life = .5f;
            };

            // Set the UpdateParticle method
            playerParticle.UpdateParticle = (float deltaT, ref Particle particle) =>
            {
                particle.Velocity += deltaT * particle.Acceleration;
                particle.Position += deltaT * particle.Velocity;
                particle.Scale -= deltaT;
                particle.Life -= deltaT;
            };


            upgradeMenu = new UpgradeMenu(this, Content, player, Score);
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

            Collision.CheckAll(new List<EntityAlive>(director.enemySpawner.Enemies), player, director.bossSpawner.boss);
            //remove dead enemies
            //EnemySpawner.Update(gameTime);
            particleSystem.Update(gameTime);
            if (player.Alive)
            {
                playerParticle.SpawnParticle = (ref Particle particle) =>
                {
                    MouseState mouse = Mouse.GetState();
                    particle.Position = new Vector2(player.Bounds.X + player.Bounds.Width / 3, player.Bounds.Y + player.Bounds.Height);
                    particle.Velocity = new Vector2(
                        MathHelper.Lerp(-50, 50, (float)random.NextDouble()), // X between -50 and 50
                        MathHelper.Lerp(-50, 50, (float)random.NextDouble()) // Y between 0 and 100
                        );
                    particle.Acceleration = 0.1f * new Vector2(0, (float)-random.NextDouble());
                    particle.Color = Color.LightYellow;
                    particle.Scale = .5f;
                    particle.Life = .5f;
                };
            }
            playerParticle.Update(gameTime);
            director.Update(gameTime);

            //testing to draw the upgradeMenu
            //temp: open upgrade by hitting 'U'
            var keyboardState = Keyboard.GetState();
            
            if (keyboardState.IsKeyDown(Keys.U))
            {
                upgradeMenu.isOpen = true;
            }


            if (upgradeMenu.isOpen)
                upgradeMenu.Update(gameTime);
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
            spriteBatch.End();
            playerParticle.Draw();
            spriteBatch.Begin();

            

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
            for(int i = 1; i <= player.Hearts; i++)
            {
                spriteBatch.Draw(heart, new BoundingRectangle(120 + (i * 40), 995, 35, 35), Color.White);
            }

            // draw nukes
            spriteBatch.DrawString(mainFont, "NUKES", new Vector2(40, 1040), Color.Red);
            for (int i = 1; i <= player.Nukes; i++)
            {
                spriteBatch.Draw(nuke, new BoundingRectangle(120 + (i * 40), 1035, 35, 35), Color.White);
            }
            if (upgradeMenu.isOpen)
                upgradeMenu.Draw(spriteBatch);

            spriteBatch.End();
            particleSystem.Draw();
            
            base.Draw(gameTime);
        }
    }
}
