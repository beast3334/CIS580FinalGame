using Microsoft.Xna.Framework;
using MonoGameWindowsStarter.PlayerNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.PlayerNamespace.Powerups
{
    public class PlayerPowerup_Default : PlayerPowerup
    {
        public override List<Tuple<PlayerState, string>> TextureNames => new List<Tuple<PlayerState, string>> {
            new Tuple<PlayerState, string>(PlayerState.Idle, "Player/Player_1"),
            new Tuple<PlayerState, string>(PlayerState.Up, "Player/Player_4"),
            new Tuple<PlayerState, string>(PlayerState.Down, "Player/Player_4"),
            new Tuple<PlayerState, string>(PlayerState.Left, "Player/Player_3"),
            new Tuple<PlayerState, string>(PlayerState.Right, "Player/Player_3"),
        };

        public override Vector2 Scale => new Vector2(0.08f);
    }
}
