﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using MonoGameWindowsStarter.PlayerNamespace;
using System.Collections.Generic;
using MonoGameWindowsStarter.Bosses;
namespace MonoGameWindowsStarter
{
    static class Collision
    {
        //static int counter = 0;

        public static void EnemyOnBullet(List<Enemy> enemies, BulletSpawner bulletSpawner)
        {
            foreach(Bullet bullet in bulletSpawner.Bullets)
            {
                foreach(Enemy enemy in enemies)
                {
                    if (enemy.Alive && enemy.Bounds.Intersects(bullet.Position)) {
                        bullet.Alive = false;
                        
                            if (enemy.Health <= 1)
                            {
                                enemy.Alive = false;
                            }
                            else
                            {
                                enemy.Health--;
                            }
                        
                        bullet.HitEntity = true;
                    }
                }
            }
            
            // Check on the bullet spawners inside the bullet spawner
            foreach (BulletSpawner bs in bulletSpawner.BulletSpawners)
            {
                EnemyOnBullet(enemies, bs);
            }
            
        }

        /// <summary>
        /// Method detects if an enemy bullet hits the player
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="player"></param>
        public static void PlayerOnBullet(List<Enemy> enemies, Player player)
        {
            foreach(Enemy enemy in enemies)
            {
                if (enemy.GetType() != typeof(BasicEnemy))
                {
                    ShootingEnemy tempE = (ShootingEnemy)enemy;
                    foreach(Bullet bullet in tempE.bulletSpawner.Bullets)
                    {
                        if (player.Bounds.Intersects(bullet.Position))
                        {
                            bullet.Alive = false;
                            bullet.HitEntity = true;
                            player.Hearts -= bullet.Damage;
                        }
                    }

                    // Go through the bullet spawners inside the bullet spawner
                    foreach (BulletSpawner bs in tempE.bulletSpawner.BulletSpawners)
                    {
                        PlayerOnBullet(bs, player);
                    }
                }
            }
        }

        /// <summary>
        /// Detects if bullets from a bullet spawner have hit the player
        /// </summary>
        /// <param name="bulletSpawner">Bullet Spawner to detect bullets from</param>
        /// <param name="player">Player to detect</param>
        private static void PlayerOnBullet(BulletSpawner bulletSpawner, Player player)
        {
            foreach (Bullet bullet in bulletSpawner.Bullets)
            {
                if (bullet.Bounds.Intersects(player.Bounds))
                {
                    player.Hearts--;
                }
            }

            foreach (BulletSpawner bs in bulletSpawner.BulletSpawners)
            {
                PlayerOnBullet(bs, player);
            }
        }

        /// <summary>
        /// Detects if the player collides with an enemy
        /// </summary>
        /// <param name="enemies">Enemies to check</param>
        /// <param name="player">Player to check</param>
        public static void PlayerOnEnemy(List<Enemy> enemies, Player player)
        {
            foreach(Enemy enemy in enemies)
            {
                if (player.Bounds.Intersects(enemy.Bounds)) {
                    enemy.Alive = false;
                    player.Hearts--;
                }
            }
        }
        public static void BossOnBullet(Bosses.Boss boss, BulletSpawner bulletSpawner)
        {
            foreach (Bullet bullet in bulletSpawner.Bullets)
            {
                if (bullet.Bounds.Intersects(boss.Bounds))
                {
                    boss.healthCurrent -= (int)bullet.Damage;
                    bullet.Alive = false;
                    boss.healthBar.Update();
                }
            }
        }
        public static void PlayeronBoss(Bosses.Boss boss, Player player)
        {
            foreach (Bullet bullet in boss.bulletSpawner.Bullets)
            {
                if (bullet.Bounds.Intersects(player.Bounds))
                {
                    player.Hearts--;
                    bullet.Alive = false;
                }
            }
            if(player.Bounds.Intersects(boss.Bounds))
            {
                player.Hearts--;
            }
            
        }

        /// <summary>
        /// Checks all the collisions possible
        /// </summary>
        /// <param name="enemies">Enemies to check</param>
        /// <param name="player">Player to check</param>
        public static void CheckAll(List<Enemy> enemies, Player player, Boss boss)
        {
            EnemyOnBullet(enemies, player.BulletSpawner);
            PlayerOnBullet(enemies, player);
            PlayerOnEnemy(enemies, player);
            if(boss != null)
            {
                BossOnBullet(boss, player.BulletSpawner);
                PlayeronBoss(boss, player);
            }
            
        }


    }
}
