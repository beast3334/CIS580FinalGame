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
        public PowerupSpawner powerupSpawner;
        Game1 game;
        int count = 0;
        double timer = 0;
        int credits;
        int BEGINCREDITS = 100;
        Random random;
        int r;

        State state;

        public Director(Game1 game)
        {
            this.game = game;
            enemySpawner = new EnemySpawner(game);
            bossSpawner = new BossSpawner(game);
            powerupSpawner = new PowerupSpawner(game);
            random = new Random();
        }

        public void LoadContent(ContentManager content)
        {
            enemySpawner.LoadContent(content);
            bossSpawner.LoadContent(content);
            powerupSpawner.LoadContent(content);
            credits = BEGINCREDITS;
            state = State.Enemy;
        }

        public void Update(GameTime gameTime)
        {
            if( state == State.Enemy && credits == 0 && count == 1)
            {
                credits = BEGINCREDITS;
            }
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            

            if(state == State.Enemy && credits <= 0 && enemySpawner.Enemies.Count <= 0)
            {
                bossSpawner.SpawnRandom();
                state = State.Boss;
            }
            else if(state == State.Boss)
            {
                if (bossSpawner.boss == null)
                {
                    game.upgradeMenu.isOpen = true;
                    state = State.Shop;
                    game.Score += 100;
                }
            }
            else if (state == State.Shop)
            {
                if (!game.upgradeMenu.isOpen)
                {
                    state = State.Enemy;
                    credits = BEGINCREDITS;
                    game.Wave++;
                }
            }

            if (state == State.Enemy)
            {
                enemySpawner.Update(gameTime);

                powerupSpawner.Update();
                if (credits > 0)
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
                }

                //spawn powerups
                r = random.Next(1000);
                // r = (int)(random.NextDouble() * 100);
                if (r >= 998)
                {
                    // r = random.Next(0, 20);
                    r = random.Next(0, 10);
                    if (r >= 9)
                    {
                        powerupSpawner.SpawnRandom(PowerupSpriteCategory.SpawnStrongPowerup);
                        //credits += 15;
                    }
                    else
                    {
                        powerupSpawner.SpawnRandom(PowerupSpriteCategory.SpawnWeakPowerup);
                        //credits += 5;
                    }
                }


            }
            else if (state == State.Boss)
            {
                bossSpawner.Update(gameTime);
            }
            else if (state == State.Shop)
            {

            }
            if (count ==0)
                count = 1;
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemySpawner.Draw(spriteBatch);
            bossSpawner.Draw(spriteBatch);
            powerupSpawner.Draw(spriteBatch);
        }
    }
}