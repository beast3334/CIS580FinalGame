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

namespace MonoGameWindowsStarter
{
    class BackgroundTile
    {
        Game1 game;
        Texture2D texture;
        BoundingRectangle bounds;
        public BoundingRectangle Bounds
        {
            get { return bounds; }

        }
        /// <summary>
        /// A tile of a scrolling background
        /// </summary>
        /// <param name="game"></param>
        /// <param name="model"></param>
        /// <param name="y"></param>
        public BackgroundTile(Game1 game, BackgroundTileModel model, float y)
        {
            this.game = game;
            texture = model.Texture;
            bounds.Y = y;
            bounds.Height = game.GraphicsDevice.Viewport.Height;
            bounds.Width = game.GraphicsDevice.Viewport.Width;
        }
        /// <summary>
        /// Sets the location of the background tile
        /// </summary>
        /// <param name="y">Set the Y location</param>
        public void SetLocation(float y)
        {
            bounds.Y = y;
        }
        public void Update(GameTime gameTime, float speed)
        {
            bounds.Y += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
