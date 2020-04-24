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
    class ShootingEnemy: Enemy
    {

        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;
        int speed;
        public BulletSpawner bulletSpawner;
        double shootTimer;
        int direction;
        

        public override BoundingRectangle Bounds => bounds;

        public ShootingEnemy(Game1 game, ContentManager content)
        {
            this.game = game;
            bounds.X = 50;
            bounds.Y = 50;
            bounds.Height = 50;
            bounds.Width = 50;
            direction = 0;
            speed = 3;
            bulletSpawner = new BulletSpawner(game, this);
            bulletSpawner.Powerup = new PowerupDefaultEnemy();
            //bulletSpawner.Powerup.Velocity.Y *= -1; 
            LoadContent(content);
        }


        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("playerShip");
            bulletSpawner.LoadContent(content);

            
        }

        public override void Update(GameTime gameTime)
        {
            shootTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(shootTimer >= 1.5)
            {
                bulletSpawner.Shoot();
                shootTimer = 0;
            }
            
            if (bounds.X + bounds.Width >= game.GraphicsDevice.Viewport.Width)
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
            }
            bulletSpawner.Update(gameTime);

            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.Green);
            bulletSpawner.Draw(spriteBatch);
        }
    }
}
