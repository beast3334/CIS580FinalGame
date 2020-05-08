using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets
{
    public class BulletSpawner
    {
        Game game;
        ContentManager content;
        BulletPosition position;
        TimeSpan timeBetweenBullets = new TimeSpan();
        TimeSpan timer = new TimeSpan();
        bool canShoot = true;
        bool canChangePowerup = true;

        /// <summary>
        /// Bullet spawners
        /// </summary>
        public List<BulletSpawner> BulletSpawners { get; private set; } = new List<BulletSpawner>();

        /// <summary>
        /// The position of this spawner
        /// </summary>
        public Vector2 Position => position.Position;

        /// <summary>
        /// The powerup this spawner is currently using
        /// <para>Default: PowerupDefault</para>
        /// </summary>
        public Powerup Powerup { get; set; } = new PowerupLaser();

        /// <summary>
        /// Texture for the bullet's to use
        /// </summary>
        public Texture2D Texture { get; private set; }

        /// <summary>
        /// A list of all the current bullets
        /// </summary>
        public List<Bullet> Bullets { get; set; } = new List<Bullet>();

        /// <summary>
        /// Spawner for bullets
        /// </summary>
        /// <param name="game"></param>
        /// <param name="position"></param>
        public BulletSpawner(Game game, Vector2 position)
        {
            Initialize(game);
            this.position = new BulletPosition(position);
        }

        /// <summary>
        /// Spawner for bullets
        /// - Create one instance where you want bullets to be spawned
        /// </summary>
        /// <param name="game"></param>
        /// <param name="entity">The EntityAlive that you want this spawner to track</param>
        public BulletSpawner(Game game, EntityAlive entity)
        {
            Initialize(game);
            position = new BulletPosition(entity, this);
        }

        /// <summary>
        /// Spawner for bullets
        /// - Create one instance where you want bullets to be spawned
        /// </summary>
        /// <param name="game"></param>
        /// <param name="bounds">The BoundingRectangle that you want this spawner to track</param>
        public BulletSpawner(Game game, BoundingRectangle bounds)
        {
            Initialize(game);
            position = new BulletPosition(bounds, this);
        }

        private void Initialize(Game game)
        {
            this.game = game;
            timeBetweenBullets = Powerup.TimeBetweenBullets;
            if (game.GraphicsDevice != null)
            {
                LoadContent(game.Content);
            }
        }

        /// <summary>
        /// If the spawner can shoot (timer between shots is at 00:00), a bullet is shot from the spawner
        /// </summary>
        /// <returns>Whether a shot was taken or not</returns>
        public bool Shoot()
        {
            if (canShoot)
            {
                // Makes the start and finish for spawning the bullets
                // Ex: NumberToSpawnOnShoot = 5 then start = -2 and finish = 2
                int n = Powerup.NumberToSpawnOnShoot;
                if (n % 2 == 1)
                {
                    n--;
                }
                int start = -(n / 2);
                int finish = (n / 2);

                for (int i = start; i <= finish; i++)
                {
                    // New bullet
                    var bullet = new Bullet(game, position, Powerup, Texture);
                    // New velocity based on the rotation
                    bullet.Velocity = Vector2.Transform(
                        bullet.Velocity,
                        Matrix.CreateRotationZ(Powerup.RotationBetweenBullets * i)
                    );
                    // Adds the bullet to the list of bullets
                    Bullets.Add(bullet);
                }
                // Can't shoot now
                canShoot = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Changes the current Powerup being used
        /// <para>**IMPORTANT** Only call this method AFTER LoadContent</para>
        /// </summary>
        /// <param name="powerup">Powerup to use</param>
        /// <returns></returns>
        public bool ChangePowerup(Powerup powerup)
        {
            // Can only change a powerup if it's allowed
            if (canChangePowerup)
            {
                Powerup = powerup;
                timeBetweenBullets = powerup.TimeBetweenBullets;
                Texture = content.Load<Texture2D>(powerup.TextureName);
                return true;
            }
            return false;
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;
            // TODO: Make a dictionary or something of Textures to use in the Draw for different bullets just in case the texture changes while the bullet is still alive.
            Texture = content.Load<Texture2D>(Powerup.TextureName);

            // Go through the spawners and Load their content
            BulletSpawners.ForEach(bs =>
            {
                bs.LoadContent(content);
            });
        }

        public void Update(GameTime gameTime)
        {

            // Checks if the timer's time is > than the time allowed between shots
            if (canShoot || timer.TotalMilliseconds > timeBetweenBullets.TotalMilliseconds)
            {
                // Resets the timer back to 0
                timer = new TimeSpan();

                // Allows the spawner to shoot
                canShoot = true;
            }
            else
            {
                // Adds the time from the last method call to the timer
                timer += gameTime.ElapsedGameTime;

                // Does not allow the spawner to shoot
                canShoot = false;
            }

            for (int i = 0; i < Bullets.Count; i++)
            {
                var bullet = Bullets[i];
                bullet.Update(gameTime);

                if (!bullet.Alive)
                {
                    if (bullet.Powerup.SpawnAfterImpact != null && bullet.HitEntity)
                    {
                        var tempBS = new BulletSpawner(game, bullet.Position) { Powerup = bullet.Powerup.SpawnAfterImpact };
                        tempBS.Shoot();
                        BulletSpawners.Add(tempBS);
                    }

                    Bullets.RemoveAt(i);
                    i--;
                }
            }

            // Go through all the spawners and update them
            BulletSpawners.ForEach(bs =>
            {
                bs.Update(gameTime);
            });
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Go through every bullet to Draw
            Bullets.ForEach(bullet =>
            {
                // Auto rotation for whichever way the bullet is traveling
                var rotation = (float)(Math.Atan2(bullet.Velocity.Y, bullet.Velocity.X) + (Math.PI / 2));
                
                // Size of the bullet for rendering
                var size = new BoundingRectangle(bullet.Position.X, bullet.Position.Y, Texture.Bounds.Width, Texture.Bounds.Height) * bullet.Scale;
                
                // Draw the bullet
                spriteBatch.Draw(
                    Texture,
                    size,
                    null,
                    bullet.Color,
                    rotation,
                    new Vector2(size.Width / 2, 0),
                    SpriteEffects.None,
                    1f
                );
            });

            // Go through all of the spawners and draw their bullets
            BulletSpawners.ForEach(bs =>
            {
                bs.Draw(spriteBatch);
            });
        }
    }
}
