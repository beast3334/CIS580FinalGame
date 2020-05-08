using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameWindowsStarter.Bosses;
namespace MonoGameWindowsStarter
{
    class Director
    {
        public EnemySpawner enemySpawner;
        public BossSpawner bossSpawner;
        //BossSpawner bossSpawner;
        //PowerupSpawner powerupSpawner
        Game1 game;
        double timer;
        int enemyTotal;
        int enemyCounter;

        public Director(Game1 game)
        {
            this.game = game;
            enemySpawner = new EnemySpawner(game);
            bossSpawner = new BossSpawner(game);
            //bossSpawner = new BossSpawner(game);
            //powerupSpawner = new PowerupSpawner(game);
        }

        public void LoadContent(ContentManager content)
        {
            enemySpawner.LoadContent(content);
            bossSpawner.LoadContent(content);
            //powerupSpawner.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {

            timer += gameTime.ElapsedGameTime.TotalSeconds;
            enemyTotal = (game.Wave * 8)/2;
            if(enemyCounter <= enemyTotal)
            {
                if (timer >= 2)
                {
                    timer = 0;
                    enemySpawner.SpawnRandom();
                    enemyCounter++;
                }
            }
            else
            {
                bossSpawner.SpawnRandom();

            }

            enemySpawner.Update(gameTime);
            if(bossSpawner.active)
            {
                bossSpawner.Update(gameTime);
            }
            
            //powerupSpawner.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemySpawner.Draw(spriteBatch);
            if(bossSpawner.active)
            {
                bossSpawner.Draw(spriteBatch);
            }
            
            //powerupSpawner.Draw(spriteBatch);
        }
    }
}
