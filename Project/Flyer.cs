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
        bool counted = false;
        int state = 1;
        bool Xnewdirection = true;
        bool Ynewdirection = true;
        int X = 0;
        int Y = 0;
        int dirx = 0;
        int diry = 0;
        Thing Collider;

        public Flyer(Game game, Texture2D _texture, Vector2 _position, Rectangle _boundingBox) : base(game, _texture, _position, _boundingBox)
        {
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Game.paused)
            {
                float dt = (float)Game.Player.totalElapsed;

                Vector2 moveDir = Position - Game.Player.Position;
                moveDir.Normalize();
                float distance = Vector2.Distance(Position, Game.Player.Position);
                if (state == 0)
                {
                    if (distance < 600 && distance > 10)
                    {
                        state = 1;
                    }
                    else
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
                                Position.X += inc;
                                X++;
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
                                Position.Y += inc;
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

                        
                    }
                }
                else if (state == 1)
                {
                    Position -= moveDir * 3;
                    state = 0;
                }
            }
            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }
    }
}
