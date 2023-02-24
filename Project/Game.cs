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
using Microsoft.Xna.Framework.Audio;

namespace Project
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static bool paused = false;
        public static bool restart = false;
        GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;
        protected Texture2D MenuScreen;
        protected Texture2D PlayerSprite;
        protected Texture2D BlockSprite;
        protected Texture2D fishSprite;
        protected Texture2D BubbleSprite;
        protected Texture2D BalloonSprite;
        protected Texture2D EmptySprite;
        protected Texture2D PuffinSprite;
        protected Texture2D SealSprite;
        protected Texture2D KangarooSprite;
        protected Texture2D WombatSprite;
        protected Texture2D RatSprite;
        protected Texture2D SpiderSprite;
        protected Texture2D JellyfishSprite;
        protected Texture2D SharkSprite;
        protected Texture2D MonkeySprite;
        protected Texture2D DogSprite;
        protected Texture2D FlyerEnemy1;
        protected Texture2D FlyerEnemy2;
        protected Texture2D NonFlyerEnemy1;
        protected Texture2D NonFlyerEnemy2;
        Song song1;
        Song song2;
        Song song3;

        //creating arrays for the background images
        private Texture2D[] levelBackgrounds1 = new Texture2D[7];
        private Texture2D[] levelBackgrounds2 = new Texture2D[7];
        private Texture2D[] levelBackgrounds3 = new Texture2D[7];

        //creating array for the oxygen bar in the water level
        public Texture2D[] Oxygen = new Texture2D[5];




        protected Texture2D PauseOverlay;
        protected Texture2D PauseOverlayController;
        protected Texture2D DeathScreen;
        protected Texture2D WinScreen1;
        protected Texture2D WinScreen2;
        public Texture2D OxygenEmpty;
        public Texture2D Oxygen20;
        public Texture2D Oxygen40;
        public Texture2D Oxygen60;
        public Texture2D Oxygen80;
        public Texture2D OxygenFull;
        public Texture2D underwatereffect;
        public Texture2D snowballSprite;
        public List<Thing> Things = new List<Thing>();
        public List<Thing> DeadThings;
        public bool dead;
        public bool Win;
        bool fishempty = false;
        public Vector2 Offset;
        int count = 0;
        public const int WIDTH = 1440;
        public const int HEIGHT = 810;
        public bool Menu = true;



        public Player Player;

        SpriteFont font;

        public int frameCount = 0;
        public int level = 1;
        public int previouslevel;


        public HashSet<Vector2> blockedNodes;


        public Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.IsBorderless = true;
            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
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
                Things.Clear(); //delete everything from the game & start over
                if (DeadThings != null)
                    DeadThings.Clear();

                restart = false;
            }


            this.song2 = Content.Load<Song>("tune2");
            MediaPlayer.Play(song2); 
            MediaPlayer.IsRepeating = true;


            blockedNodes = new HashSet<Vector2>();


            Random rnd = new Random();

            //rnd.Next(1, 6);

            dead = false;
            Win = false;

            //Loading all assets/sprites
            MenuScreen = Content.Load<Texture2D>("Menu Screen");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerSprite = Content.Load<Texture2D>("SPSS");
            BlockSprite = Content.Load<Texture2D>("block" + level);
            fishSprite = Content.Load<Texture2D>("fish");
            BubbleSprite = Content.Load<Texture2D>("Bubbles");
            BalloonSprite = Content.Load<Texture2D>("balloons");


            PuffinSprite = Content.Load<Texture2D>("puffin");
            SealSprite = Content.Load<Texture2D>("seal");
            KangarooSprite = Content.Load<Texture2D>("kangaroo");
            WombatSprite = Content.Load<Texture2D>("wombat");
            RatSprite = Content.Load<Texture2D>("rat");
            SpiderSprite = Content.Load<Texture2D>("spider");
            JellyfishSprite = Content.Load<Texture2D>("jellyfish");
            SharkSprite = Content.Load<Texture2D>("shark");
            MonkeySprite = Content.Load<Texture2D>("spacemonkey");
            DogSprite = Content.Load<Texture2D>("spacedog");
            List<Song> soundEffects = new List<Song>();


            Enemy[] Enemies1 = new Enemy[] { null, new Flyer(this, PuffinSprite), new NonFlyer(this, KangarooSprite), new NonFlyer(this, RatSprite), new Flyer(this, JellyfishSprite), new Flyer(this, MonkeySprite) };
            Enemy[] Enemies2 = new Enemy[] { null, new NonFlyer(this, SealSprite), new NonFlyer(this, WombatSprite), new NonFlyer(this, SpiderSprite), new Flyer(this, SharkSprite), new Flyer(this, DogSprite) };



            for (int i = 1; i < 6; i++)
            {
                levelBackgrounds1[i] = Content.Load<Texture2D>("Background" + (i * 3 - 2));
                levelBackgrounds2[i] = Content.Load<Texture2D>("Background" + (i * 3 - 1));
                levelBackgrounds3[i] = Content.Load<Texture2D>("Background" + (i * 3));
            }
            Things.Add(new Background(this, levelBackgrounds1[level], new Vector2(-500, -300)));

            Things.Add(new Background(this, levelBackgrounds2[level], new Vector2(1548, -300)));

            Things.Add(new Background(this, levelBackgrounds3[level], new Vector2(3596, -300)));

            PauseOverlay = Content.Load<Texture2D>("Pause Menu");
            PauseOverlayController = Content.Load<Texture2D>("Pause Menu Controller");
            font = Content.Load<SpriteFont>("Score");
            DeathScreen = Content.Load<Texture2D>("Death screen");
            WinScreen1 = Content.Load<Texture2D>("Win1");
            WinScreen2 = Content.Load<Texture2D>("Win2");
            underwatereffect = Content.Load<Texture2D>("underwater");
            snowballSprite = Content.Load<Texture2D>("Snowball");


            //filling the oxygen array with the correct bar percentage
            Oxygen = new Texture2D[]
            {
                Content.Load<Texture2D>("Oxygen empty"),
                Content.Load<Texture2D>("Oxygen 20"),
                Content.Load<Texture2D>("Oxygen 40"),
                Content.Load<Texture2D>("Oxygen 60"),
                Content.Load<Texture2D>("Oxygen 80"),
                Content.Load<Texture2D>("Oxygen full"),
            };






            if (level != 4)
            {
                for (int i = 1; i < 6; i++)
                {
                    createBlock(i, 9);
                }
            }


            int blockoffsetX = 6;
            int blockoffsetY = 1;

            int fishoffsetX = 415;
            int fishoffsetY = 69;

            int PlayerposX = 100;
            int PlayerposY = 500;




            if (level == 4)
            {
                for (int i = 0; i < 10; i++)
                {
                    createBlock(-5, i);
                    createBlock(80, i);
                }
                for (int i = -4; i < 81; i++)
                {
                    createBlock(i, 0);
                    createBlock(i, 9);
                }

                blockoffsetX = 0;
                blockoffsetY = 0;
                fishoffsetX = 0;
                fishoffsetY = 0;

                PlayerposX = -100;
                PlayerposY = 200;
            }



            int size = 64;
            int X = 64;
            int Y;
            if (level == 4)
                Y = 8;
            else
                Y = 12;




            for (int f = 0; f < 13; f++)
            {
                Things.Add(new LevelEnd(this, BalloonSprite, new Vector2(4000, (f * 40) + 200), new Rectangle(0, 0, 40, 40))); //change texture from bubbles
            }

            for (int x = 0; x < X; x++)
            {
                int lastblockY = -1;  // at what height was the last block?
                bool placepossible = true;  // can we place a block here?
                bool maxheight = false;  // true when we have max number of blocks



                for (int y = Y; y > 0; y--)
                {
                    int number = rnd.Next(-1, 2);
                    int chance = rnd.Next(0, 10);

                    if (rnd.Next(0, 10) > 10 - y)
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
                            createBlock(x + blockoffsetX, y + blockoffsetY);
                            lastblockY = y;
                        }
                        placepossible = true;
                    }
                    else if (rnd.Next(0, 10) == 1)
                    {
                        if (level == 4 && rnd.Next(0, 2) == 1)
                        {
                            Things.Add(new Bubbles(this, BubbleSprite, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY)));
                        }
                        else
                        {
                            Things.Add(new fish(this, fishSprite, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY)));
                        }
                        placepossible = false; //stops it from placing the block
                    }
                    else
                    {
                        lastblockY = -1;
                    }
                }

            }


            for (int i = 0; i < 1; i++)
            {
                int x;
                int y;
                Vector2 v;
                do
                {
                    x = rnd.Next(X);
                    y = rnd.Next(Y);
                    v = new Vector2(x, y);
                } while (blockedNodes.Contains(v));
                Enemy enemy1 = this.CreateEnemy1(level);
                Things.Add(enemy1);
                enemy1.setPosition(new Vector2(x * size + fishoffsetX, y * size + fishoffsetY));

                do
                {
                    x = rnd.Next(X);
                    y = rnd.Next(Y);
                    v = new Vector2(x, y);
                } while (blockedNodes.Contains(v));
                Enemy enemy2 = this.CreateEnemy2(level);
                Things.Add(enemy2);
                enemy2.setPosition(new Vector2(x * size + fishoffsetX, y * size + fishoffsetY));


            }


            Player = new Player(this, PlayerSprite, new Vector2(PlayerposX, PlayerposY), font, snowballSprite);
            Things.Add(Player);


        }


        private Enemy CreateEnemy1(int level)
        {
            Enemy[] Enemies1 = new Enemy[] { null, new Flyer(this, PuffinSprite), new NonFlyer(this, KangarooSprite), new NonFlyer(this, RatSprite), new Flyer(this, JellyfishSprite), new Flyer(this, MonkeySprite) };

            return Enemies1[level];
        }

        private Enemy CreateEnemy2(int level)
        {
            Enemy[] Enemies2 = new Enemy[] { null, new NonFlyer(this, SealSprite), new NonFlyer(this, WombatSprite), new NonFlyer(this, SpiderSprite), new Flyer(this, SharkSprite), new NonFlyer(this, DogSprite) };

            return Enemies2[level];
        }





        //Add a block to the game
        private void createBlock(int x, int y)
        {
            //Console.WriteLine(">" + x + ", " + y);
            Block block = new Block(this, BlockSprite, new Vector2(x * 64, y * 64));
            Things.Add(block);
            //Vector2 node = new Vector2(x, y + 1);
            blockedNodes.Add(block.GetNode());
            Console.WriteLine(block.GetNode().ToString() + " ... " + block.Position.ToString());
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {

            if (restart)
            {
                LoadContent();

            }
            if (Menu || paused || dead || Win)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    restart = true;
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

            fishempty = true;
            foreach (var Thing in Things)
            {
                if (Thing is fish)
                {
                    fishempty = false;
                }
            }

            if (fishempty)
            {
                Win = true;
            }
        }

        public void message(string s)
        {
            //spriteBatch.DrawString(font, "DEBUG:  " + s, new Vector2(100, 750), Color.White);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            DeadThings = new List<Thing>();

            //update everything
            foreach (var Thing in Things)
            {
                Thing.Update(gameTime, spriteBatch);
            }

            //remove all the DeadThings from the game
            foreach (var Thing in DeadThings)
            {
                Things.Remove(Thing);
            }

            if (level == 4)
            {
                spriteBatch.Draw(underwatereffect, Vector2.Zero, Color.White);
            }

            if (paused)
            {
                if (Player.Controller)
                    spriteBatch.Draw(PauseOverlayController, Vector2.Zero, Color.White); //pause screen controller
                else
                    spriteBatch.Draw(PauseOverlay, Vector2.Zero, Color.White); //pause screen keyboard
            }

            if (Menu)
            {
                spriteBatch.Draw(MenuScreen, Vector2.Zero, Color.White); //menu screen
            }
            if (dead)
            {
                spriteBatch.Draw(DeathScreen, Vector2.Zero, Color.White); //death screen
                paused = false;
            }
            if (Win && count >= 100)
            {
                spriteBatch.Draw(WinScreen2, Vector2.Zero, Color.White); //credits roll
                paused = false;
            }
            else if (Win)
            {
                spriteBatch.Draw(WinScreen1, Vector2.Zero, Color.White); //win screen
                paused = false;
            }


            spriteBatch.End();

            base.Draw(gameTime);

        }

    }

}
