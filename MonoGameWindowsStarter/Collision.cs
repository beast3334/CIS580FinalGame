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

        public static void EnemyOnBullet(List<Enemy> enemies, Player player)
        {
            foreach(Bullet b in player.BulletSpawner.Bullets)
            {
                foreach(Enemy e in enemies)
                {
                    if (e.Bounds.Intersects(b.Position)){
                        //b.Alive = false;
                        e.Alive = false;
                    }
                }
            }
            
        }

        public static void PlayerOnBullet(List<Enemy> enemies, Player player)
        {
            
            foreach(Enemy e in enemies)
            {
                if (e.GetType() != typeof(BasicEnemy))
                {
                    ShootingEnemy tempE = (ShootingEnemy)e;
                    foreach(Bullet b in tempE.bulletSpawner.Bullets)
                    {
                        if (player.Bounds.Intersects(b.Position))
                        {
                            //player.BulletSpawner.Bullets[i].Alive = false;
                            player.Alive = false;
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
            EnemyOnBullet(enemies, player);
            PlayerOnBullet(enemies, player);
            PlayerOnEnemy(enemies, player);
        }


    }
}
