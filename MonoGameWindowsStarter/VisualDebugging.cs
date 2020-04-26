using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

#if DEBUG
namespace MonoGameWindowsStarter
{
    /// <summary>
    /// A static class containing a library of visual debugging methods
    /// </summary>
    static class VisualDebugging
    {
        /// <summary>
        /// A texture consisting of a single white pixel
        /// </summary>
        static Texture2D pixel;

        /// <summary>
        /// Loads the necessary content for visual debugging
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public static void LoadContent(ContentManager content)
        {
            pixel = content.Load<Texture2D>("pixel");
        }

        /// <summary>
        /// Draws a visual debugging rectangle in the specified color
        /// </summary>
        /// <param name="spriteBatch">The spritebatch to use</param>
        /// <param name="rectangle">The rectangle's bounds</param>
        /// <param name="color">The rectangle's color</param>
        public static void DrawRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(pixel, rectangle, color);
        }

        public static void DrawRectangle(SpriteBatch spriteBatch, BoundingRectangle rectangle, Color color)
        {
            spriteBatch.Draw(pixel, new Rectangle((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height), color);
        }
    }
}
#endif
