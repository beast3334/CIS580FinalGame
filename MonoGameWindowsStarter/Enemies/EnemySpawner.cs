using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameWindowsStarter.Powerups.Bullets.Powerups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameWindowsStarter.Enemies
{
    class EnemySpawner
    {
        enum State
        {
            Idle,
            Spawning,
            Disabled
        }
        public List<Enemy> Enemies { get; private set; }
        ContentManager Content;
        double timer = 0;
        double timer2 = 0;
        int counter = 0;
        int pos;
        Game1 game;
        Random random = new Random();
        State state = State.Idle;


        public EnemySpawner(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(ContentManager content)
        {
            Enemies = new List<Enemy>();
            Content = content;
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            
            if (timer >= 2 && state == State.Idle)
            {
                pos = random.Next(50, 900);
                var r = random.Next(0, 4);
                switch (r)
                {
                    case 0:
                        Enemies.Add(new BasicEnemy(game, Content, pos));
                        break;
                    case 1:
                        Enemies.Add(new BasicShootingEnemy(game, Content, pos));
                        break;
                    case 2:
                        Enemies.Add(new SwervingEnemy(game, Content, pos));
                        break;
                    case 3:
                        Enemies.Add(new SwervingEnemy(game, Content, pos));
                        state = State.Spawning;
                        
                        break;
                        
                } 
                //Enemies.Add(new BasicShootingEnemy(game, Content));
                timer = 0;
            }
            else if(state == State.Spawning)
            {
                timer2 += gameTime.ElapsedGameTime.TotalSeconds;
                if(timer2 >= .5 && counter <5)
                {
                    Enemies.Add(new SwervingEnemy(game, Content, pos));
                    timer2 = 0;
                    counter++;
                }
                else if (counter >= 5)
                {
                    counter = 0;
                    state = State.Idle;
                }
            }
            

            //update all enemies
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Update(gameTime);
            }
            //remove dead enemies;
            for (int i = 0; i < Enemies.Count; i++)
            {
                if (Enemies[i].ReadyForTrash == true)
                {
                    Enemies.Remove(Enemies[i]);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].Draw(spriteBatch);
            }
        }

        
    }
}
