﻿using Microsoft.Xna.Framework;
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

namespace MonoGameWindowsStarter
{
    class Director
    {
        public EnemySpawner enemySpawner;
        //BossSpawner bossSpawner;
        //PowerupSpawner powerupSpawner
        Game1 game;

        public Director(Game1 game)
        {
            this.game = game;
            enemySpawner = new EnemySpawner(game);
            //bossSpawner = new BossSpawner(game);
            //powerupSpawner = new PowerupSpawner(game);
        }

        public void LoadContent(ContentManager content)
        {
            enemySpawner.LoadContent(content);
            //bossSpawner.LoadContent(content);
            //powerupSpawner.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            enemySpawner.Update(gameTime);
            //bossSpawner.Update(gameTime);
            //powerupSpawner.Update(gameTime);

            enemySpawner.SpawnRandom();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemySpawner.Draw(spriteBatch);
            //bossSpawner.Draw(spriteBatch);
            //powerupSpawner.Draw(spriteBatch);
        }
    }
}