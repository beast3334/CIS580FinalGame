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

namespace MonoGameWindowsStarter.Powerups
{
    public abstract class Powerup
    {
        /// <summary>
        /// Name of the Texture in the Content
        /// </summary>
        public abstract string TextureName { get; }

        /// <summary>
        /// Direction and Speed
        /// <para>Default: X=0, Y=-10</para>
        /// </summary>
        public virtual Vector2 Velocity { get; set; } = new Vector2(0, -10);

        /// <summary>
        /// Acceleration of the bullet
        /// <para>Default: X=0, Y=0</para>
        /// </summary>
        public virtual Vector2 Acceleration { get; set; } = Vector2.Zero;

        /// <summary>
        /// Scale of the Texture
        /// <para>Default: X=1, Y=1</para>
        /// </summary>
        public virtual Vector2 Scale => Vector2.One;

        /// <summary>
        /// Color for the SpriteBatch to use in the Draw method
        /// <para>Default: White</para>
        /// </summary>
        public virtual Color Color => Color.White;

        /// <summary>
        /// TimeSpan set to 1 second
        /// <para>Default: 200 milliseconds</para>
        /// </summary>
        public virtual TimeSpan TimeBetweenBullets => new TimeSpan(0, 0, 0, 0, 200);

        /// <summary>
        /// The amount of rotation between bullets
        /// <para>Default: PI / 15 radians</para>
        /// </summary>
        public virtual float RotationBetweenBullets => (float)(Math.PI / 15);

        /// <summary>
        /// Amount of Damage the bullet does on collision
        /// <para>Default: 1</para>
        /// </summary>
        public virtual int Damage => 1;

        /// <summary>
        /// Number of Bullets to dispersely shoot
        /// <para>Default: 1</para>
        /// </summary>
        public virtual int NumberToSpawnOnShoot => 1;

        /// <summary>
        /// The powerup to spawn after the bullet has impacted another entity
        /// </summary>
        public virtual Powerup SpawnAfterImpact => null;

        /// <summary>
        /// The time that the powerup lasts if it isn't permanent
        /// </summary>
        public virtual TimeSpan Timer => new TimeSpan(0, 0, 30);
    }
}
