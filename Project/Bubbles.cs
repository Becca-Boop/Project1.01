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
    public class Bubbles : Thing
    {
        //bool counted = false;


        public Bubbles(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }


        public override void Collision(Thing otherThing)
        {
            {                
                Game.Player.airstarttime = Game.Player.totalElapsed;                
            }
        }
    }
}