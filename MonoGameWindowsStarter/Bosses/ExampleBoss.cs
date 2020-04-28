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
//using MonoGameWindowsStarter.Powerups.Bullets;
//using MonoGameWindowsStarter.Powerups.Bullets.Powerups;


namespace MonoGameWindowsStarter.Bosses
{
    class ExampleBoss : Boss
    {
        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;
        Random random;

        //adding a timer to make the boss move in different patterns
        double bossTimer;

        //added to make boss shoot at the player
        //public BulletSpawner bulletSpawner;
        double shootTimer;

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
            //adding in shooting for the boss
            //bulletSpawner = new BulletSpawner(game, this);
            //bulletSpawner.Powerup = new PowerupDefaultEnemy();

            random = new Random();


        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("movieTheater");
           // bulletSpawner.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            //Depends on Boss
            bossTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //adds shooting
            //shootTimer += gameTime.ElapsedGameTime.TotalSeconds;
            //if (shootTimer >= 1.5)
            //{
            //    bulletSpawner.Shoot();
            //    shootTimer = 0;
            //}

            //check to trigger different movement
            if(bossTimer > random.Next(7,11))
            {
                Attack();
            }

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


            //update bulletspawner
            //bulletSpawner.Update(gameTime);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
           // bulletSpawner.Draw(spriteBatch);
        }

        public void Attack()
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
            }

        }
    }
}
