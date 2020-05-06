using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoGameWindowsStarter.Enemies;



namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class PowerupTrackingEnemy : Powerup
    {
        Game1 game;
        Enemy enemy;
        public PowerupTrackingEnemy(Game1 game, Enemy enemy)
        {
            this.game = game;
            this.enemy = enemy;
        }
        public override string TextureName => "Bullets/Bullet_2";

        public override Vector2 Velocity => Vector2.Normalize(Vector2.Subtract((new Vector2(game.player.Bounds.X, game.player.Bounds.Y)), (new Vector2(enemy.Bounds.X, enemy.Bounds.Y)))) * 5;
        

        //public override Vector2 Scale => new Vector2(0.05f, 0.05f);
        public override Vector2 Scale => new Vector2(0.1f, 0.1f);

    }
}
