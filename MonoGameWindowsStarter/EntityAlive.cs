using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter
{
    public abstract class EntityAlive
    {
        public abstract BoundingRectangle Bounds { get; }

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent(ContentManager Content);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
