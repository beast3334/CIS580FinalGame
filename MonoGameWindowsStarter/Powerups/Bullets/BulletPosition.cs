using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets
{
    public class BulletPosition
    {
        readonly EntityAlive entity;
        BoundingRectangle bounds;
        Vector2 position;
        Vector2 bulletSize = Vector2.One;

        public Vector2 Position { 
            get {
                // entity bounds is used
                if (entity != null)
                {
                    return new Vector2(entity.Bounds.X + (entity.Bounds.Width / 2) - (bulletSize.X / 2), entity.Bounds.Y);
                }
                // position is used
                else if (position != null)
                {
                    return position;
                }
                // bounds is used
                else
                {
                    return new Vector2(bounds.X + (bounds.Width / 2) - (bulletSize.X / 2), bounds.Y);
                }
            }
        }

        public BulletPosition(EntityAlive entity, BulletSpawner bulletSpawner)
        {
            this.entity = entity;
        }

        public BulletPosition(BoundingRectangle bounds, BulletSpawner bulletSpawner)
        {
            this.bounds = bounds;
        }

        public BulletPosition(Vector2 position)
        {
            this.position = position;
        }
    }
}
