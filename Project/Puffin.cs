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
    public class Puffin : Thing
    {
        bool counted = false;
        bool Xnewdirection= true;
        bool Ynewdirection = true;
        int X = 0;
        int Y = 0;
        int dirx = 0;
        int diry = 0;
        Thing Collider;

        public Puffin(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }




        public override void Collision(Thing otherThing)
        {
            {
                if (!counted)
                {
                    if (Game.Player.IsSliding)
                    {
                        Game.DeadThings.Add(this);
                    }
                    else
                    {
                        Game.Player.health--;
                    }
                    counted = true;
                }
                
            }
        }
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Random rnd = new Random();

            if (!Game.paused && !Game.dead && !Game.Win)
            {
                if (Xnewdirection)
                {
                    dirx = rnd.Next(-1, 2);
                }
                int Xlength = rnd.Next(1, 24);


                if (X < Xlength)
                {
                    // Horizontal movement
                    int inc = 4 * dirx;
                    // Dip a toe in
                    BigBoundingBox.X += inc;

                    Collider = this.IsColliding(Game);
                    if (Collider is Block)
                    {
                        // Hit something, so undo the change and stop
                        BigBoundingBox.X -= inc;
                    }
                    else
                    {
                        Position.X += inc;
                    }
                    X++;

                    if (Collider is Player)
                    {
                    }
                    else
                    {
                        counted = false;
                    }
                }
                int XDirChange = rnd.Next(1, 10);
                if (XDirChange == 1)
                {
                    Xnewdirection = true;
                    X = 0;
                }
                else
                {
                    Xnewdirection = false;
                }



                if (Ynewdirection)
                {
                    diry = rnd.Next(-1, 2);
                }
                int Ylength = rnd.Next(1, 24);

                if (Y < Ylength)
                {
                    // Vertical movement
                    int inc = 4 * diry;
                    // Dip a toe in
                    BigBoundingBox.Y += inc;

                    Collider = this.IsColliding(Game);
                    if (Collider is Block)
                    {
                        // Hit something, so undo the change and stop
                        BigBoundingBox.Y -= inc;
                    }
                    else
                    {
                        Position.Y += inc;
                    }
                    Y++;
                }
                int YDirChange = rnd.Next(1, 10);
                if (YDirChange == 1)
                {
                    Ynewdirection = true;
                    Y = 0;
                }
                else
                {
                    Ynewdirection = false;
                }
                
            }

            double range = IsInRange(this);
            if (range <= 200)
            {
                dirx = 0;
                diry = 0;
            }

            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);

        }
    }
}