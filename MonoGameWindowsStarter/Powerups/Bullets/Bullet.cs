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
        private Vector2 position;
        private Game game;

        /// <summary>
        /// Position of the bullet
        /// </summary>
        public Vector2 Position => position;

        /// <summary>
        /// Powerup on the bullet
        /// </summary>
        public Powerup Powerup { get; }

        /// <summary>
        /// Is this bullet alive?
        /// </summary>
        public bool Alive => alive;
        private bool alive = true;

        /// <summary>
        /// The Bullet that moves
        /// </summary>
        /// <param name="bulletPosition">The BulletPosition for the bullet</param>
        /// <param name="powerup">The powerup to follow for the rest of it's life</param>
        public Bullet(Game game, BulletPosition bulletPosition, Powerup powerup)
        {
            this.game = game;
            position = bulletPosition.Position;
            Powerup = powerup;
        }

        /// <summary>
        /// Updates the Bullet
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            position += Powerup.Velocity;

            if (   position.Y > game.GraphicsDevice.Viewport.Height + 100
                || position.Y < 0 - 100
                || position.X > game.GraphicsDevice.Viewport.Width + 100
                || position.X < 0 - 100)
            {
                alive = false;
            }
        }
    }
}
