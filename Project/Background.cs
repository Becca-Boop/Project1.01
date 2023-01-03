using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Project
{
    public class Background : Thing
    {
        protected Texture2D texture;

        public Background(Game game, Texture2D _texture, Vector2 _position) : base(game, _texture, new Rectangle(0, 0, 2048, 1500))
        {
            collision = false;
            Position = _position;
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - (Game.Offset / 2), LittleBoundingBox, Color.White);
        }

        public Thing IsColliding(Game Game)
        {
            return null;
        }

    }
}
