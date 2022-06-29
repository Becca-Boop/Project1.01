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
        bool counted = false;
        int state = 1;
        bool Xnewdirection = true;
        bool Ynewdirection = true;
        int X = 0;
        int dirx = 0;
        Thing Collider;
        int div = 7;
        public double totalElapsed;
        bool changedir = true;
        int dir = 0;





        public NonFlyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Game.paused)
            {
                int heightOverFloor = GetHeightOverFloor(Game);
                bool falling = heightOverFloor > 0;
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                totalElapsed += elapsed;
                long delay = (long)totalElapsed / 80;


                float dt = (float)Game.Player.totalElapsed;

                Vector2 moveDir = Position - Game.Player.Position;
                moveDir.Normalize();
                float distance = Vector2.Distance(Position, Game.Player.Position);
                if (state == 0)
                {
                    //if (distance < 600 && distance > 10)
                    //{
                    //    state = 1;
                    //}
                    //else
                    //{
                        Random rnd = new Random();
                    if (changedir)
                    {
                        dir = rnd.Next(-1, 2);
                    }

                    int change = rnd.Next(1, 10);
                    if (change == 1)
                    {
                        changedir = true;
                    }
                    else
                    {
                        changedir = false;
                    }

                        if (dir != 0)
                        {

                            if (!Game.paused && !Game.dead && !Game.Win)
                            {

                                // Horizontal movement
                                int inc = (int)elapsed / div * dir;
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
                                    delay = (long)totalElapsed / 80;
                                }


                                //if (Xnewdirection)
                                //{
                                //    dirx = rnd.Next(-1, 2);
                                //}
                                //int Xlength = rnd.Next(1, 24);


                                //if (X < Xlength)
                                //{
                                //    // Horizontal movement
                                //    int inc = 4 * dirx;
                                //    Position.X += inc;
                                //    X++;
                                //}
                                //int XDirChange = rnd.Next(1, 10);
                                //if (XDirChange == 1)
                                //{
                                //    Xnewdirection = true;
                                //    X = 0;
                                //}
                                //else
                                //{
                                //    Xnewdirection = false;
                                //}
                            }
                        }
                    //}
                }
                else if (state == 1)
                {
                    //Position -= moveDir * 3;
                    state = 0;
                }

                if (falling)
                {
                    int inc = (int)elapsed / 3;
                    // Fall no further than floor
                    if (inc > heightOverFloor) inc = heightOverFloor;
                    BigBoundingBox.Y += inc;
                    Position.Y += inc;
                }
            }
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }
    }
}
