﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Powerups.Bullets.Powerups
{
    public class PowerupTriplePenetration : Powerup
    {
        public override string TextureName => "Bullets/Bullet_2";

        public override Vector2 Scale => new Vector2(0.04f, 0.09f);

        public override Vector2 Velocity => new Vector2(0, -10f);

        public override Powerup SpawnAfterImpact => new PowerupTriplePenetration();

        public override int NumberToSpawnOnShoot => 3;

        public override int Damage => 2;
    }
}
