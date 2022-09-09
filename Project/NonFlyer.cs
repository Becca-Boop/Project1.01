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
        public int X = 0;
        int dirx = 0;
        Thing Collider;
        int div = 7;
        public double totalElapsed;
        bool changedir = true;
        int dir = 0;
        int changernd = 0;
        SpriteEffects s = SpriteEffects.None;







        public NonFlyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Game.paused)
            {
                if (dir == 1)
                {
                    s = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    s = SpriteEffects.None;
                }

                X = (int)Position.X ;
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

                if (X == 1)
                {
                    Random rnd1 = new Random();
                    changernd = rnd1.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd1.Next(-1, 2);
                    }

                }
                else if (X == 2)
                {
                    Random rnd2 = new Random();
                    changernd = rnd2.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd2.Next(-1, 2);
                    }
                }
                else if (X == 3)
                {
                    Random rnd3 = new Random();
                    changernd = rnd3.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd3.Next(-1, 2);
                    }

                }
                else if (X == 4)
                {
                    Random rnd4 = new Random();
                    changernd = rnd4.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd4.Next(-1, 2);
                    }
                }
                else if (X == 5)
                {
                    Random rnd5 = new Random();
                    changernd = rnd5.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd5.Next(-1, 2);
                    }
                }
                else if (X == 6)
                {
                    Random rnd6 = new Random();
                    changernd = rnd6.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd6.Next(-1, 2);
                    }
                }
                else if (X == 7)
                {
                    Random rnd7 = new Random();
                    changernd = rnd7.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd7.Next(-1, 2);
                    }
                }
                else if (X == 8)
                {
                    Random rnd8 = new Random();
                    changernd = rnd8.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd8.Next(-1, 2);
                    }
                }
                else if (X == 9)
                {
                    Random rnd9 = new Random();
                    changernd = rnd9.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd9.Next(-1, 2);
                    }
                }
                else if (X == 10)
                {
                    Random rnd10 = new Random();
                    changernd = rnd10.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd10.Next(-1, 2);
                    }
                }
                else if (X == 11)
                {
                    Random rnd11 = new Random();
                    changernd = rnd11.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd11.Next(-1, 2);
                    }
                }
                else if (X == 12)
                {
                    Random rnd12 = new Random();
                    changernd = rnd12.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd12.Next(-1, 2);
                    }
                }
                else if (X == 13)
                {
                    Random rnd13 = new Random();
                    changernd = rnd13.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd13.Next(-1, 2);
                    }
                }
                else if (X == 14)
                {
                    Random rnd14 = new Random();
                    changernd = rnd14.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd14.Next(-1, 2);
                    }
                }
                else if (X == 15)
                {
                    Random rnd15 = new Random();
                    changernd = rnd15.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd15.Next(-1, 2);
                    }
                }
                else
                {
                    Random rnd16 = new Random();
                    changernd = rnd16.Next(1, 50);
                    if (changernd == 1)
                    {
                        dir = rnd16.Next(-1, 2);
                    }
                }
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
                    //if (changedir)
                    //{
                    //    dir = rnd.Next(-1, 2);
                    //}

                    //if (changernd == 1)
                    //{
                    //    changedir = true;
                    //}
                    //else
                    //{
                    //    changedir = false;
                    //}

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
                                if (Collider is Player)
                                {
                                    Game.Player.health--;
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
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White, 0f, Vector2.Zero, Vector2.One, s, 0f);
        }
    }
}
