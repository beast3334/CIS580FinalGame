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

namespace MonoGameWindowsStarter.Bosses
{
    enum State
    {
        Moving,
        Diving
    }
    class DivingBoss : Boss
    {
        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;
        Random random;
        ContentManager content;
        State state = State.Moving;
        double diveTimer, shootTimer;
        int speedX, speedY;

        public  override BoundingRectangle Bounds => bounds;


        public DivingBoss(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
            bounds.X = 500;
            bounds.Y = 50;
            bounds.Height = 200;
            bounds.Width = 200;
            healthCurrent = 100;
            bossName = "Divey Mcgee";
            healthMax = 100;
            speed = 3;
            healthBar = new HealthBar(game, content, this);
            
            
            bulletSpawner = new Powerups.Bullets.BulletSpawner(game, this);
            bulletSpawner.Powerup = new Powerups.Bullets.Powerups.PowerupDefaultEnemy();
            LoadContent(content);
            //setting the speed for this specific boss
            speedX = speed;
            speedY = speed * 3;

        }

        public override void LoadContent(ContentManager Content)
        {
            texture = content.Load<Texture2D>("Enemy3");

            random = new Random();

        }

        public override void Update(GameTime gameTime)
        {
            //Depends on Boss
            diveTimer += gameTime.ElapsedGameTime.TotalSeconds;
            shootTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (state == State.Moving)
            {

                if (bounds.X >= game.GraphicsDevice.Viewport.Width - bounds.Width)
                {
                    speedX *= -1;
                }
                if (bounds.X <= 0)
                {
                    speedX = speed;
                }
                bounds.X += speedX;
                if(shootTimer >= 1000)
                {
                    bulletSpawner.Shoot();
                    shootTimer = 0;
                }
                if(diveTimer >= 2)
                {
                    
                    state = State.Diving;
                    
                }
                
            }
            if (state == State.Diving)
            {
                bounds.Y += speedY;
                if(bounds.Y >= game.GraphicsDevice.Viewport.Height)
                {
                    speedY *= -3;
                }
                if(bounds.Y <= 0)
                {
                    speedY = speed * 3;
                    bounds.Y = 50;
                    state = State.Moving;
                    diveTimer = 0;
                }
            }





            //move this boss side to side if Moving is true


            bulletSpawner.Update(gameTime);
            healthBar.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            bulletSpawner.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
        }




    }
}
