using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameWindowsStarter.Powerups;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Enemies;
using MonoGameWindowsStarter.PlayerNamespace;
using System.Collections.Generic;

namespace MonoGameWindowsStarter
{
    static class Collision
    {

        public static void EnemyOnBullet(List<Enemy> enemies, BulletSpawner bulletSpawner)
        {
            foreach(Bullet bullet in bulletSpawner.Bullets)
            {
                foreach(Enemy enemy in enemies)
                {
                    if (enemy.Alive && enemy.Bounds.Intersects(bullet.Position)) {
                        bullet.Alive = false;
                        enemy.Alive = false;
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

        }

        public static void CheckAll(List<Enemy> enemies, List<BulletSpawner> bulletSpawners, Player player)
        {
            EnemyOnBullet(enemies, player.BulletSpawner);
            PlayerOnBullet(enemies, player);
            PlayerOnEnemy(enemies, player);
        }


    }
}
