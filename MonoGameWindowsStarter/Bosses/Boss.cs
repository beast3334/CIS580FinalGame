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

        //how much health each boss will be able to take
        public int health;
        //bool to activate when it is time for a boss to appear
        public bool active;
        //speed of the boss's movement
        public int speed;
        public abstract BoundingRectangle Bounds
        { get; }
        
        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public abstract void Draw(SpriteBatch spriteBatch);


        //Additonal methods can be defined here that apply to all bosses in the game.

        //Having an abstract class will allow for referecence to all bosses abstractly, rather than by name.
    }
}
