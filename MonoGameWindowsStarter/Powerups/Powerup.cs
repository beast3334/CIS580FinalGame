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
        /// Direction and Speed the bullet goes
        /// </summary>
        public abstract Vector2 Velocity { get; }

        /// <summary>
        /// Scale of the Texture
        /// <para>- If the texture is bigger than you want it</para>
        /// </summary>
        public abstract Vector2 Scale { get; }

        /// <summary>
        /// Color for the SpriteBatch to use in the Draw method
        /// </summary>
        public Color Color => Color.White;

        /// <summary>
        /// Degrees of rotation for the Sprite to rotate
        /// </summary>
        public float Angle => 0f;

        /// <summary>
        /// TimeSpan set to 1 second
        /// </summary>
        public TimeSpan TimeBetweenBullets => new TimeSpan(0, 0, 0, 0, 200);

        //Extend with any methods required by all powerups.

        //Will allow for abstract access to all powerups, instead of by name.
    }
}
