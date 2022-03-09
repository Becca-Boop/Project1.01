using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Star : Thing
    {
        Game Game;

        public Star(Texture2D _texture, Vector2 _position, Rectangle _boundingBox, Game _game) : base(_texture, _position, _boundingBox)
        {
            Game = _game;
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(Texture, Position, LittleBoundingBox, Color.White);
        }
        
    }
}
