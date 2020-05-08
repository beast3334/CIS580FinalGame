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
namespace MonoGameWindowsStarter.Bosses
{
    public class CircleShooterBoss : Boss
    {
        BoundingRectangle bounds;
        Texture2D texture;
        Game1 game;
        ContentManager content;
        BulletSpawner bulletSpawner;
        int direction = 0;
        double shootTimer = 0;
        double angle;
        

        public override BoundingRectangle Bounds => bounds;
        public CircleShooterBoss(Game1 game, ContentManager content)
        {
            this.game = game;
            this.content = content;
            bounds.X = game.GraphicsDevice.Viewport.Width / 2;
            bounds.Y = 50;
            bounds.Height = 100;
            bounds.Width = 100;
            speed = 2;
            healthMax = 100;
            bossName = "McCircly";
            healthCurrent = healthMax;
            bulletSpawner = new BulletSpawner(game, this);
            bulletSpawner.Powerup = new Powerup360Shot();
            LoadContent(content);
            bulletSpawner.LoadContent(content);

        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("CircleShooterBoss");
            bounds.Height = 200;
            bounds.Width = 200;
            healthBar = new HealthBar(game, content, this);
        }

        public override void Update(GameTime gameTime)
        {
            shootTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            angle += gameTime.ElapsedGameTime.TotalSeconds * speed;

            bounds.X = (float)(game.GraphicsDevice.Viewport.Width / 2 + Math.Cos(angle) * 400);
            bounds.Y = (float)(75 + Math.Sin(angle) * 250);

            if (shootTimer >= 2000 && Alive)
            {
                bulletSpawner.Shoot();
                shootTimer = 0;
            }
            bulletSpawner.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
            bulletSpawner.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
        }
    }
}
