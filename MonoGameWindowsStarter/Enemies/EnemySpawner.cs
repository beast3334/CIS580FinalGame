using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
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

        public void SpawnStrong()
        {
            pos = random.Next(50, 1700);
            var r = random.Next(0, 100);
            if (r > 0 && r <= 40)
            {
                Enemies.Add(new SwervingEnemy(game, Content, pos));
                state = State.Spawning;
            }
            else if (r > 40 && r <= 100)
            {
                Enemies.Add(new TrackingEnemy(game, Content, pos));
            }
        }
         public void SpawnWeak()
        {
            pos = random.Next(50, 1700);
            var r = random.Next(0, 100);
            if (r >= 0 && r <= 40)
            {
                Enemies.Add(new BasicEnemy(game, Content, pos));
            }
            else if (r > 40 && r <= 70)
            {
                Enemies.Add(new BasicShootingEnemy(game, Content, pos));
            }
            else if (r > 70 && r <= 100)
            {
                Enemies.Add(new SwervingEnemy(game, Content, pos));
            }
            
        }

        public void SpawnRandom()
        {
            if (timer >= 2 && state == State.Idle)
            {
                pos = random.Next(50, 1700);
                var r = random.Next(0, 100);
                if (r >= 0 && r <= 30)
                {
                    Enemies.Add(new BasicEnemy(game, Content, pos));
                }
                else if (r > 30 && r <= 60)
                {
                    Enemies.Add(new BasicShootingEnemy(game, Content, pos));
                }
                else if (r > 60 && r <= 80)
                {
                    Enemies.Add(new SwervingEnemy(game, Content, pos));
                }
                else if (r > 80 && r <= 85)
                {
                    Enemies.Add(new SwervingEnemy(game, Content, pos));
                    state = State.Spawning;
                }
                else if (r > 85 && r <= 95)
                {
                    Enemies.Add(new TrackingEnemy(game, Content, pos));
                }
                
                timer = 0;
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;


            if (state == State.Spawning)
            {
                timer2 += gameTime.ElapsedGameTime.TotalSeconds;
                if (timer2 >= .5 && counter < 5)
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

            if (Enemies.Count > 0)
            {
                //update all enemies
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].Update(gameTime);
                }
                //remove dead enemies;
                for (int i = 0; i < Enemies.Count(); i++)
                {
                    if (Enemies[i].ReadyForTrash == true)
                    {
                        var x = Enemies[i].Bounds.X;
                        var y = Enemies[i].Bounds.Y;
                        if (Enemies[i].Bounds.Y < game.GraphicsDevice.Viewport.Height)
                        {
                            
                            game.Score += Enemies[i].points;
                        }
                        Enemies.RemoveAt(i);
                        i--;
                        
                    }
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

        public void KillAll()
        {
            game.particleSystem.SpawnPerFrame = 30;
            game.particleSystem.SpawnParticle = (ref Particle particle) =>
            {
                
                particle.Position = new Vector2(game.GraphicsDevice.Viewport.Width/2, game.GraphicsDevice.Viewport.Height/2);
                particle.Velocity = new Vector2(
                    MathHelper.Lerp(-500, 500, (float)random.NextDouble()), 
                    MathHelper.Lerp(-500, 500, (float)random.NextDouble()) 
                    );
                particle.Acceleration = 2f * new Vector2(0, (float)-random.NextDouble());
                particle.Color = Color.GreenYellow;
                particle.Scale = 3f;
                particle.Life = 3f;
            };

            // Adds the score from each enemy
            // Deletes the enemy and boss
            for (int i = 0; i < Enemies.Count; i++)
            {
                game.Score += Enemies[i].points;
                Enemies.RemoveAt(i);
                i--;
            }
        }


    }
}