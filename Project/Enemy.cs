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
    public class Enemy : Thing
    {
        bool counted = false;
        int state = 1;
        bool Xnewdirection = true;
        bool Ynewdirection = true;
        int X = 0;
        int Y = 0;
        int dirx = 0;
        int diry = 0;
        Thing Collider;

        public Enemy(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }

        public override void Collision(Thing otherThing)
        {
            {
                //Game.Player.health--;



                //if (!counted)
                //{
                //    if (Game.Player.IsSliding)
                //    {
                //        Game.DeadThings.Add(this);
                //    }
                //    else
                //    {
                //        Game.Player.health--;
                //    }
                //    counted = true;
                //}

            }
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }
    }
}