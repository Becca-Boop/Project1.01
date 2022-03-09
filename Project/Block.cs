using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;


namespace Project
{
    public class Block : Thing
    {

        protected Texture2D texture;
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            protected set
            {
                rectangle = value;
            }
        }
        public Block(Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(_texture, _position, _boundingBox)
        {
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, LittleBoundingBox, Color.White);
        }

        private static ContentManager content;
        public static ContentManager Content
        {
            protected get
            {
                return content;
            }
            set
            {
                content = value;
            }
        }
    }
}







