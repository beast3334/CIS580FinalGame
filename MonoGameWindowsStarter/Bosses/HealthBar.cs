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
    public class HealthBar
    {
        Game1 game;
        ContentManager content;
        Texture2D texture;
        Boss bossHost;
        SpriteFont bossFont;
        float nameSize;
        BoundingRectangle bounds;
        int initial_width = 500;
        public BoundingRectangle Bounds => bounds;
        public HealthBar(Game1 game, ContentManager content, Boss bossHost)
        {
            this.game = game;
            this.content = content;
            this.bossHost = bossHost;
            bounds.Width = 500;
            bounds.Height = 10;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2 - bounds.Width / 2;
            bounds.Y = 100;
            LoadContent();
        }
        public void LoadContent()
        {
            texture = content.Load<Texture2D>("Health_Bar");
            bossFont = content.Load<SpriteFont>("BossName");
            nameSize = bossFont.MeasureString(bossHost.bossName).X;
        }
        public void Update()
        {
            bounds.Width = (bossHost.healthCurrent / bossHost.healthMax) * initial_width;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.DrawString(bossFont, bossHost.bossName, new Vector2(bounds.X + 250 - nameSize, bounds.Y - 50), Color.White);
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}