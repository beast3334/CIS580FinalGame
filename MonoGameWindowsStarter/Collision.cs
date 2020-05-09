﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Powerups;
using Microsoft.Xna.Framework.Audio;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using MonoGameWindowsStarter.PlayerNamespace;
using System.Collections.Generic;
using MonoGameWindowsStarter.Bosses;
using System;
using MonoGameWindowsStarter.PlayerNamespace.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;

namespace MonoGameWindowsStarter
{
    static class Collision
    {
        //static int counter = 0;

        public static void EnemyOnBullet(List<EntityAlive> enemies, BulletSpawner bulletSpawner, SoundEffect enemyKilled)
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
                                enemyKilled.Play();
                            
                                enemy.game.particleSystem.SpawnPerFrame = 20;
                                enemy.game.particleSystem.SpawnParticle = (ref Particle particle) =>
                                {

                                      particle.Position = new Vector2(enemy.Bounds.X+enemy.Bounds.Width/2, enemy.Bounds.Y+enemy.Bounds.Height/2);
                                      particle.Velocity = new Vector2(
                                         MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()), // X between -50 and 50
                                         MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()) // Y between 0 and 100
                                         );
                                      particle.Acceleration = 0.1f * new Vector2(0, (float)-enemy.game.random.NextDouble());
                                      particle.Color = Color.Gold;
                                       particle.Scale = 1.5f;
                                       particle.Life = .3f;
                                };
                            
                        }
                            else
                            {
                                enemy.game.particleSystem.SpawnPerFrame = 3;
                                enemy.game.particleSystem.SpawnParticle = (ref Particle particle) =>
                                {

                                    particle.Position = new Vector2(enemy.Bounds.X + enemy.Bounds.Width / 2, enemy.Bounds.Y + enemy.Bounds.Height / 2);
                                    particle.Velocity = new Vector2(
                                        MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()), // X between -50 and 50
                                        MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()) // Y between 0 and 100
                                        );
                                    particle.Acceleration = 0.1f * new Vector2(0, (float)-enemy.game.random.NextDouble());
                                    particle.Color = Color.Gold;
                                    particle.Scale = 1.0f;
                                    particle.Life = .3f;
                                };
                            enemy.Health-=(int)bullet.Damage;
                            }
                        
                        bullet.HitEntity = true;
                    }
                }
            }
            
            // Check on the bullet spawners inside the bullet spawner
            foreach (BulletSpawner bs in bulletSpawner.BulletSpawners)
            {
                EnemyOnBullet(enemies, bs, enemyKilled);
            }
            
        }

        /// <summary>
        /// Method detects if an enemy bullet hits the player
        /// </summary>
        /// <param name="enemies"></param>
        /// <param name="player"></param>
        public static void PlayerOnBullet(List<EntityAlive> enemies, Player player, SoundEffect playerHit)
        {
            foreach(Enemy enemy in enemies)
            {
                if (enemy.GetType() != typeof(BasicEnemy))
                {
                    ShootingEnemy tempE = (ShootingEnemy)enemy;
                    foreach(Bullet bullet in tempE.bulletSpawner.Bullets)
                    {
                        if (player.Bounds.Intersects(bullet.Position) && player.Alive)
                        {
                            enemy.game.particleSystem.SpawnPerFrame = 30;
                            enemy.game.particleSystem.SpawnParticle = (ref Particle particle) =>
                            {

                                particle.Position = new Vector2(player.Bounds.X + player.Bounds.Width / 2, player.Bounds.Y + player.Bounds.Height / 2);
                                particle.Velocity = new Vector2(
                                   MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()), // X between -50 and 50
                                   MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()) // Y between 0 and 100
                                   );
                                particle.Acceleration = 0.2f * new Vector2(0, (float)-enemy.game.random.NextDouble());
                                particle.Color = Color.Gold;
                                particle.Scale = 2f;
                                particle.Life = .8f;
                            };

                            bullet.Alive = false;
                            player.Hearts -= bullet.Damage;
                            bullet.HitEntity = true;
                            playerHit.Play();
                        }
                    }
                    // Go through the bullet spawners inside the bullet spawner
                    foreach (BulletSpawner bs in tempE.bulletSpawner.BulletSpawners)
                    {
                        PlayerOnBullet(bs, player, playerHit);
                    }
                }
            }
        }

        /// <summary>
        /// Detects if bullets from a bullet spawner have hit the player
        /// </summary>
        /// <param name="bulletSpawner">Bullet Spawner to detect bullets from</param>
        /// <param name="player">Player to detect</param>
        private static void PlayerOnBullet(BulletSpawner bulletSpawner, Player player, SoundEffect playerHit)
        {
            foreach (Bullet bullet in bulletSpawner.Bullets)
            {
                if (bullet.Bounds.Intersects(player.Bounds) && player.Alive)
                {
                    player.Hearts--;
                    playerHit.Play();
                }
            }

            foreach (BulletSpawner bs in bulletSpawner.BulletSpawners)
            {
                PlayerOnBullet(bs, player, playerHit);
            }
        }
        

        /// <summary>
        /// Detects if the player collides with an enemy
        /// </summary>
        /// <param name="enemies">Enemies to check</param>
        /// <param name="player">Player to check</param>
        public static void PlayerOnEnemy(List<EntityAlive> enemies, Player player, SoundEffect playerHit)
        {
            foreach(Enemy enemy in enemies)
            {
                if (player.Bounds.Intersects(enemy.Bounds) && player.Alive){
                    enemy.Alive = false;
                    player.Hearts--;
                    playerHit.Play();

                    enemy.game.particleSystem.SpawnPerFrame = 40;
                    enemy.game.particleSystem.SpawnParticle = (ref Particle particle) =>
                    {

                        particle.Position = new Vector2(enemy.Bounds.X, enemy.Bounds.Y) + Vector2.Subtract(new Vector2(player.Bounds.X, player.Bounds.Y), new Vector2(enemy.Bounds.X, enemy.Bounds.Y))/2;
                        particle.Velocity = new Vector2(
                           MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()), // X between -50 and 50
                           MathHelper.Lerp(-100, 100, (float)enemy.game.random.NextDouble()) // Y between 0 and 100
                           );
                        particle.Acceleration = 0.5f * new Vector2(0, (float)-enemy.game.random.NextDouble());
                        particle.Color = Color.Gold;
                        particle.Scale = 1.5f;
                        particle.Life = .3f;
                    };

                    /*e.game.playerParticle.SpawnParticle = (ref Particle particle) =>
                    {
                        MouseState mouse = Mouse.GetState();
                        particle.Position = new Vector2(player.Bounds.X + player.Bounds.Width / 3, player.Bounds.Y + player.Bounds.Height);
                        particle.Velocity = new Vector2(
                            MathHelper.Lerp(-50, 50, (float)e.game.random.NextDouble()), // X between -50 and 50
                            MathHelper.Lerp(-50, 50, (float)e.game.random.NextDouble()) // Y between 0 and 100
                            );
                        particle.Acceleration = 0.1f * new Vector2(0, (float)-e.game.random.NextDouble());
                        particle.Color = Color.LightYellow;
                        particle.Scale = .5f;
                        particle.Life = .5f;
                    };*/
                
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
        public static void PlayeronBoss(Bosses.Boss boss, Player player, SoundEffect playerHit)
        {
            foreach (Bullet bullet in boss.bulletSpawner.Bullets)
            {
                if (bullet.Bounds.Intersects(player.Bounds))
                {
                    player.Hearts--;
                    bullet.Alive = false;
                    playerHit.Play();
                }
            }
            if(player.Bounds.Intersects(boss.Bounds))
            {
                player.Hearts--;
            }
            
        }
        /*
        ExplodingShot,
        Heart_Powerup,
        Laser_Powerup,
        Nuke_Powerup,
        PenetrationShot,
        Speed_Powerup,
        TripleSplitShot,
        Trishot_Powerup
         */
        public static void PlayerOnPowerup(Player player, PowerupSpawner powerups)
        {
            foreach(PowerupSprite powerup in powerups.PowerupSprites)
            {
                if (player.Bounds.Intersects(new BoundingRectangle(powerup.Position.X, powerup.Position.Y, 50, 50)))
                {
                    switch (powerup.Powerup.Item2.ToString())
                    {
                        case "ThreeSixtyShot":
                            player.ChangePowerup_PickedUp(new Powerup360Shot());
                            break;
                        case "ExplodingShot":
                            player.ChangePowerup_PickedUp(new PowerupExploding360Shot());
                            break;
                        
                        case "Laser_Powerup":
                            player.ChangePowerup_PickedUp(new PowerupLaser());
                            break;
                        
                        case "PenetrationShot":
                            player.ChangePowerup_PickedUp(new PowerupPenetration());
                            break;
                        
                        case "TripleSplitShot":
                            player.ChangePowerup_PickedUp(new PowerupTriplePenetration());
                            break;
                        case "Trishot_Powerup":
                            player.ChangePowerup_PickedUp(new PowerupTriplePenetration());
                            break;
                        case "Nuke_Powerup":
                            player.Nukes++;
                            break;
                        case "Speed_Powerup":
                            player.AddSpeed(new Vector2(1, 1));
                            break;
                        case "Heart_Powerup":
                            player.AddHearts_PickedUpPowerup(1);
                            break;

                    }
                    powerup.Alive = false;
                    
                }
            }
        }

        /// <summary>
        /// Checks all the collisions possible
        /// </summary>
        /// <param name="enemies">Enemies to check</param>
        /// <param name="player">Player to check</param>
        public static void CheckAll(List<EntityAlive> enemies, Player player, Boss boss, PowerupSpawner powerups, SoundEffect enemyKilled, SoundEffect playerHit)
        {
            EnemyOnBullet(enemies, player.BulletSpawner, enemyKilled);
            PlayerOnBullet(enemies, player, playerHit);
            PlayerOnEnemy(enemies, player, playerHit);
            if(boss != null)
            {
                BossOnBullet(boss, player.BulletSpawner);
                PlayeronBoss(boss, player, playerHit);
            }
            PlayerOnPowerup(player, powerups);
            
        }


    }
}
