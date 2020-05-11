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
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;

namespace MonoGameWindowsStarter.Enemies
{
    class SwervingEnemy: ShootingEnemy
    {

        BoundingRectangle bounds;
        Texture2D texture;
        //Game1 game;
        int speed;
        //public BulletSpawner bulletSpawner;
        double shootTimer;
        int direction;
        //public bool ReadyForTrash = false;
        Vector2 Velocity;
        Vector2 Acceleration;
        

        public override BoundingRectangle Bounds => bounds;

        public SwervingEnemy(Game1 game, ContentManager content, int position)
        {
            this.game = game;
            bounds.X = position;
            bounds.Y = 20;
            bounds.Height = 45;
            bounds.Width = 45;
            direction = 0;
            speed = 3;
            bulletSpawner = new BulletSpawner(game, this);
            bulletSpawner.Powerup = new PowerupDefaultEnemy();
            //bulletSpawner.Powerup.Velocity.Y *= -1; 
            LoadContent(content);
            Acceleration = new Vector2(0.1f, 0);
            Velocity = new Vector2(0, 1);
            Health = 1;
        }


        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Enemy3");
            bulletSpawner.LoadContent(content);

            
        }

        public override void Update(GameTime gameTime)
        {
            //first checks if entity is on screen
            if (bounds.Y >= game.GraphicsDevice.Viewport.Height)
            {
                Alive = false;
            }

            Vector2 Position = new Vector2(bounds.X, bounds.Y);
            Velocity += Acceleration;
            Position += Velocity;
            bounds.X = Position.X;
            bounds.Y = Position.Y;

            shootTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(shootTimer >= 1.5 && Alive)
            {
                bulletSpawner.Shoot();
                shootTimer = 0;
            }
            
            if(Velocity.X >= 5)
            {
                Acceleration = new Vector2(-0.1f, 0);
            }else if(Velocity.X <= -5)
            {
                Acceleration = new Vector2(0.1f, 0);
            }
            /*if (bounds.X + bounds.Width >= game.GraphicsDevice.Viewport.Width)
            {
                direction = 1;
                
            }else if(bounds.X <= 0)
            {
                direction = 0;
                
            }
            if (direction == 0)
            {
                bounds.X += speed;
            }else if(direction == 1)
            {
                bounds.X -= speed;
            }*/

            bulletSpawner.Update(gameTime);
            if(bulletSpawner.Bullets.Count == 0 && !Alive)
            {
                ReadyForTrash = true;
            }

            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Alive)
            {
                spriteBatch.Draw(texture, bounds, Color.White);
            }
            bulletSpawner.Draw(spriteBatch);
        }
    }
}
