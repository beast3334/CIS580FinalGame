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
using MonoGameWindowsStarter.PlayerNamespace;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;
using MonoGameWindowsStarter.Powerups;

namespace MonoGameWindowsStarter
{
    public class UpgradeMenu
    {
        //bool to see if the shop is open
        public bool isOpen = false;

        Texture2D texture;
        BoundingRectangle bounds;
        Game1 game;
        ContentManager content;
        KeyboardState oldKeyboard;
        //all of the variables that need to be displayed as a font
        Vector2 playerSpeed = new Vector2(1,1);
        //int playerPoints = 3000;
        int playerPoints;
        int playerHearts;
        String bulletType;
        SoundEffect purchase;

        SpriteFont font;
        Player player;

        public UpgradeMenu(Game1 game, ContentManager content, Player players, int score)
        {
            this.game = game;
            this.content = content;
            bounds.X = 0;   
            bounds.Y = 0;
            bounds.Height = game.GraphicsDevice.Viewport.Height;
            bounds.Width = game.GraphicsDevice.Viewport.Width;
            //sets variables to be displayed in the menu to the same as the player's
            player = players;
            playerHearts = player.Hearts;
            playerPoints = score;
            LoadContent();
        }

        public void LoadContent()
        {
            texture = content.Load<Texture2D>("UpgradeMenu");
            font = content.Load<SpriteFont>("mainFont");
            purchase = content.Load<SoundEffect>("upgrade");
        }

        public  void Update(GameTime gameTime)
        {
            //display powerup names correctly
            if (player.BulletSpawner.Powerup.ToString() == "MonoGameWindowsStarter.Powerups.Bullets.Powerups.Powerup360Shot")
            {
                bulletType = "360";
            }
            else if (player.BulletSpawner.Powerup.ToString() == "MonoGameWindowsStarter.Powerups.Bullets.Powerups.PowerupExploding360Shot")
            {
                bulletType = "Exploding";
            }
            else if (player.BulletSpawner.Powerup.ToString() == "MonoGameWindowsStarter.Powerups.Bullets.Powerups.PowerupLaser")
            {
                bulletType = "Laser";
            }
            else if (player.BulletSpawner.Powerup.ToString() == "MonoGameWindowsStarter.Powerups.Bullets.Powerups.PowerupPenetration")
            {
                bulletType = "Penetration";
            }
            else if (player.BulletSpawner.Powerup.ToString() == "MonoGameWindowsStarter.Powerups.Bullets.Powerups.PowerupTriplePenetration")
            {
                bulletType = "Trishot";
            }
            else
            {
                bulletType = "Default";
            }
            //bulletType = player.BulletSpawner.Powerup.ToString();

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
                    player.BulletSpawner.ChangePowerup(new PowerupLaser());
                    purchase.Play();
                }
            }
            //trishot
            if ( keyboardState.IsKeyDown(Keys.D2) && !oldKeyboard.IsKeyDown(Keys.D2))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                    player.BulletSpawner.ChangePowerup(new PowerupTriplePenetration());
                    purchase.Play();
                }
            }
            //360
            if ( keyboardState.IsKeyDown(Keys.D3) && !oldKeyboard.IsKeyDown(Keys.D3))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                    player.BulletSpawner.ChangePowerup(new Powerup360Shot());
                    purchase.Play();
                }
            }
            //tripleSplit
            if ( keyboardState.IsKeyDown(Keys.D4) && !oldKeyboard.IsKeyDown(Keys.D4))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                    player.BulletSpawner.ChangePowerup(new PowerupTriplePenetration());
                    purchase.Play();
                   
                }
            }
            //exploding
            if ( keyboardState.IsKeyDown(Keys.D5) && !oldKeyboard.IsKeyDown(Keys.D5))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                    player.BulletSpawner.ChangePowerup(new PowerupExploding360Shot());
                    purchase.Play();
                }
            }
            //penetration
            if ( keyboardState.IsKeyDown(Keys.D6) && !oldKeyboard.IsKeyDown(Keys.D6))
            {
                if (playerPoints >= 200)
                {
                    playerPoints -= 200;
                    player.BulletSpawner.ChangePowerup(new PowerupPenetration());
                    purchase.Play();
                }
            }
            //heart
            if (keyboardState.IsKeyDown(Keys.H) && !oldKeyboard.IsKeyDown(Keys.H))
            {
                if (playerPoints >= 100)
                {
                    playerPoints -= 100;
                    purchase.Play();
                    player.AddHearts_PurchasedPowerup(1);
                }
            }
            //nuke
            if (keyboardState.IsKeyDown(Keys.N) && !oldKeyboard.IsKeyDown(Keys.N))
            {
                if (playerPoints >= 250)
                {
                    playerPoints -= 250;
                    purchase.Play();
                    player.Nukes++;
                }
            }
            //speed increase
            if (keyboardState.IsKeyDown(Keys.S) && !oldKeyboard.IsKeyDown(Keys.S))
            {
                if (playerPoints >= 50)
                {
                    playerPoints -= 50;
                    purchase.Play();
                    player.AddSpeed(playerSpeed);
                }
            }
            oldKeyboard = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            //draw score
            spriteBatch.DrawString(font, playerPoints.ToString(), new Vector2(game.GraphicsDevice.Viewport.Width - 1000, game.GraphicsDevice.Viewport.Height - 325), Color.White);
            //draw number of nukes
            spriteBatch.DrawString(font, player.Nukes.ToString(), new Vector2(game.GraphicsDevice.Viewport.Width - 1000, game.GraphicsDevice.Viewport.Height - 275), Color.White);
            //draw number of hearts
            spriteBatch.DrawString(font, player.Hearts.ToString(), new Vector2(game.GraphicsDevice.Viewport.Width - 1000, game.GraphicsDevice.Viewport.Height - 225), Color.White);
            //draw speed
            spriteBatch.DrawString(font, player.Velocity.X.ToString(), new Vector2(game.GraphicsDevice.Viewport.Width - 1000, game.GraphicsDevice.Viewport.Height - 175), Color.White);
            //draw current bullet type
            spriteBatch.DrawString(font, bulletType, new Vector2(game.GraphicsDevice.Viewport.Width - 1000, game.GraphicsDevice.Viewport.Height - 125), Color.White);
        }
    }
}
