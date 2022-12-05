using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Project
{
    class PathStep
    {
        public PathStep previous;
        public Vector2 position;

        public PathStep(PathStep previous, Vector2 position)
        {
            this.previous = previous;
            this.position = position;
        }
    }
}
