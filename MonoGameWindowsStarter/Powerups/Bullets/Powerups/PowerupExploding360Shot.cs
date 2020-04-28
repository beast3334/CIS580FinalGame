using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class PowerupExploding360Shot : Powerup
    {
        public override string TextureName => "Bullets/Bullet_3";

        public override Vector2 Velocity => new Vector2(0, -5f);

        public override Vector2 Scale => new Vector2(0.05f, 0.05f);

        public override TimeSpan TimeBetweenBullets => new TimeSpan(0, 0, 0, 1);

        public override Color Color => Color.Green;

        public override float Damage => 1f;

        public override Powerup SpawnAfterImpact => new Powerup360Shot();
    }
}
