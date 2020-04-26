using Microsoft.Xna.Framework;
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

        /// <summary>
        /// Position of the bullet
        /// </summary>
        public Vector2 Position { get; private set; }

        /// <summary>
        /// Powerup on the bullet
        /// </summary>
        public Powerup Powerup { get; }

        /// <summary>
        /// Is this bullet alive?
        /// </summary>
        public bool Alive { get; private set; } = true;

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
        /// The Bullet that moves
        /// </summary>
        /// <param name="bulletPosition">The BulletPosition for the bullet</param>
        /// <param name="powerup">The powerup to follow for the rest of it's life</param>
        public Bullet(Game game, BulletPosition bulletPosition, Powerup powerup)
        {
            this.game = game;
            Position = bulletPosition.Position;
            Powerup = powerup;
            Acceleration = Powerup.Acceleration;
            Velocity = Powerup.Velocity;
            Damage = Powerup.Damage;
            Color = Powerup.Color;
            Scale = Powerup.Scale;
        }

        /// <summary>
        /// Updates the Bullet
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            Velocity += Acceleration;
            Position += Velocity;

            if (Position.Y > game.GraphicsDevice.Viewport.Height + 100
                || Position.Y < 0 - 100
                || Position.X > game.GraphicsDevice.Viewport.Width + 100
                || Position.X < 0 - 100)
            {
                Alive = false;
            }
        }
    }
}
