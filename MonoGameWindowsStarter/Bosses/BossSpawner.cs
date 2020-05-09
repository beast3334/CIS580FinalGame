using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Bosses
{
    class BossSpawner
    {
        Game1 game;
        ContentManager content;
        public Boss boss;
        Random random = new Random((int)DateTime.UtcNow.Ticks);
        bool active = false;

        public bool Active => active;
        public BossSpawner(Game1 game)
        {
            this.game = game;
        }
        public void LoadContent(ContentManager content)
        {
            this.content = content;
        }
        public void SpawnRandom()
        {
            int r = random.Next(100, 200);
            if(r < 100)
            {
                boss = new CircleShooterBoss(game, content);
            }
            else
            {
                boss = new DivingBoss(game, content);
            }
            active = true;
        }
        public void Update(GameTime gameTime)
        {
            if(boss != null)
            {
                boss.Update(gameTime);
                if (boss.healthCurrent <= 0)
                {
                    boss = null;
                    active = false;
                }
            }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(boss != null)
            {
                boss.Draw(spriteBatch);
            }
            
        }
    }
}
