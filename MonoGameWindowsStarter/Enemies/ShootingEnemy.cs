using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using MonoGameWindowsStarter.Powerups.Bullets;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;

namespace MonoGameWindowsStarter.Enemies
{
    abstract class ShootingEnemy : Enemy
    {
        public BulletSpawner bulletSpawner;

    }
}
