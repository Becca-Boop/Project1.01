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
    class NonFlyer : Enemy
    {
        public int X = 0;
        int div = 7;
        int dir = 0;
        int changernd = 0;



        public NonFlyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }



        public override Vector2 GetMove(float dt)
        {
            X = (int)Position.X;
            if (X < 0)
            {
                X = X * -1;
            }

            if (X < 1000)
            {
                X = X / 100;
            }
            else
            {
                X = X / 1000;
            }

            Random rnd = new Random();
            changernd = rnd.Next(1, 50);
            if (changernd == 1)
            {
                dir = rnd.Next(-1, 2);
            }

            int heightOverFloor = GetHeightOverFloor(Game);
            bool falling = heightOverFloor > 0;

            Vector2 moveDir = Position - Game.Player.Position;
            moveDir.Normalize();
            float distance = Vector2.Distance(Position, Game.Player.Position);

            // Horizontal movement
            int inc = (int)dt / div * dir;

            if (falling)
            {
                int yinc = (int)dt / 3;
                // Fall no further than floor
                if (yinc > heightOverFloor) yinc = heightOverFloor;
                return new Vector2(inc, yinc);
            }
            return new Vector2(inc, 0);
        }
    }
}
