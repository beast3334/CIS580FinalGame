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
    enum State
    {
        Enemy,
        Boss,
        Shop
    }
    class Director
    {
        public EnemySpawner enemySpawner;
        public BossSpawner bossSpawner;
        //BossSpawner bossSpawner;
        //PowerupSpawner powerupSpawner
        Game1 game;
        int count = 0;
        double timer = 0;
        int credits = 100;
        const int BEGINCREDITS = 100;

        State state = State.Enemy;

        public Director(Game1 game)
        {
            this.game = game;
            enemySpawner = new EnemySpawner(game);
            bossSpawner = new BossSpawner(game);
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
            enemySpawner.Update(gameTime);
            //bossSpawner.Update(gameTime);
            //powerupSpawner.Update(gameTime);

            if(state == State.Enemy && credits <= 0 && enemySpawner.Enemies.Count <= 0)
            {
                state = State.Boss;
            }
            else if(state == State.Boss)
            {
                /*if (game.BossSpawner.boss == null)
                {
                    game.upgradeMenu.isOpen = true;
                    state = State.Shop;
                }*/
            }
            else if (state == State.Shop)
            {
                if (!game.upgradeMenu.isOpen)
                {
                    state = State.Enemy;
                }
            }

            if (state == State.Enemy)
            {

                //spawn enemies
                enemySpawner.SpawnRandom();
                credits -= 5;
                if (timer >= 8)
                {
                    for (int i = 0; i <= 3; i++)
                    {
                        enemySpawner.SpawnWeak();
                        credits -= 5;
                    }
                    enemySpawner.SpawnStrong();
                    credits -= 10;
                    timer = 0;
                }

                //spawn powerups


            }
            else if (state == State.Boss)
            {

            }
            else if (state == State.Shop)
            {

            }
            

            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemySpawner.Draw(spriteBatch);
            bossSpawner.Draw(spriteBatch);
            //powerupSpawner.Draw(spriteBatch);
        }
    }
}