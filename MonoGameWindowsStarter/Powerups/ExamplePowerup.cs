﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace MonoGameWindowsStarter.Powerups
{
    public class ExamplePowerup : Powerup
    {
        BoundingRectangle bounds;
        Texture2D texture;
        ContentManager content;
        Game1 game;

        public override BoundingRectangle Bounds => bounds;

        public ExamplePowerup(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
            bounds.X = 50;
            bounds.Y = 50;
            bounds.Height = 100;
            bounds.Width = 100;
            LoadContent();
        }


        public override void LoadContent()
        {
            texture = content.Load<Texture2D>("SPRITENAME");
        }

        public override void Update(GameTime gameTime)
        {
            //Depends on Powerups
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

    }
}
