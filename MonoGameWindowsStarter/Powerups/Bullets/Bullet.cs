using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets
{
    public class Bullet
    {
        private Game game;

        public Vector2 Position { get => new Vector2(Bounds.X, Bounds.Y); }

        /// <summary>
        /// X, Y, Width, and Height
        /// </summary>
        public BoundingRectangle Bounds { get; private set; }

        /// <summary>
        /// Powerup on the bullet
        /// </summary>
        public Powerup Powerup { get; }

        /// <summary>
        /// Is this bullet alive?
        /// </summary>
        public bool Alive { get; set; } = true;

        /// <summary>
        /// Direction and Speed the bullet goes
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Acceleration of the bullet
        /// </summary>
        public Vector2 Acceleration { get; set; }

        /// <summary>
        /// Amount of Damage this bullet gives
        /// </summary>
        public float Damage { get; }

        /// <summary>
        /// Color for the SpriteBatch to use in the Draw method
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Scale of the Texture
        /// </summary>
        public Vector2 Scale { get; }

        /// <summary>
        /// True if the bullet hit the entity or False if it went off the screen or hasn't hit anything yet
        /// </summary>
        public bool HitEntity { get; set; } = false;

        /// <summary>
        /// The Bullet that moves
        /// </summary>
        /// <param name="bulletPosition">The BulletPosition for the bullet</param>
        /// <param name="powerup">The powerup to follow for the rest of it's life</param>
        public Bullet(Game game, BulletPosition bulletPosition, Powerup powerup, Texture2D texture)
        {
            this.game = game;
            Powerup = powerup;
            Acceleration = Powerup.Acceleration;
            Velocity = Powerup.Velocity;
            Damage = Powerup.Damage;
            Color = Powerup.Color;
            Scale = Powerup.Scale;
            Bounds = new BoundingRectangle(bulletPosition.Position.X, bulletPosition.Position.Y, texture.Bounds.Width, texture.Bounds.Height);
        }

        /// <summary>
        /// Updates the Bullet
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Velocity += Acceleration;
            var position = Position;
            position += Velocity;
            Bounds = new BoundingRectangle(position.X, position.Y, Bounds.Width, Bounds.Height);

            if (position.Y > game.GraphicsDevice.Viewport.Height + Bounds.Height
                || position.Y < 0 - Bounds.Height
                || position.X > game.GraphicsDevice.Viewport.Width + Bounds.Width
                || position.X < 0 - Bounds.Width)
            {
                Alive = false;
            }
        }
    }
}
