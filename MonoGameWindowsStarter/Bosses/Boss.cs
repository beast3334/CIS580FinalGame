using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
namespace MonoGameWindowsStarter.Bosses
{
    /// <summary>
    /// An abstract class containing all the items that an in game boss must possess
    /// </summary>
    public abstract class Boss : EntityAlive
    {
        //Additonal methods can be defined here that apply to all bosses in the game.

        //Having an abstract class will allow for referecence to all bosses abstractly, rather than by name.
    }
}
