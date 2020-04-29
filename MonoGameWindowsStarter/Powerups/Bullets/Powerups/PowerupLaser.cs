using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class PowerupLaser : Powerup
    {
        public override string TextureName => "Bullets/Bullet_1";

        public override Vector2 Velocity => new Vector2(0, -30f);

        public override Vector2 Scale => new Vector2(0.05f, 0.5f);

        public override TimeSpan TimeBetweenBullets => new TimeSpan(0);

        public override Color Color => Color.Purple;

        public override float Damage => 5f;
    }
}
