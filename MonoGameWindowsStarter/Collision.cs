using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using System.Collections.Generic;

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
