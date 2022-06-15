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
        int state = 0;

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
            float dt = (float)Game.Player.totalElapsed;

            Vector2 moveDir = Position - Game.Player.Position;
            moveDir.Normalize();

            float distance = Vector2.Distance(Position, Game.Player.Position);
            if (state == 0)
            {
                if (distance < 600)
                {
                    state = 1;
                }
                //else
                //{
                //    state = 0;
                //}
            }
            else if (state == 1)
            {
                Position -= moveDir * 3;
                state = 0;
            }

            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);


        }
        public int getstate
        {
            get { return state;}
        }
    }
}