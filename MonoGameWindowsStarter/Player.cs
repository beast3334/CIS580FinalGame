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
namespace MonoGameWindowsStarter
{
    enum State
    {
        Idle,
        Up,
        Down,
        Left,
        Right
    }
    public class Player
    {
        BoundingRectangle bounds;
        Game1 game;
        Texture2D texture;
        State state = State.Idle;
        public BoundingRectangle Bounds
        {
            get { return bounds; }

        }

        public Player(Game1 game)
        {
            this.game = game;
        }
        public void LoadContent(ContentManager content)
        {
            bounds.Width = 50;
            bounds.Height = 50;
            bounds.X = (game.GraphicsDevice.Viewport.Width - bounds.Width) / 2; //Places player horizontally in the middle of viewwindow
            bounds.Y = game.GraphicsDevice.Viewport.Height; //Places player at bottom of viewwindow.
            texture = content.Load<Texture2D>("playerShip");
        }
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            //Up Movement
            if(keyboardState.IsKeyDown(Keys.Up))
            {
                bounds.Y -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                state = State.Up; //For future, if adding animation to player
            }
            //Down Movement
            if(keyboardState.IsKeyDown(Keys.Down))
            {
                bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                state = State.Down; //For future, if adding animation to player
            }
            //Left Movement
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                bounds.X -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                state = State.Left; //For future, if adding animation to player
            }
            //Right Movement
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                bounds.X += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
                state = State.Right; //For future, if adding animation to player
            }
            //Idle Movement
            if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down) && keyboardState.IsKeyUp(Keys.Left) && keyboardState.IsKeyUp(Keys.Right))
            {
                state = State.Idle; //For future, if adding animation to player
            }

            //Check Y bounds
            if (bounds.Y <= 0)
            {
                bounds.Y = 0;
            }
            if(bounds.Y >= game.GraphicsDevice.Viewport.Height - bounds.Height)
            {
                bounds.Y = game.GraphicsDevice.Viewport.Height - bounds.Height;
            }
            //Check X bounds
            if(bounds.X <= 0)
            {
                bounds.X = 0;
            }
            if(bounds.X >= game.GraphicsDevice.Viewport.Width - bounds.Width)
            {
                bounds.X = game.GraphicsDevice.Viewport.Width - bounds.Width;
            }


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
         
    }
}
