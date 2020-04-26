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
    public class Background
    {
        const int MAXTILES = 3; //The max number of tiles in the queue at one time
        Game1 game;
        Queue<BackgroundTile> backgroundTiles = new Queue<BackgroundTile>(); //Using a queue allows for different backgrounds based on location, level, etc
        int highestLocation = 0;
        public float speed = 0.1f; //Speed of scrolling background (1 is default)
        public Background(Game1 game, BackgroundTileModel model)
        {
            this.game = game;
            //Set initial location for each tile
            for (int i = 0; i < MAXTILES; i++)
            {
                backgroundTiles.Enqueue(new BackgroundTile(game, model, i * -game.GraphicsDevice.Viewport.Height));
               if(i == MAXTILES - 1)
                {
                    highestLocation = i * -game.GraphicsDevice.Viewport.Height;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            BackgroundTile tempTile;

            foreach(BackgroundTile tile in backgroundTiles)
            {
                tile.Update(gameTime, speed);
            }

            //Move tiles from top of queue to bottom if offscreen
            if (backgroundTiles.Peek().Bounds.Y >= game.GraphicsDevice.Viewport.Height)
            {
                tempTile = backgroundTiles.Dequeue();
                tempTile.SetLocation(tempTile.Bounds.Y - (2 * game.GraphicsDevice.Viewport.Height));
                backgroundTiles.Enqueue(tempTile);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw top 2 tiles in stack
            for (int i = 0; i < 3; i++)
            {
                backgroundTiles.ElementAt(i).Draw(spriteBatch);
            }
        }
    }
}
