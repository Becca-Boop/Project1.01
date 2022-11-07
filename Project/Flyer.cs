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
    class Flyer : Enemy
    {
        public Flyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
            ignoreBlocks = true;
        }



        public override Vector2 GetMove(float dt)
        {
            Vector2 moveDir = Position - Game.Player.Position;


            moveDir.Normalize();
            float distance = Vector2.Distance(Position, Game.Player.Position);

            Vector2 moveVector = new Vector2(-moveDir.X * dt / 10, -moveDir.Y * dt / 10);
            return moveVector;
        }
    }
}
