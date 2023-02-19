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
    public class Enemy : Thing
    {
        public bool ignoreBlocks = false;
        float dt = 0;
        float tt = 0;

        public Enemy(Game game, Texture2D _texture) : base(game, _texture, new Rectangle(0, 0, 40, 40))
        {
        }

        public void setPosition(Vector2 _position)
        {
            this.Position = _position;
            BigBoundingBox = new Rectangle(LittleBoundingBox.X + (int)Position.X, LittleBoundingBox.Y + (int)Position.Y, LittleBoundingBox.Width, LittleBoundingBox.Height);
        }

        public override void Collision(Thing otherThing)
        {
            {
                if (1000 < tt - Game.Player.hittime)
                {
                    Game.Player.health--;
                    Game.Player.hittime = tt;
                }
            }
        }

        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Game.Menu || Game.paused || Game.dead || Game.Win) return;

            dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            tt = (float)gameTime.TotalGameTime.TotalMilliseconds;

            Vector2 move = GetMove(dt);

            BigBoundingBox.X += (int)move.X;
            BigBoundingBox.Y += (int)move.Y;
            Thing Collider = this.IsColliding(Game);
            //if (Collider != null) Console.WriteLine("Collided with " + Collider.GetType());

            //Game.message("Collided with " + (Collider == null ? "nothing" : Collider.GetType().ToString()));

            bool flag;
            if (Collider is Block)
            {
                BigBoundingBox.X -= (int)move.X;
                BigBoundingBox.Y -= (int)move.Y;
                this.Bump();
                flag = true;
            }
            else
            {
                Position.X += (int)move.X;
                Position.Y += (int)move.Y;
                flag = false;
            }

            SpriteEffects s = SpriteEffects.None;
            if (move.X < 0)
            {
                s = SpriteEffects.FlipHorizontally;
            }
            else
            {
                s = SpriteEffects.None;
            }
            //Console.WriteLine("" + dt + " - " + move.X + ", " + move.Y + " - " + Position.X + ", " + Position.Y + " - " + flag);

            spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);
        }

        public virtual Vector2 GetMove(float dt)
        {
            return new Vector2(0, 0);
        }

        public static void placeInGame(Game game, List<Thing> Things, Thing thing, int size)
        {
            Random rnd = new Random();
            Thing collider;
            do
            {
                thing.BigBoundingBox.X = rnd.Next(0, 8) * size;
                thing.BigBoundingBox.Y = rnd.Next(0, 8) * size;
                collider = thing.IsColliding(game);
            }
            while (collider != null)            ;

            thing.Position.X = thing.BigBoundingBox.X;
            thing.Position.Y = thing.BigBoundingBox.Y;



            Things.Add(thing);

        }

        public virtual void Bump()
        {
            Console.WriteLine("default bump");
        }
    }
}