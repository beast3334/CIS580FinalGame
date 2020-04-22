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

namespace MonoGameWindowsStarter.Enemies
{
    public abstract class Enemy
    {
        public abstract BoundingRectangle Bounds
        { get; }

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public abstract void Draw(SpriteBatch spriteBatch);

        //Extend with any methods required by all enemies.

        //Will allow for abstract access to all enemies, instead of by name.
    }
}
