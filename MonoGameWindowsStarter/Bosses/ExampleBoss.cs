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
        Game1 game;

        public override BoundingRectangle Bounds => bounds;

        public ExampleBoss(Game1 game)
        {
            this.game = game;
            bounds.X = 50;
            bounds.Y = 50;
            bounds.Height = 100;
            bounds.Width = 100;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("SPRITENAME");
        }

        public override void Update(GameTime gameTime)
        {
          //Depends on Boss
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
