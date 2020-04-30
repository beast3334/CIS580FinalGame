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
    public class CircleShooterBoss : Boss
    {
        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;
        ContentManager content;

        public override BoundingRectangle Bounds => throw new NotImplementedException();
        public CircleShooterBoss(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("CircleShooterBoss");
            bounds.Height = 200;
            bounds.Width = 200;
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }
    }
}
