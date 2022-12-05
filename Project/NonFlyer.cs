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
        int div = 7;
        int dir = 0;

        public NonFlyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }



        public override Vector2 GetMove(float dt)
        {

            Random rnd = new Random();

            Vector2 moveDir = Position - Game.Player.Position;
            moveDir.Normalize();
            float distance = Vector2.Distance(Position, Game.Player.Position);

            if (distance < 5 * 64)
            {
                dir = moveDir.X < 0 ? 1 : -1;
            }
            else if (dir == 0 || rnd.Next(1, 50) == 0)
            {
                dir = rnd.Next(0, 100) < 50 ? 1 : -1;
            }

            // Horizontal movement
            float inc = dt / div * dir;

            float yinc = 0;
            int heightOverFloor = GetHeightOverFloor(Game);
            if (heightOverFloor > 0)
            {
                yinc = dt / 3;
                // Fall no further than floor
                if (yinc > heightOverFloor) yinc = heightOverFloor;
            }
            return new Vector2(inc, yinc);
        }

        public override void Bump()
        {
            dir = 0;
        }
    }
}
