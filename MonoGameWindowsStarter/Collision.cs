using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using System.Collections.Generic;
using System;

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
            
            foreach (BulletSpawner bs in bulletSpawner.BulletSpawners)
            {
                EnemyOnBullet(enemies, bs);
            }
            
        }

        public static void PlayerOnBullet(List<Enemy> enemies, Player player)
        {
            
            foreach(Enemy enemy in enemies)
            {
                if (enemy.GetType() != typeof(BasicEnemy))
                {
                    ShootingEnemy tempE = (ShootingEnemy)enemy;
                    foreach(Bullet b in tempE.bulletSpawner.Bullets)
                    {
                        if (player.Bounds.Intersects(b.Position))
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

                            b.Alive = false;
                            player.Alive = false;
                            b.HitEntity = true;
                        }
                    }
                }
            }
        }

        public static void PlayerOnEnemy(List<Enemy> enemies, Player player)
        {
            foreach(Enemy e in enemies)
            {
                if (player.Bounds.Intersects(e.Bounds)){
                    e.Alive = false;
                    player.Alive = false;

                    e.game.particleSystem.SpawnPerFrame = 40;
                    e.game.particleSystem.SpawnParticle = (ref Particle particle) =>
                    {

                        particle.Position = new Vector2(e.Bounds.X, e.Bounds.Y) + Vector2.Subtract(new Vector2(player.Bounds.X, player.Bounds.Y), new Vector2(e.Bounds.X, e.Bounds.Y))/2;
                        particle.Velocity = new Vector2(
                           MathHelper.Lerp(-100, 100, (float)e.game.random.NextDouble()), // X between -50 and 50
                           MathHelper.Lerp(-100, 100, (float)e.game.random.NextDouble()) // Y between 0 and 100
                           );
                        particle.Acceleration = 0.5f * new Vector2(0, (float)-e.game.random.NextDouble());
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

        public static void CheckAll(List<Enemy> enemies, List<BulletSpawner> bulletSpawners, Player player)
        {
            EnemyOnBullet(enemies, player.BulletSpawner);
            PlayerOnBullet(enemies, player);
            PlayerOnEnemy(enemies, player);
        }


    }
}
