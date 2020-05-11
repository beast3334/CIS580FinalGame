using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonoGameWindowsStarter.Powerups
{
    public enum PowerupSpriteCategory
    {
        SpawnStrongPowerup,
        SpawnWeakPowerup
    }

    public enum PowerupsType
    {
        ThreeSixtyShot,
        ExplodingShot,
        Heart_Powerup,
        Laser_Powerup,
        Nuke_Powerup,
        PenetrationShot,
        Speed_Powerup,
        TripleSplitShot,
        Trishot_Powerup
    }

    public class PowerupSpawner
    {
        public List<PowerupSprite> PowerupSprites { get; private set; } = new List<PowerupSprite>();

        private Game _game;
        private List<Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>> _textures = new List<Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>>();
        public PowerupSpawner(Game game)
        {
            _game = game;
        }

        public void LoadContent(ContentManager Content)
        {
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.ThreeSixtyShot, Content.Load<Texture2D>("Powerups/360shot")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.ExplodingShot, Content.Load<Texture2D>("Powerups/ExplodingShot")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.Heart_Powerup, Content.Load<Texture2D>("Powerups/Heart_Powerup")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnStrongPowerup, PowerupsType.Laser_Powerup, Content.Load<Texture2D>("Powerups/Laser_Powerup")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnStrongPowerup, PowerupsType.Nuke_Powerup, Content.Load<Texture2D>("Powerups/Nuke_Powerup")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.PenetrationShot, Content.Load<Texture2D>("Powerups/PenetrationShot")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.Speed_Powerup, Content.Load<Texture2D>("Powerups/Speed_Powerup")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.TripleSplitShot, Content.Load<Texture2D>("Powerups/TripleSplitShot")));
            _textures.Add(new Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>(PowerupSpriteCategory.SpawnWeakPowerup, PowerupsType.Trishot_Powerup, Content.Load<Texture2D>("Powerups/Trishot_Powerup")));
        }

        public void Update()
        {
            for (int i = 0; i < PowerupSprites.Count; i++)
            {
                var sprite = PowerupSprites[i];
                sprite.Position += sprite.Velocity;

                if (!sprite.Alive || sprite.Position.Y > _game.GraphicsDevice.Viewport.Height)
                {
                    PowerupSprites.Remove(sprite);
                    i--;
                }
            }
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            PowerupSprites.ForEach(sprite =>
            {
                SpriteBatch.Draw(
                    sprite.Powerup.Item3,
                    new BoundingRectangle(sprite.Position.X, sprite.Position.Y, sprite.Powerup.Item3.Bounds.Width * sprite.Scale, sprite.Powerup.Item3.Bounds.Height * sprite.Scale),
                    Color.White
                );
            });
        }

        /// <summary>
        /// Spawns a random powerup from the given category
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public PowerupSprite SpawnRandom(PowerupSpriteCategory Category)
        {
            var random = new Random();
            var powerup = GetPowerup(Category);
            var scale = 0.05f + random.Next(4) / 1000f;
            var position = new Vector2(random.Next(_game.GraphicsDevice.Viewport.Width), -(powerup.Item3.Height * scale));
            var velocity = new Vector2(0f, 2 + random.Next(2));
            var powerupSprite = new PowerupSprite(powerup, position, velocity, scale);
            PowerupSprites.Add(powerupSprite);
            return powerupSprite;
        }

        private Tuple<PowerupSpriteCategory, PowerupsType, Texture2D> GetPowerup(PowerupSpriteCategory category)
        {
            var workingTextures = new List<Tuple<PowerupSpriteCategory, PowerupsType, Texture2D>>();
            
            // filters the working set of textures to the specified Category
            workingTextures = _textures.FindAll(tuple => 
            {
                return tuple.Item1 == category;
            });

            // returns a random texture from the list of textures
            return workingTextures[new Random().Next(workingTextures.Count)];
        }
    }

    public class PowerupSprite
    {
        /// <summary>
        /// Texture of the sprite
        /// </summary>
        public Tuple<PowerupSpriteCategory, PowerupsType, Texture2D> Powerup { get; set; }

        /// <summary>
        /// Speed and dirction of the sprite
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// X, Y coordinates of the sprite
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Size of the sprite
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// If the powerup is still on the screen or not
        /// </summary>
        public bool Alive { get; set; } = true;

        /// <summary>
        /// Sprite that displays Powerups
        /// </summary>
        /// <param name="Powerup">PlayerPowerupSpriteCategory, PowerupsType, and Texture2D of the sprite</param>
        /// <param name="Position">X, Y coordinates of the sprite</param>
        /// <param name="Velocity">Speed and dirction of the sprite</param>
        /// <param name="Scale">Size of the sprite</param>
        public PowerupSprite(Tuple<PowerupSpriteCategory, PowerupsType, Texture2D> Powerup, Vector2 Position, Vector2 Velocity, float Scale)
        {
            this.Powerup = Powerup;
            this.Position = Position;
            this.Velocity = Velocity;
            this.Scale = Scale;
        }
    }
}
