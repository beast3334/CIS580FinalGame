using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class PowerupAcceleration : Powerup
    {
        public override string TextureName => "Bullets/Bullet_4";

        public override Vector2 Scale => new Vector2(0.04f, 0.09f);

        public override Vector2 Velocity => new Vector2(0, 0);

        public override Vector2 Acceleration => new Vector2(0, -1);

        public override int NumberToSpawnOnShoot => 3;
    }
}
