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
    public class Thing
    {
        public Vector2 Position;
        protected Texture2D Texture;
        public Rectangle LittleBoundingBox;
        public Rectangle BigBoundingBox;
        protected Rectangle sourceRect;
        public Game Game;
        public bool collision;

        public Thing(Game game, Texture2D _texture, Rectangle _boundingBox)
        {
            Game = game;
            Texture = _texture;
            LittleBoundingBox = _boundingBox;
            BigBoundingBox = new Rectangle(LittleBoundingBox.X + (int)Position.X, LittleBoundingBox.Y + (int)Position.Y, LittleBoundingBox.Width, LittleBoundingBox.Height);
            collision = true;
        }


        public Thing IsColliding(Game Game)
        {
            foreach (var Thing in Game.Things)
            {
                if (this.IsColliding(Thing)) //if colliding with an object, recieve the object its colliding with
                {
                    return Thing;
                }
            }
            return null;
        }



        public bool IsColliding(Thing otherThing)
        {
            if (otherThing == this || !otherThing.collision)
            {
                return false;
            }
            else
            {
                return BigBoundingBox.Intersects(otherThing.BigBoundingBox);
            }
        }

        public virtual void Collision(Thing otherThing)
        {
        }



        public virtual int GetHeightOver(Thing Thing)
        {
            return 9999;
        }

        public virtual double IsInRange(Thing otherThing)
        {
            return 9999;
        }

        public int GetHeightOverFloor(Game game)
        {
            int result = 9999;
            foreach (var Thing in Game.Things)
            {
                int height = Thing.GetHeightOver(this);
                if (height < result)
                {
                    result = height;
                }
            }

            return result;
        }

        public virtual void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
        public Vector2 position
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
            }
        }

        public Vector2 GetNode()
        {
            // get centre, divide by 64 to get grid place
            int x = (int)Math.Floor((this.position.X + this.LittleBoundingBox.Width / 2) / 64);
            int y = (int)Math.Floor((this.position.Y + this.LittleBoundingBox.Height / 2) / 64);
            //int x = (int)Math.Round(this.position.X / 64, MidpointRounding.AwayFromZero);
            //int y = (int)Math.Round(this.position.Y / 64, MidpointRounding.AwayFromZero);
            return new Vector2(x, y);
        }

        public Vector2 GetNodeCoords()
        {
            Vector2 v = this.GetNode();
            return new Vector2(v.X * 64, v.Y * 64);
            //return new Vector2(v.X * 64 - this.LittleBoundingBox.Width / 2, v.Y * 64 - this.LittleBoundingBox.Height / 2);
        }


        public bool Centred()
        {
            Vector2 v = this.GetNodeCoords();
            return Math.Abs(v.X - this.Position.X) < 0.1 && Math.Abs(v.Y - this.Position.Y) < 0.1;
        }



    }
}
