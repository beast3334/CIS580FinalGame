using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct BoundingRectangle
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;

        public BoundingRectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public BoundingRectangle(Rectangle rectangle)
        {
            X = rectangle.X;
            Y = rectangle.Y;
            Width = rectangle.Width;
            Height = rectangle.Height;
        }

        /// <summary>
        /// Allows the BoundingRectangle to be used as a Rectangle
        /// </summary>
        public static implicit operator Rectangle(BoundingRectangle br)
        {
            return new Rectangle(
                (int)br.X,
                (int)br.Y,
                (int)br.Width,
                (int)br.Height);
        }

        /// <summary>
        /// Multiplies the BoundingRectangle's Width and Height by the Vector2
        /// </summary>
        /// <param name="br">Bounding Rectangle</param>
        /// <param name="scale">Vector2 Scale</param>
        /// <returns></returns>
        public static BoundingRectangle operator *(BoundingRectangle br, Vector2 scale)
        {
            // Multiplies br's width and height by a Vector2 scale
            var tempVect2 = new Vector2(br.Width, br.Height) * scale;

            // Returns the new scaled BoundingRectangle
            return new BoundingRectangle(br.X, br.Y, tempVect2.X, tempVect2.Y);
        }

    }


}