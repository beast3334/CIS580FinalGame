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
    public class UpgradeMenu
    {
        //bool to see if the shop is open
        public bool isOpen = true;

        Texture2D texture;
        BoundingRectangle bounds;
        Game1 game;
        ContentManager content;
        KeyboardState oldKeyboard;
        //all of the variables that need to be displayed as a font
        int playerSpeed = 1;
        int nukeNumber = 0;
        int playerPoints = 3000;
        int playerHearts = 3;
        String bulletType;

        SpriteFont font;

        public UpgradeMenu(Game1 game, ContentManager content, Player player)
        {
            this.game = game;
            this.content = content;
            bounds.X = 0;   
            bounds.Y = 0;
            bounds.Height = game.GraphicsDevice.Viewport.Height;
            bounds.Width = game.GraphicsDevice.Viewport.Width;
            //sets variables to be displayed in the menu to the same as the player's
            //playerSpeed = player.speed;
            // bulletType = player.BulletSpawner.GetType().ToString();
            bulletType = player.BulletSpawner.Powerup.ToString();
            LoadContent();
        }

        public void LoadContent()
        {
            texture = content.Load<Texture2D>("UpgradeMenu");
            font = content.Load<SpriteFont>("font");
        }

        public  void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            //resume
            if (keyboardState.IsKeyDown(Keys.Enter) )
            {
                isOpen = false;
            }
            //laser
            if ( keyboardState.IsKeyDown(Keys.D1) && !oldKeyboard.IsKeyDown(Keys.D1))
            {
               if(playerPoints >= 200)
                {
                    playerPoints -= 200;
                }
            }
            //trishot
            if ( keyboardState.IsKeyDown(Keys.D2) && !oldKeyboard.IsKeyDown(Keys.D2))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                }
            }
            //360
            if ( keyboardState.IsKeyDown(Keys.D3) && !oldKeyboard.IsKeyDown(Keys.D3))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                }
            }
            //tripleSplit
            if ( keyboardState.IsKeyDown(Keys.D4) && !oldKeyboard.IsKeyDown(Keys.D4))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                }
            }
            //exploding
            if ( keyboardState.IsKeyDown(Keys.D5) && !oldKeyboard.IsKeyDown(Keys.D5))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                }
            }
            //penetration
            if ( keyboardState.IsKeyDown(Keys.D6) && !oldKeyboard.IsKeyDown(Keys.D6))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                }
            }
            //heart
            if (keyboardState.IsKeyDown(Keys.H) && !oldKeyboard.IsKeyDown(Keys.H))
            {
                if (playerPoints >= 100)
                {
                    playerPoints -= 100;
                    playerHearts++;
                }
            }
            //nuke
            if (keyboardState.IsKeyDown(Keys.N) && !oldKeyboard.IsKeyDown(Keys.N))
            {
                if (playerPoints >= 250)
                {
                    playerPoints -= 250;
                    nukeNumber++;
                }
            }
            //speed increase
            if (keyboardState.IsKeyDown(Keys.S) && !oldKeyboard.IsKeyDown(Keys.S))
            {
                if (playerPoints >= 50)
                {
                    playerPoints -= 50;
                    playerSpeed++;
                }
            }
            oldKeyboard = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            //draw score
            spriteBatch.DrawString(font, playerPoints.ToString(), new Vector2(610, 505), Color.White);
            //draw number of nukes
            spriteBatch.DrawString(font, nukeNumber.ToString(), new Vector2(610, 545), Color.White);
            //draw number of hearts
            spriteBatch.DrawString(font, playerHearts.ToString(), new Vector2(610, 587), Color.White);
            //draw speed
            spriteBatch.DrawString(font, playerSpeed.ToString(), new Vector2(610, 627), Color.White);
            //draw current bullet type
            spriteBatch.DrawString(font, bulletType, new Vector2(610, 670), Color.White);
        }
    }
}
