using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.PlayerNamespace
{
    public abstract class PlayerPowerup
    {
        /// <summary>
        /// The name of the texture with the PlayerState it goes with
        /// </summary>
        public abstract List<Tuple<PlayerState, string>> TextureNames { get; }
        
        /// <summary>
        /// Scale of the Texture
        /// <para>Default: X=1, Y=1</para>
        /// </summary>
        public abstract Vector2 Scale { get; }

        /// <summary>
        /// Direction and Speed
        /// <para>Default: X=0, Y=-10</para>
        /// </summary>
        public virtual Vector2 Velocity { get; set; } = new Vector2(15);

        /// <summary>
        /// Color for the SpriteBatch to use in the Draw method
        /// <para>Default: White</para>
        /// </summary>
        public virtual Color Color => Color.White;

        /// <summary>
        /// Health to add or replace
        /// <para>Bool: Replace the current health</para>
        /// <para>Float: Amount of Health</para>
        /// <para>Default: (false, 0) ; Add 0</para>
        /// </summary>
        public virtual Tuple<bool, int> Hearts => new Tuple<bool, int>(false, 0);
    }
}
