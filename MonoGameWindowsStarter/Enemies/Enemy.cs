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

namespace MonoGameWindowsStarter.Enemies
{
    public abstract class Enemy : EntityAlive
    {

        //Extend with any methods required by all enemies.

        //Will allow for abstract access to all enemies, instead of by name.
        //public bool Alive { get; set; } = true;
        public bool ReadyForTrash = false;
        public int Health = 0;
        public int points = 100;
        public Game1 game;

    }
}
