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

namespace MonoGameWindowsStarter.PlayerNamespace
{
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

        public PlayerState State { get; set; } = PlayerState.Idle;
        public override BoundingRectangle Bounds { get => bounds; }

        public Vector2 Velocity { get; set; } = Vector2.Zero;

        public Vector2 Scale { get; set; } = Vector2.One;

        public float Health { get; set; }

        public BulletSpawner BulletSpawner { get; set; }
        public PlayerPowerup Powerup { get; set; } = new PlayerPowerup_Default();

        public Player(Game1 game)
        {
            this.game = game;
            BulletSpawner = new BulletSpawner(game, this);

            if (game.GraphicsDevice != null)
            {
                LoadContent(game.Content);
            }
        }

        private Texture2D GetTexture2D(PlayerState ps)
        {
            for (int i = 0; i < Textures.Count; i++)
            {
                if (Textures[i].Item1 == ps)
                {
                    return Textures[i].Item2;
                }
            }
            return Textures[0].Item2;
        }

        private void UpdateBounds()
        {
            // Gets the texture
            var texture = GetTexture2D(State);

            // Sets the bounds according to the texture
            bounds = new BoundingRectangle(
                bounds.X,
                bounds.Y,
                texture.Width * Scale.X,
                texture.Height * Scale.Y
            );
        }

        public void ChangePowerup(PlayerPowerup powerup)
        {
            Powerup = powerup;
            LoadContent(game.Content);
        }

        public override void LoadContent(ContentManager content)
        {
            Powerup.TextureNames.ForEach(tex =>
            {
                Textures.Add(new Tuple<PlayerState, Texture2D>(tex.Item1, content.Load<Texture2D>(tex.Item2)));
            });

            // Get the texture
            var texture = GetTexture2D(State);
            // Set the bounds according to the texture
            bounds = new BoundingRectangle(
                (game.GraphicsDevice.Viewport.Width - Bounds.Width) / 2, //Places player horizontally in the middle of viewwindow
                game.GraphicsDevice.Viewport.Height, //Places player at bottom of viewwindow
                texture.Width * Scale.X,
                texture.Height * Scale.Y
            );

            Velocity = Powerup.Velocity;
            Scale = Powerup.Scale;
            Health = Powerup.Health;

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
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                BulletSpawner.Shoot();
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw the Bullet Spawner under the player
            BulletSpawner.Draw(spriteBatch);

            // Draw the Player
            spriteBatch.Draw(
                GetTexture2D(State), 
                bounds, 
                Powerup.Color
            );
        }
    }
}
