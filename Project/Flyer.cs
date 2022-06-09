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
        //bool counted = false;
        //bool Xnewdirection = true;
        //bool Ynewdirection = true;
        //int X = 0;
        //int Y = 0;
        //int dirx = 0;
        //int diry = 0;
        //Thing Collider;

        public Flyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }
        //public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        //{            
        //    Random rnd = new Random();

        //    if (!Game.paused && !Game.dead && !Game.Win && getstate == 0)
        //    {
        //        if (Xnewdirection)
        //        {
        //            dirx = rnd.Next(-1, 2);
        //        }
        //        int Xlength = rnd.Next(1, 24);


        //        if (X < Xlength)
        //        {
        //            // Horizontal movement
        //            int inc = 4 * dirx; 
        //            Position.X += inc;                  
        //            X++;                    
        //        }
        //        int XDirChange = rnd.Next(1, 10);
        //        if (XDirChange == 1)
        //        {
        //            Xnewdirection = true;
        //            X = 0;
        //        }
        //        else
        //        {
        //            Xnewdirection = false;
        //        }



        //        if (Ynewdirection)
        //        {
        //            diry = rnd.Next(-1, 2);
        //        }
        //        int Ylength = rnd.Next(1, 24);

        //        if (Y < Ylength)
        //        {
        //            // Vertical movement
        //            int inc = 4 * diry; 
        //            Position.Y += inc;
        //            Y++;
        //        }
        //        int YDirChange = rnd.Next(1, 10);
        //        if (YDirChange == 1)
        //        {
        //            Ynewdirection = true;
        //            Y = 0;
        //        }
        //        else
        //        {
        //            Ynewdirection = false;
        //        }

        //    }

        //    ////double range = IsInRange(this);
        //    //float distance = Vector2.Distance(Position, Game.Player.Position);
        //    //if (distance < 600)
        //    //{
        //    //    float dt = (float)Game.Player.totalElapsed;

        //    //    Vector2 moveDir = Game.Player.Position - Position;
        //    //    moveDir.Normalize();
        //    //}

        //    spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);

        //}
    }
}
