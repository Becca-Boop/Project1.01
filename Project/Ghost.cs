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
    class Ghost : Flyer
    {
        public Ghost(Game game, Texture2D _texture) : base(game, _texture)
        {
            ignoreBlocks = true;
        }
    }
}
