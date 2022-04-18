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
using Microsoft.Xna.Framework.Content;

namespace Project
{



    public class Game : Microsoft.Xna.Framework.Game
    {
        public static bool paused = false;
        public static bool restart = false;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D PlayerSprite;
        public Texture2D BlockSprite;
        public Texture2D fishSprite;
        public Texture2D PuffinSprite;
        private Texture2D background;
        public Texture2D PauseOverlay;
        public Texture2D PauseOverlayController;
        public Texture2D DeathScreen;
        public Texture2D WinScreen1;
        public Texture2D WinScreen2;
        public List<Thing> Things = new List<Thing>();
        public List<Thing> DeadThings;
        public bool dead;
        public bool Win;
        public Vector2 Offset;
        int count = 0;
        public const int WIDTH = 1440;
        public const int HEIGHT = 810;

        public Player Player;

        SpriteFont font;

        public int frameCount = 0;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            Window.IsBorderless = false;
            Window.Position = new Point((graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width - WIDTH) / 2, (graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height - HEIGHT) / 2 - 30);
            graphics.ApplyChanges();
        }

        public void Log(String s)
        {
            System.Diagnostics.Trace.WriteLine(s);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }


        protected override void LoadContent()
        {
            if (restart)
            {
                Things.Clear();
                if (DeadThings != null)
                    DeadThings.Clear();

                restart = false;
            }

            dead = false;
            Win = false;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerSprite = Content.Load<Texture2D>("SPSS");
            BlockSprite = Content.Load<Texture2D>("block");
            fishSprite = Content.Load<Texture2D>("fish");
            PuffinSprite = Content.Load<Texture2D>("puffinplaceholder");
            background = Content.Load<Texture2D>("Background");
            PauseOverlay = Content.Load<Texture2D>("Pause Menu");
            PauseOverlayController = Content.Load<Texture2D>("Pause Menu Controller");
            font = Content.Load<SpriteFont>("Score");
            DeathScreen = Content.Load<Texture2D>("Death screen");
            WinScreen1 = Content.Load<Texture2D>("Win1");
            WinScreen2 = Content.Load<Texture2D>("Win2");

            Random rnd = new Random();


            Things.Add(new Block(this, BlockSprite, new Vector2(20, 600), new Rectangle(0, 0, 400, 20)));

            for (int i = 0; i < 5; i++)
            {
                int numberX = rnd.Next(0, 1440);
                int numberY = rnd.Next(0, 810);
                //Things.Add(new fish(this, fishSprite, new Vector2(numberX, numberY), new Rectangle(0, 0, 40, 40)));

            }


            int size = 64;
            int X = 30;
            int Y = 8;
            for (int x = 0; x < X; x++)
            {
                int lastblockY = -1;  // at what height was the last block?
                bool placepossible = true;  // can we place a block here?
                bool maxheight = false;  // true when we have max number of blocks

                for (int y = Y; y > 0; y--)
                {
                    int number = rnd.Next(-1, 2);
                    int chance = rnd.Next(0, 10);

                    if (rnd.Next(0, 10) == 1 && lastblockY != -1) //makes sure the fish isn't inside a block
                    {
                        placepossible = false; //stops it from placing the block
                        Things.Add(new fish(this, fishSprite, new Vector2(x * size + 415, y * size + 325), new Rectangle(0, 0, 40, 40)));
                    }
                    else if (rnd.Next(0, 10) > 10 - y)
                    {
                        if (y == lastblockY + 1 && maxheight == true) //checks if it is trying to place a block on top of 2 blocks
                        {
                            placepossible = false; //stops it from placing the block
                            maxheight = false;
                        }
                        else if (y == lastblockY + 1) //checks is there is a block below this point
                        {
                            maxheight = true; //the blocks can only be a maximum height of 2 for the player to be able to jump over it, so this is true if the heights of blocks is true
                        }

                        if (placepossible) //makes sure the block will be navigatable
                        {
                            Things.Add(new Block(this, BlockSprite, new Vector2(x * size + 400, y * size + 310), new Rectangle(0, 0, size, size)));
                            lastblockY = y;
                        }
                        placepossible = true;
                    }
                    else
                    {
                        lastblockY = -1;
                    }
                }
            }
            Things.Add(new Puffin(this, PuffinSprite, new Vector2(400, 400), new Rectangle(0, 0, 40, 40)));

            Player = new Player(this, PlayerSprite, new Vector2(100, 520), new Rectangle(2, 2, 35, 48), font);
            Things.Add(Player);
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {

            if (paused || dead || Win)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    restart = true;
                    LoadContent();
                    LoadContent();

                }
            }
            if (Win)
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, 1440, 810), Color.White);

            DeadThings = new List<Thing>();

            foreach (var Thing in Things)
            {
                Thing.Update(gameTime, spriteBatch);

            }

            foreach (var Thing in DeadThings)
            {
                Things.Remove(Thing);
            }

            if (paused)
            {
                if (Player.Controller)
                    spriteBatch.Draw(PauseOverlayController, Vector2.Zero, Color.White);
                else
                    spriteBatch.Draw(PauseOverlay, Vector2.Zero, Color.White);
            }

            if (dead)
            {
                spriteBatch.Draw(DeathScreen, Vector2.Zero, Color.White);
                paused = false;
            }
            if (Win && count >= 100)
            {
                spriteBatch.Draw(WinScreen2, Vector2.Zero, Color.White);
                paused = false;
            }
            else if (Win)
            {
                spriteBatch.Draw(WinScreen1, Vector2.Zero, Color.White);
                paused = false;
            }

            spriteBatch.End();

            base.Draw(gameTime);

        }

    }

}
