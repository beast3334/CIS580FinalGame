using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
        public Background background;
        public int Score;
        public Random random = new Random();
        public int Wave;
        SpriteFont mainFont;
        Texture2D heart;
        Texture2D nuke;

        public bool started;
        public bool paused;
        Texture2D pauseMenuTexture;
        Texture2D mainMenuTexture;
        SoundEffect enemyKilledSound;
        SoundEffect playerHitSound;
        SoundEffect useNuke;
        public SoundEffect upgradePickup;
        Song gameSong;

        KeyboardState oldKeyboard;

        //List<BulletSpawner> BulletSpawners = new List<BulletSpawner>();
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
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Microsoft.Xna.Framework.Media.MediaPlayer.IsRepeating = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            started = false;
            pauseMenuTexture = Content.Load<Texture2D>("PauseMenu");
            mainMenuTexture = Content.Load<Texture2D>("MainMenu");
            paused = false;
            player = new Player(this);
            backgroundTileModel = new BackgroundTileModel();
            Score = 0;
            Wave = 1;
            heart = Content.Load<Texture2D>("Heart");
            nuke = Content.Load<Texture2D>("Nuke");
            useNuke = Content.Load<SoundEffect>("useNuke");
            upgradePickup = Content.Load<SoundEffect>("waveupgrade");
            enemyKilledSound = Content.Load<SoundEffect>("enemyKilled");
            playerHitSound = Content.Load<SoundEffect>("playerHit");
            gameSong = Content.Load<Song>("space_theme");
            MediaPlayer.Play(gameSong);
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
            var keyboardState = Keyboard.GetState();
            if (started)
            {
                //pause menu
                if (paused)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !oldKeyboard.IsKeyDown(Keys.Escape) || Keyboard.GetState().IsKeyDown(Keys.D2) && !oldKeyboard.IsKeyDown(Keys.D2))
                        Exit();
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !oldKeyboard.IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.D1) && !oldKeyboard.IsKeyDown(Keys.D1))
                        paused = false;
                }

                if (player.Alive && !paused)
                {
                    //Pause Game
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        paused = true;

                    //Use Nuke
                    if(Keyboard.GetState().IsKeyDown(Keys.LeftShift) && !oldKeyboard.IsKeyDown(Keys.LeftShift) && player.Nukes >0)
                    {
                        director.enemySpawner.KillAll();
                        player.Nukes--;
                        useNuke.Play();
                    }

                    player.Update(gameTime);

                    background.Update(gameTime);
                    base.Update(gameTime);
                    //Check all collisions
                    Collision.CheckAll(new List<EntityAlive>(director.enemySpawner.Enemies), player, director.bossSpawner.boss, director.powerupSpawner, enemyKilledSound, playerHitSound);
                    
                    particleSystem.Update(gameTime);
                    if (player.Alive)
                    {
                        playerParticle.SpawnParticle = (ref Particle particle) =>
                        {
                            MouseState mouse = Mouse.GetState();
                            particle.Position = new Vector2((player.Bounds.X + player.Bounds.Width/2)-4, player.Bounds.Y + player.Bounds.Height);
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

                    if (keyboardState.IsKeyDown(Keys.U))
                    {
                        upgradeMenu.isOpen = true;
                    }


                    if (upgradeMenu.isOpen)
                        upgradeMenu.Update(gameTime);
                }
                else
                {
                    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
                        LoadContent();
                }
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D2) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D1) || Keyboard.GetState().IsKeyDown(Keys.Enter))
                    started = true;
            }
            oldKeyboard = keyboardState;
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
            else
            {
                spriteBatch.DrawString(mainFont, "GAME OVER: Press 'Enter' to restart", new Vector2(700, 540), Color.Red);
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

            if (paused)
            {
                spriteBatch.Draw(pauseMenuTexture, new BoundingRectangle(0, 0, 1920, 1080), Color.White);
            }
            if (!started)
            {
                spriteBatch.Draw(mainMenuTexture, new BoundingRectangle(0, 0, 1920, 1080), Color.White);
            }
            spriteBatch.End();
            particleSystem.Draw();
            
            
            base.Draw(gameTime);
        }
    }
}
