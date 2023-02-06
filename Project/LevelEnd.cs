using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    class LevelEnd : Thing
    {

        public LevelEnd(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, new Rectangle(0, 0, 40, 40))
        {
            Position = _position;
            BigBoundingBox = new Rectangle(LittleBoundingBox.X + (int)Position.X, LittleBoundingBox.Y + (int)Position.Y, LittleBoundingBox.Width, LittleBoundingBox.Height);
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }


        public override void Collision(Thing otherThing)
        {
            if (!Game.Player.levelEnded)
            {
                if (Game.level == 5)
                {
                    Game.Win = true;
                }
                else
                {
                    Console.WriteLine("Ending...");
                    Game.level++;
                    Game.restart = true;
                }
                Game.Player.levelEnded = true;
            }
        }
    }
}
