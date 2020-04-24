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

        static void EnemyOnBullet(List<Enemy> enemies, Player player)
        {
            for(int i=0; i<enemies.Count; i++)
            {
                for(int j=0; j<player.BulletSpawner.Bullets.Count; j++)
                {
                    if (enemies[i].Bounds.Intersects(player.BulletSpawner.Bullets[i].Position)){
                        //player.BulletSpawner.Bullets[i].Alive = false;
                        enemies[i].Alive = false;
                    }
                }
            }
        }

        static void PlayerOnBullet(List<Enemy> enemies, Player player)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if(enemies[i].GetType() != typeof(BasicEnemy))
                {
                    ShootingEnemy tempE = (ShootingEnemy)enemies[i];
                    for (int j = 0; j < tempE.bulletSpawner.Bullets.Count; j++)
                    {
                        if (player.Bounds.Intersects(tempE.bulletSpawner.Bullets[j].Position))
                        {
                            //player.BulletSpawner.Bullets[i].Alive = false;
                            player.Alive = false;
                        }
                    }
                }
                
            }
        }

        static void PlayerOnEnemy(List<Enemy> enemies, Player player)
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
