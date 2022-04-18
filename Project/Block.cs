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
        public Block(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }


        public override int GetHeightOver(Thing otherThing)
        {
            /*    // Is this under the other? If not, return -1
                if (BigBoundingBox.Left > otherThing.BigBoundingBox.Right - 5)
                    return 9999;
                if (BigBoundingBox.Right < otherThing.BigBoundingBox.Left + 5)
                    return 9999;
                // +5/-5 to stop sticking to walls when falling*/

            // Is this under the other? If not, return -1
            if (BigBoundingBox.Left > otherThing.BigBoundingBox.Right)
                return 9999;
            if (BigBoundingBox.Right < otherThing.BigBoundingBox.Left)
                return 9999;
            // +5/-5 to stop sticking to walls when falling

            // Is this over (or overlapping) the other? If so, return -1
            if (BigBoundingBox.Bottom <= otherThing.BigBoundingBox.Top - 5)
                return 9999;
            if (BigBoundingBox.Top >= otherThing.BigBoundingBox.Bottom)
                return BigBoundingBox.Top - otherThing.BigBoundingBox.Bottom;

            return 9999;
        }
    }
}












