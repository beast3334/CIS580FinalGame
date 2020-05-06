using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class Powerup360Shot : Powerup
    {
        public override string TextureName => "Bullets/Bullet_2";

        public override Vector2 Velocity => new Vector2(0, -10f);

        public override Vector2 Scale => new Vector2(0.05f, 0.05f);

        public override TimeSpan TimeBetweenBullets => new TimeSpan(0, 0, 0, 0, 500);

        public override Color Color => Color.White;

        public override float Damage => 0.5f;

        public override float RotationBetweenBullets => (float)(Math.PI / 10);

        public override int NumberToSpawnOnShoot => 20;
    }
}
