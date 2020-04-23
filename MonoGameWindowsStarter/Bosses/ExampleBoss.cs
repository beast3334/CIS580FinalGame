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
namespace MonoGameWindowsStarter.Bosses
{
    class ExampleBoss : Boss
    {
        BoundingRectangle bounds;
        Texture2D texture;
        ContentManager content;
        Game1 game;

        public override BoundingRectangle Bounds => bounds;

        public ExampleBoss(Game1 game, ContentManager content, Player player)
        {
            this.game = game;
            this.content = content;
            bounds.X = 500;
            bounds.Y = player.Bounds.Y - 700;
            bounds.Height = 200;
            bounds.Width = 200;
            LoadContent();
            //setting the speed for this specific boss
            speed = 2;
        }


        public override void LoadContent()
        {
            texture = content.Load<Texture2D>("movieTheater");
        }

        public override void Update(GameTime gameTime)
        {
            //Depends on Boss
       

            //move this boss side to side
            
            bounds.X += speed;

            //Check Y bounds
            if (bounds.Y <= 0)
            {
                bounds.Y = 0;
            }
            if (bounds.Y >= game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }

            //check x bounds 
            if (bounds.X <= 0)
            {
                bounds.X = 0;
                speed *= -1;
            }
            if (bounds.X >= game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
                speed *= -1;
            }

        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

    }
}
