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
    class DivingBoss : Boss
    {
        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;
        Random random;
        ContentManager content;
        bool attackRand = true;
        
        //adding a timer to make the boss move in different patterns
        double bossTimer;


        //a bool that if true will make the boss move back and forth
        bool moving = true;
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
            
            
            
            bulletSpawner = new Powerups.Bullets.BulletSpawner(game, bounds);
            LoadContent(content);
            //setting the speed for this specific boss
            speed = 3;

            
            

        }

        public override void LoadContent(ContentManager Content)
        {
            texture = content.Load<Texture2D>("Enemy3");
            healthBar = new HealthBar(game, content, this);
        }

        public override void Update(GameTime gameTime)
        {
            //Depends on Boss
            bossTimer += gameTime.ElapsedGameTime.TotalSeconds;


            //check to trigger different movement
            random = new Random();

            if (bossTimer > random.Next(7,11))
            {
                //random = new Random();

                if (attackRand)
                {
                    Attack1();
                }
                else
                {
                    moving = false;
                    Attack2();
                }

                    
            }

            //move this boss side to side if Moving is true

            if(moving)
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


            healthBar.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            healthBar.Draw(spriteBatch);
        }

        public void Attack1()
        {
            //once this is triggered the boss should go down towards the player bounce off the bottom sceen and go back
            bounds.Y += speed;
            if(bounds.Y >= game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
                speed *= -1;
            }
            if (bounds.Y <= 0)
            {
                bounds.Y = 0;
                //reseting the timer should stop the method from being called
                bossTimer = 0;
                attackRand = false;
            }
        }

        public void Attack2()
        {

            //once this is triggered the boss should go down towards the player bounce off the bottom sceen and go back
            bounds.Y += speed;
            if (bounds.Y >= game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
                speed *= -1;
            }
            if (bounds.Y <= 0)
            {
                bounds.Y = 0;
                //reseting the timer should stop the method from being called
                bossTimer = 0;
                moving = true;
                attackRand = true;
            }
        }


    }
}
