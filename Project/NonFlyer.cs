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
        public NonFlyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }
    }
}
