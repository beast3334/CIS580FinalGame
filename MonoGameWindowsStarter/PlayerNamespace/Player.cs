using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.PlayerNamespace.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;
using MonoGameWindowsStarter.Powerups;

namespace MonoGameWindowsStarter.PlayerNamespace
{
    /// <summary>
    /// Different states of the Player
    /// </summary>
    public enum PlayerState
    {
        Idle,
        Up,
        Down,
        Left,
        Right
    }

    public class Player : EntityAlive
    {
        Game1 game;
        List<Tuple<PlayerState, Texture2D>> Textures = new List<Tuple<PlayerState, Texture2D>>();
        BoundingRectangle bounds = new BoundingRectangle();

        /// <summary>
        /// State of the player
        /// </summary>
        public PlayerState State { get; private set; } = PlayerState.Idle;

        /// <summary>
        /// Bounds of the player
        /// </summary>
        public override BoundingRectangle Bounds { get => bounds; }

        /// <summary>
        /// Velocity of the player
        /// </summary>
        public Vector2 Velocity { get; set; } = Vector2.Zero;

        /// <summary>
        /// Scale of the player
        /// </summary>
        public Vector2 Scale { get; set; } = Vector2.One;

        /// <summary>
        /// Current number of Hearts for the player
        /// </summary>
        public int Hearts { get; set; } = 3;

        /// <summary>
        /// Max number of hearts that the player can have
        /// </summary>
        public int MaxHearts { get; set; } = 3;

        /// <summary>
        /// Current number of nukes the player can use
        /// </summary>
        public int Nukes { get; set; } = 3;

        /// <summary>
        /// The bullet spawner
        /// </summary>
        public BulletSpawner BulletSpawner { get; set; }

        /// <summary>
        /// The currently used player powerup
        /// </summary>
        public PlayerPowerup PlayerPowerup { get; private set; } = new PlayerPowerup_Default();

        /// <summary>
        /// Temporary Bullet Powerup the player picked up
        /// </summary>
        public Powerup TempPowerup { get; private set; } = null;

        /// <summary>
        /// The permanent Bullet powerup
        /// </summary>
        public Powerup CurrentPowerup { get; private set; } = null;

        private TimeSpan? _tempPowerupTimer = null;
        private bool _usedTempPowerup = false;

        public Player(Game1 game)
        {
            this.game = game;
            BulletSpawner = new BulletSpawner(game, this);

            if (game.GraphicsDevice != null)
            {
                LoadContent(game.Content);
            }
        }

        /// <summary>
        /// Adds hearts to the Player's current heart count if it isn't already full
        /// </summary>
        /// <param name="numberOfHearts">The number of hearts to add to the player</param>
        /// <returns>If a heart was added</returns>
        public bool AddHearts_PickedUpPowerup(int numberOfHearts)
        {
            if (Hearts < MaxHearts)
            {
                Hearts += numberOfHearts;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds hearts to the Player's max heart count
        /// <para>Fills the players current hearts</para>
        /// </summary>
        /// <param name="numberOfHearts"></param>
        public void AddHearts_PurchasedPowerup(int numberOfHearts)
        {
            MaxHearts += numberOfHearts;
            Hearts = MaxHearts;
        }

        /// <summary>
        /// Gets the Texture of the player
        /// </summary>
        /// <returns>The Player's current Texture2D</returns>
        private Texture2D GetPlayerTexture()
        {
            for (int i = 0; i < Textures.Count; i++)
            {
                if (Textures[i].Item1 == State)
                {
                    return Textures[i].Item2;
                }
            }
            return Textures[0].Item2;
        }

        /// <summary>
        /// Updates the player's bounds
        /// </summary>
        private void UpdateBounds()
        {
            // Gets the texture
            var texture = GetPlayerTexture();

            // Sets the bounds according to the texture
            bounds = new BoundingRectangle(
                bounds.X,
                bounds.Y,
                texture.Width * Scale.X,
                texture.Height * Scale.Y
            );
        }

        /// <summary>
        /// Changes the powerup of the player
        /// </summary>
        /// <param name="powerup">Powerup to add</param>
        public void ChangePowerup_Permanent(Powerup powerup)
        {
            CurrentPowerup = powerup;
            TempPowerup = null;
            _tempPowerupTimer = null;
            _usedTempPowerup = false;
            LoadContent(game.Content);
        }

        /// <summary>
        /// Gives the player a temp powerup that lasts until they use it or the timer runs out
        /// </summary>
        /// <param name="powerup">Powerup to use</param>
        public void ChangePowerup_PickedUp(Powerup powerup)
        {
            TempPowerup = CurrentPowerup;
            CurrentPowerup = powerup;
            _tempPowerupTimer = powerup.Timer;
            _usedTempPowerup = false;
            LoadContent(game.Content);
        }

        private void ChangeTempPowerupBack()
        {
            CurrentPowerup = TempPowerup;
            TempPowerup = null;
            _tempPowerupTimer = null;
            _usedTempPowerup = false;
            LoadContent(game.Content);
        }

        public void AddSpeed(Vector2 Speed)
        {
            Velocity += Speed;
        }

        /// <summary>
        /// Loads the content and updates the powerup
        /// </summary>
        /// <param name="content">Content to load from</param>
        public override void LoadContent(ContentManager content)
        {
            PlayerPowerup.TextureNames.ForEach(tex =>
            {
                Textures.Add(new Tuple<PlayerState, Texture2D>(tex.Item1, content.Load<Texture2D>(tex.Item2)));
            });

            BulletSpawner.ChangePowerup(CurrentPowerup);

            // Get the texture
            var texture = GetPlayerTexture();
            // Set the bounds according to the texture
            bounds = new BoundingRectangle(
                (game.GraphicsDevice.Viewport.Width - Bounds.Width) / 2, //Places player horizontally in the middle of viewwindow
                game.GraphicsDevice.Viewport.Height, //Places player at bottom of viewwindow
                texture.Width * Scale.X,
                texture.Height * Scale.Y
            );

            Velocity = PlayerPowerup.Velocity;
            Scale = PlayerPowerup.Scale;

            // Add to current hearts
            if (PlayerPowerup.Hearts.Item1 == false)
            {
                Hearts += PlayerPowerup.Hearts.Item2;
            }
            // Replace the current hearts
            else
            {
                Hearts = PlayerPowerup.Hearts.Item2;
            }

            // Load the Bullet Spawner Content
            BulletSpawner.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            //Up Movement
            if(keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                bounds.Y -= Velocity.Y;
                State = PlayerState.Up; //For future, if adding animation to player
            }

            //Down Movement
            if(keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                bounds.Y += Velocity.Y;
                State = PlayerState.Down; //For future, if adding animation to player
            }

            //Left Movement
            if(keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                bounds.X -= Velocity.X;
                State = PlayerState.Left; //For future, if adding animation to player
            }

            //Right Movement
            if(keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                bounds.X += Velocity.X;
                State = PlayerState.Right; //For future, if adding animation to player
            }

            //Idle Movement
            if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
            {
                State = PlayerState.Up; //For future, if adding animation to player
            }

            // Updates the bounds based on the PlayerState
            UpdateBounds();

            //Check Action Button
            if (keyboardState.IsKeyDown(Keys.Space) && Alive)
            {
                BulletSpawner.Shoot();

                // The temporary powerup is used
                if (TempPowerup != null)
                {
                    // Knows the powerup is used
                    _usedTempPowerup = true;
                }
            }

            //Check Y bounds
            if (bounds.Y <= 0)
            {
                bounds.Y = 0;
            }
            if(bounds.Y >= game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            //Check X bounds
            if(bounds.X <= 0)
            {
                bounds.X = 0;
            }
            if(bounds.X >= game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }

            // Update the Bullet Spawner
            BulletSpawner.Update(gameTime);

            if (_tempPowerupTimer != null)
            {
                _tempPowerupTimer -= gameTime.ElapsedGameTime;

                if (_tempPowerupTimer.Value.Ticks < 0 && _usedTempPowerup == true)
                {
                    ChangeTempPowerupBack();
                }
            }

            if (Hearts <= 0)
            {
                Alive = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Bullet Spawner under the player
            BulletSpawner.Draw(spriteBatch);
            var texture = GetPlayerTexture();

            // Draw the Player
            spriteBatch.Draw(
                texture,
                bounds,
                null,
                PlayerPowerup.Color,
                0f,
                new Vector2(texture.Width / 2, 0),
                SpriteEffects.None,
                1f
            );
        }
    }
}
