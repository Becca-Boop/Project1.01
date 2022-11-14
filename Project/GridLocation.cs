using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Xna.Framework.Content;

namespace Project
{
    public class GridLocation
    {
        public Vector2 Position;
        protected Texture2D Texture;
        public Rectangle LittleBoundingBox;
        public Rectangle BigBoundingBox;
        protected Rectangle sourceRect;
        public Game Game;
        public bool collision;

        public GridLocation(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox)
        {
            Game = game;
            Texture = _texture;
            Position = _position;
            LittleBoundingBox = _boundingBox;
            BigBoundingBox = new Rectangle(LittleBoundingBox.X + (int)Position.X, LittleBoundingBox.Y + (int)Position.Y, LittleBoundingBox.Width, LittleBoundingBox.Height);
            collision = true;
        }
    }
}
