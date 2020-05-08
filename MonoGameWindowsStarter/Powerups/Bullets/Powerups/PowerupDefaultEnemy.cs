using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class PowerupDefaultEnemy : Powerup
    {
        public override string TextureName => "Bullets/Bullet_1";

        public override Vector2 Velocity => new Vector2(0, 10);

        public override Vector2 Scale => new Vector2(0.05f, 0.05f);
    }
}
