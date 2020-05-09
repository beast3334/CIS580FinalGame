using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MonoGameWindowsStarter
{

    public struct Particle
    {
        /// <summary>
        /// The current position of the particle
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// The current velocity of the particle
        /// </summary>
        public Vector2 Velocity;

        /// <summary>
        /// The current acceleration of the particle
        /// </summary>
        public Vector2 Acceleration;

        /// <summary>
        /// The current scale of the particle
        /// </summary>
        public float Scale;

        /// <summary>
        /// The current life of the particle
        /// </summary>
        public float Life;

        /// <summary>
        /// The current color of the particle
        /// </summary>
        public Color Color;
    }
}
