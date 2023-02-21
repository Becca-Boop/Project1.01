using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Project
{
    public class Player : Thing
    {
        public int health;
        int frames = 0;
        public double totalElapsed;
        double jumpStartTime = 0;
        public double airtime = 0;
        double airtimepercent;
        public double airstarttime = 0;
        bool isjumping = false;
        bool slidejump = false;
        public bool IsSliding = false;
        const int gravity = 2;
        Vector2 velocity = Vector2.Zero;
        bool pausing = false;
        bool unpausing = false;
        public bool Controller;
        SpriteFont font;
        public int score;
        Vector2 Length = new Vector2(0, 0);
        int count = 0;
        Texture2D Oxytexture;
        bool attack = false;
        Texture2D SnowballSprite;
        private float ShotTimer;
        public float hittime = 0;
        public bool levelEnded = false;





        Thing Collider;
        Vector2 CENTRE = new Vector2(Game.WIDTH / 2 - 14, Game.HEIGHT / 2 - 20);
        string debug;


        public Player(Game game, Texture2D _texture, Vector2 _position, SpriteFont _font, Texture2D _snowball) : base(game, _texture, new Rectangle(2, 2, 35, 48))
        {
            health = 4;
            Game.paused = false;
            //Game.restart = true;
            score = 0;
            font = _font;
            SnowballSprite = _snowball;
            Position = _position;
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {



            BigBoundingBox = new Rectangle(LittleBoundingBox.X + (int)Position.X, LittleBoundingBox.Y + (int)Position.Y, LittleBoundingBox.Width, LittleBoundingBox.Height);




            if (!Game.Menu && !Game.paused && !Game.dead && !Game.Win && Game.level == 4)
            {
                // Handle timing issues
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                totalElapsed += elapsed;
                long delay = (long)totalElapsed / 80;
                debug = "";


                airtime = totalElapsed - airstarttime;
                if (airtime >= 10000)
                {
                    health--;
                }
                airtimepercent = airtime / 1000;
                Math.Round(airtimepercent, 0);
                airtimepercent = (int)airtimepercent;
                airtimepercent = 5 - (int)airtime / 2000;
                //debug += airtimepercent;
                Oxytexture = Game.Oxygen[(int)airtimepercent];
                spriteBatch.Draw(Oxytexture, new Vector2(550, 700), Color.White);

                spriteBatch.Draw(Oxytexture, new Vector2(550, 700), Color.White);


                int heightOverFloor = GetHeightOverFloor(Game);
                frames = 6;

                // What does the player want to do?
                int dir = 0;
                Vector2 leftthumbstick = GamePad.GetState(0).ThumbSticks.Left;
                attack = false;


                if (Keyboard.GetState().IsKeyDown(Keys.A) || leftthumbstick.X < 0) dir--;
                if (Keyboard.GetState().IsKeyDown(Keys.D) || leftthumbstick.X > 0) dir++;
                bool jump = ((Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && (jumpStartTime + 500) < totalElapsed);
                bool falling = !isjumping && heightOverFloor > 0;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftControl) || GamePad.GetState(0).IsButtonDown(Buttons.X)) attack = true;


                if (jump)
                {
                    isjumping = true;
                    jumpStartTime = totalElapsed;
                }
                else if (isjumping && jumpStartTime + 200 < totalElapsed)
                {
                    isjumping = false;
                }


                // Determine horizontal speed modifier
                int div = 7;



                // Vertical movement (ignores frames)
                if (falling)
                {
                    int inc = (int)elapsed / 10;
                    // Fall no further than floor
                    if (inc > heightOverFloor) inc = heightOverFloor;
                    BigBoundingBox.Y += inc;
                    Position.Y += inc;
                }
                if (isjumping)
                {
                    double jumpTime = totalElapsed - jumpStartTime;
                    int inc = (int)(elapsed * 30 / (jumpTime + 10));
                    BigBoundingBox.Y -= inc;
                    Position.Y -= inc;
                }

                if (dir != 0)
                {
                    //debug += " dir=" + dir;
                    // Select the frame
                    frames = dir == -1 ? 0 : 12;

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
                }


                if (Collider == null)
                {
                    // Only needed if fishes move
                    Collider = this.IsColliding(Game);
                }

                if (Collider != null)
                {
                    Collider.Collision(this);
                }

                if (Collider is Flyer)
                {
                    health--;
                }


                // Now draw it
                sourceRect = new Rectangle(42 * frames, 0, 42, 60);
            }
            else if (!Game.Menu && !Game.paused && !Game.dead && !Game.Win)
            {
                // Handle timing issues
                float elapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                totalElapsed += elapsed;
                long delay = (long)totalElapsed / 80;
                debug = "";

                int heightOverFloor = GetHeightOverFloor(Game);
                frames = 6;

                // What does the player want to do?
                int dir = 0;
                Vector2 leftthumbstick = GamePad.GetState(0).ThumbSticks.Left;


                if (Keyboard.GetState().IsKeyDown(Keys.A) || leftthumbstick.X < 0) dir--;
                if (Keyboard.GetState().IsKeyDown(Keys.D) || leftthumbstick.X > 0) dir++;
                bool jump = (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(0).IsButtonDown(Buttons.A)) && heightOverFloor == 0;
                bool falling = !isjumping && heightOverFloor > 0;
                slidejump = ((Keyboard.GetState().IsKeyDown(Keys.LeftShift) || GamePad.GetState(0).IsButtonDown(Buttons.LeftTrigger)) && Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(0).IsButtonDown(Buttons.A) && heightOverFloor == 0) || slidejump && heightOverFloor != 0;
                bool sliding = (Keyboard.GetState().IsKeyDown(Keys.LeftShift) || GamePad.GetState(0).IsButtonDown(Buttons.LeftTrigger)) && heightOverFloor == 0;


                if (jump)
                {
                    isjumping = true;
                    jumpStartTime = totalElapsed;
                }
                else if (isjumping && jumpStartTime + 700 < totalElapsed)
                {
                    isjumping = false;
                }


                // Determine horizontal speed modifier
                int div = 7;

                if (slidejump)
                {

                    float increase = (float)(count * 0.009);

                    div = 3 + (int)increase;
                    count++;
                }
                else if (sliding)
                {
                    IsSliding = true;
                    float increase = (float)(count * 0.009);

                    div = 4 + (int)increase;
                    count++;
                }
                else
                {
                    IsSliding = false;
                    count = 0;
                }



                // Vertical movement (ignores frames)
                if (falling)
                {
                    int inc = (int)elapsed / 3;
                    // Fall no further than floor
                    if (inc > heightOverFloor) inc = heightOverFloor;
                    BigBoundingBox.Y += inc;
                    Position.Y += inc;
                }
                if (isjumping)
                {
                    double jumpTime = totalElapsed - jumpStartTime;
                    int inc = (int)(elapsed * 30 / (jumpTime + 10));
                    BigBoundingBox.Y -= inc;
                    Position.Y -= inc;
                }

                if (dir != 0)
                {
                    //debug += " dir=" + dir;
                    // Select the frame
                    int frameStart = dir == -1 ? 3 : 7;

                    int frameCount = 3;
                    if (sliding || slidejump)
                    {
                        frameStart = dir == -1 ? 0 : 12;
                        frameCount = 1;
                    }
                    frames = (isjumping || falling) ? frameStart : ((int)delay % frameCount) + frameStart;


                    // Horizontal movement
                    int inc = (int)elapsed / div * dir;
                    // Dip a toe in
                    BigBoundingBox.X += inc;
                    Collider = this.IsColliding(Game);
                    //if (Collider != null) Console.WriteLine("Collided with " + Collider.GetType());


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
                }

                if (Collider == null)
                {
                    // Only needed if fishes move
                    Collider = this.IsColliding(Game);
                }

                if (Collider != null)
                {
                    Collider.Collision(this);
                    debug += Collider;
                }

                if (Collider != null) debug += " hit=" + Collider.GetType();

                // Now draw it
                sourceRect = new Rectangle(42 * frames, 0, 42, 60);
            }
            Game.Offset = this.Position - CENTRE;
            spriteBatch.Draw(Texture, CENTRE, sourceRect, Color.White);
            spriteBatch.DrawString(font, "SCORE:  " + score, new Vector2(100, 700), Color.White);
            spriteBatch.DrawString(font, "HEALTH:  " + health, new Vector2(1200, 700), Color.White);
            //spriteBatch.DrawString(font, "P:  " + this.GetNode().ToString(), new Vector2(800, 600), Color.White); //for testing

            foreach (var thisthing in Game.Things)
            {
                if (thisthing is NonFlyer)
                {
                    debug += thisthing.Position.X + "    ";
                }

            }
            //spriteBatch.DrawString(font, "DEBUG:  " + debug, new Vector2(100, 750), Color.White);

            if (Game.Menu)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    Game.Menu = false;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || GamePad.GetState(0).IsButtonDown(Buttons.Start))
            {
                pausing = true;
                if (GamePad.GetState(0).IsButtonDown(Buttons.Start))
                    Controller = true;
                else
                    Controller = false;
            }
            else if (pausing)
            {
                Game.paused = true;
                pausing = false;
            }

            if (!pausing && Game.paused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter) || GamePad.GetState(0).IsButtonDown(Buttons.A))
                {
                    unpausing = true;
                }
                else if (unpausing)
                {
                    Game.paused = false;
                    unpausing = false;
                }

            }

            if (Position.Y >= 900)
            {
                health = 0;
                Game.dead = true;
            }
            if (Position.X >= 4000)
            {
                //Game.level++;
            }

            if (health == 0)
            {
                Game.dead = true;
            }
            //if (score >= 10)
            //{
            //    Game.Win = true;
            //}

            if (attack)
            {
                //Shoot();                
            }

        }

        public override double IsInRange(Thing otherThing)
        {
            Length = Position - (otherThing.Position - Game.Offset);
            double Distance = ((Length.X) * (Length.X)) + ((Length.Y) * (Length.Y));
            Distance = Math.Sqrt(Distance);
            //debug += Distance;
            return Distance;
        }

        //private void Shoot()
        //{
        //    attack = true;
        //    ShotTimer = 0.5f;
        //}
    }
}