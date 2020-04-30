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
    class BasicEnemy: Enemy
    {

        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;

        public override BoundingRectangle Bounds => bounds;

        public BasicEnemy(Game1 game, ContentManager content, int position)
        {
            this.game = game;
            bounds.X = position;
            bounds.Y = 50;
            bounds.Height = 50;
            bounds.Width = 50;
            LoadContent(content);
        }


        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerShip");
        }

        public override void Update(GameTime gameTime)
        {
            bounds.Y += 1;
            if(bounds.Y>= game.GraphicsDevice.Viewport.Height)
            {
                Alive = false;
            }
            if (!Alive)
            {
                ReadyForTrash = true;
            }

            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.Red);
        }
    }
}
