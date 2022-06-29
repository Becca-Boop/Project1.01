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
        public Texture2D BubbleSprite;
        public Texture2D PuffinSprite;
        public Texture2D SealSprite;
        public Texture2D KangarooSprite;
        public Texture2D WombatSprite;
        public Texture2D RatSprite;
        public Texture2D SpiderSprite;
        public Texture2D JellyfishSprite;
        public Texture2D StarfishSprite;
        public Texture2D SharkSprite;
        public Texture2D MonkeySprite;
        public Texture2D DogSprite;
        public Texture2D FlyerEnemy1;
        public Texture2D FlyerEnemy2;
        public Texture2D NonFlyerEnemy1;
        public Texture2D NonFlyerEnemy2;



        private Texture2D l1background1;
        private Texture2D l1background2;
        private Texture2D l1background3;
        private Texture2D l2background1;
        private Texture2D l2background2;
        private Texture2D l2background3;
        private Texture2D l3background1;
        private Texture2D l3background2;
        private Texture2D l3background3;
        private Texture2D l4background1;
        private Texture2D l4background2;
        private Texture2D l4background3;
        private Texture2D l5background1;
        private Texture2D l5background2;
        private Texture2D l5background3;


        public Texture2D PauseOverlay;
        public Texture2D PauseOverlayController;
        public Texture2D DeathScreen;
        public Texture2D WinScreen1;
        public Texture2D WinScreen2;
        public Texture2D OxygenEmpty;
        public Texture2D Oxygen20;
        public Texture2D Oxygen40;
        public Texture2D Oxygen60;
        public Texture2D Oxygen80;
        public Texture2D OxygenFull;
        public Texture2D underwatereffect;
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
        public int level;
        

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

            Random Level = new Random();
            level = Level.Next(1, 6);

            dead = false;
            Win = false;
            

            spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerSprite = Content.Load<Texture2D>("SPSS");
            BlockSprite = Content.Load<Texture2D>("block" +  level);
            fishSprite = Content.Load<Texture2D>("fish");
            BubbleSprite = Content.Load<Texture2D>("Bubbles");
            PuffinSprite = Content.Load<Texture2D>("puffinplaceholder");
            SealSprite = Content.Load<Texture2D>("Seal");
            KangarooSprite = Content.Load<Texture2D>("kangaroo");
            WombatSprite = Content.Load<Texture2D>("wombat");
            RatSprite = Content.Load<Texture2D>("rat");
            SpiderSprite = Content.Load<Texture2D>("spider");
            JellyfishSprite = Content.Load<Texture2D>("jellyfish");
            StarfishSprite = Content.Load<Texture2D>("starfish");
            SharkSprite = Content.Load<Texture2D>("shark");
            MonkeySprite = Content.Load<Texture2D>("spacemonkey");
            DogSprite = Content.Load<Texture2D>("spacedog");

            l1background1 = Content.Load<Texture2D>("Background1");
            l1background2 = Content.Load<Texture2D>("Background2");
            l1background3 = Content.Load<Texture2D>("Background3");
            l2background1 = Content.Load<Texture2D>("Background4");
            l2background2 = Content.Load<Texture2D>("Background5");
            l2background3 = Content.Load<Texture2D>("Background6");
            l3background1 = Content.Load<Texture2D>("Background7");
            l3background2 = Content.Load<Texture2D>("Background8");
            l3background3 = Content.Load<Texture2D>("Background9");
            l4background1 = Content.Load<Texture2D>("Background10");
            l4background2 = Content.Load<Texture2D>("Background11");
            l4background3 = Content.Load<Texture2D>("Background12");
            l5background1 = Content.Load<Texture2D>("Background13");
            l5background2 = Content.Load<Texture2D>("Background14");
            l5background3 = Content.Load<Texture2D>("Background15");
            PauseOverlay = Content.Load<Texture2D>("Pause Menu");
            PauseOverlayController = Content.Load<Texture2D>("Pause Menu Controller");
            font = Content.Load<SpriteFont>("Score");
            DeathScreen = Content.Load<Texture2D>("Death screen");
            WinScreen1 = Content.Load<Texture2D>("Win1");
            WinScreen2 = Content.Load<Texture2D>("Win2");
            OxygenEmpty = Content.Load<Texture2D>("Oxygen empty");
            Oxygen20 = Content.Load<Texture2D>("Oxygen 20");
            Oxygen40 = Content.Load<Texture2D>("Oxygen 40");
            Oxygen60 = Content.Load<Texture2D>("Oxygen 60");
            Oxygen80 = Content.Load<Texture2D>("Oxygen 80");
            OxygenFull = Content.Load<Texture2D>("Oxygen full");
            underwatereffect = Content.Load<Texture2D>("underwater");

            

            Random rnd = new Random();
            if (level == 1)
            {
                Things.Add(new Background(this, l1background1, new Vector2(-500, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l1background2, new Vector2(1548, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l1background3, new Vector2(3596, -300), new Rectangle(0, 0, 2048, 1500)));

                FlyerEnemy1 = PuffinSprite;
                FlyerEnemy2 = null;
                NonFlyerEnemy1 = SealSprite;
                NonFlyerEnemy2 = null;
            }
            else if (level == 2)
            {
                Things.Add(new Background(this, l2background1, new Vector2(-500, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l2background2, new Vector2(1548, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l2background3, new Vector2(3596, -300), new Rectangle(0, 0, 2048, 1500)));

                FlyerEnemy1 = null;
                FlyerEnemy2 = null;
                NonFlyerEnemy1 = KangarooSprite;
                NonFlyerEnemy2 = WombatSprite;
            }
            else if (level == 3)
            {
                Things.Add(new Background(this, l3background1, new Vector2(-500, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l3background2, new Vector2(1548, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l3background3, new Vector2(3596, -300), new Rectangle(0, 0, 2048, 1500)));

                FlyerEnemy1 = null;
                FlyerEnemy2 = null;
                NonFlyerEnemy1 = RatSprite;
                NonFlyerEnemy2 = SpiderSprite;
            }
            else if (level == 4)
            {
                Things.Add(new Background(this, l4background1, new Vector2(-500, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l4background2, new Vector2(1548, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l4background3, new Vector2(3596, -300), new Rectangle(0, 0, 2048, 1500)));

                FlyerEnemy1 = JellyfishSprite;
                FlyerEnemy2 = SharkSprite;
                NonFlyerEnemy1 = null;
                NonFlyerEnemy2 = null;
            }
            else if (level == 5)
            {
                Things.Add(new Background(this, l5background1, new Vector2(-500, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l5background2, new Vector2(1548, -300), new Rectangle(0, 0, 2048, 1500)));
                Things.Add(new Background(this, l5background3, new Vector2(3596, -300), new Rectangle(0, 0, 2048, 1500)));

                FlyerEnemy1 = MonkeySprite;
                FlyerEnemy2 = DogSprite;
                NonFlyerEnemy1 = null;
                NonFlyerEnemy2 = null;
            }
            

            if (level != 4)
            {
                Things.Add(new Block(this, BlockSprite, new Vector2(16, 566), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(80, 566), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(144, 566), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(208, 566), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(272, 566), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(336, 566), new Rectangle(0, 0, 64, 64)));
            }
            

            int blockoffsetX = 400;
            int blockoffsetY = 54;

            int fishoffsetX = 415;
            int fishoffsetY = 69;

            int PlayerposX = 100;
            int PlayerposY = 500;




            if (level == 4)
            {
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 1), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 2), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 3), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 4), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 5), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 6), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 7), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 8), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 64 * 9), new Rectangle(0, 0, 64, 64)));

                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 1), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 2), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 3), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 4), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 5), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 6), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 7), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 8), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 64 * 9), new Rectangle(0, 0, 64, 64)));


                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -4, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -3, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -2, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -1, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 0, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 1, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 2, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 3, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 4, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 5, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 6, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 7, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 8, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 9, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 10, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 11, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 12, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 13, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 14, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 15, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 16, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 17, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 18, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 19, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 20, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 21, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 22, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 23, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 24, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 25, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 26, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 27, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 28, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 29, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 30, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 31, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 32, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 33, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 34, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 35, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 36, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 37, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 38, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 39, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 40, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 41, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 42, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 43, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 44, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 45, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 46, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 47, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 48, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 49, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 50, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 51, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 52, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 53, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 54, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 55, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 56, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 57, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 58, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 59, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 60, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 61, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 62, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 63, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 64, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 65, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 66, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 67, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 68, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 69, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 70, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 71, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 72, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 73, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 74, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 75, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 76, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 77, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 78, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 79, 0), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 0), new Rectangle(0, 0, 64, 64)));



                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -5, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -4, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -3, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -2, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * -1, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 0, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 1, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 2, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 3, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 4, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 5, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 6, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 7, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 8, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 9, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 10, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 11, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 12, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 13, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 14, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 15, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 16, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 17, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 18, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 19, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 20, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 21, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 22, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 23, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 24, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 25, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 26, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 27, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 28, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 29, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 30, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 31, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 32, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 33, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 34, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 35, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 36, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 37, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 38, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 39, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 40, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 41, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 42, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 43, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 44, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 45, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 46, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 47, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 48, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 49, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 50, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 51, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 52, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 53, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 54, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 55, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 56, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 57, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 58, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 59, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 60, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 61, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 62, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 63, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 64, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 65, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 66, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 67, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 68, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 69, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 70, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 71, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 72, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 73, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 74, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 75, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 76, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 77, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 78, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 79, 576), new Rectangle(0, 0, 64, 64)));
                Things.Add(new Block(this, BlockSprite, new Vector2(64 * 80, 576), new Rectangle(0, 0, 64, 64)));

                blockoffsetX = 0;
                blockoffsetY = 0;
                fishoffsetX = 0;
                fishoffsetY = 0;

                PlayerposX = -100;
                PlayerposY = 200;
            }


            //for (int i = 0; i < 5; i++)
            //{
            //    int numberX = rnd.Next(0, 1440);
            //    int numberY = rnd.Next(0, 810);
            //    //Things.Add(new fish(this, fishSprite, new Vector2(numberX, numberY), new Rectangle(0, 0, 40, 40)));

            //}


            int size = 64;
            int X = 64;
            int Y;
            if (level == 4)
                Y = 8;
            else
                Y = 12;
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
                            Things.Add(new Block(this, BlockSprite, new Vector2(x * size + blockoffsetX, y * size + blockoffsetY), new Rectangle(0, 0, size, size)));
                            lastblockY = y;
                        }
                        placepossible = true;
                    }
                    else if (rnd.Next(0, 10) == 1)
                    {
                        if (level == 4 && rnd.Next(0, 2) == 1)
                        {
                            Things.Add(new Bubbles(this, BubbleSprite, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                        }
                        else if (rnd.Next(0, 3) == 1)
                        {
                            //if(rnd.Next(0,2) == 0 && FlyerEnemy1 != null)
                            //    Things.Add(new Flyer(this, FlyerEnemy1, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                            //else if(rnd.Next(0, 4) == 1 && FlyerEnemy2 != null)
                            //    Things.Add(new Flyer(this, FlyerEnemy2, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                            //else if (rnd.Next(0, 4) == 2 && NonFlyerEnemy1 != null)
                            //    Things.Add(new NonFlyer(this, NonFlyerEnemy1, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                            //else if (rnd.Next(0, 4) == 3 && NonFlyerEnemy2 != null)
                            //    Things.Add(new NonFlyer(this, NonFlyerEnemy2, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                            Texture2D enemy1 = null;
                            Texture2D enemy2 = null;


                            if (FlyerEnemy1 != null)
                            {
                                enemy1 = FlyerEnemy1;
                            }
                            else if (FlyerEnemy2 != null)
                            {
                                enemy1 = FlyerEnemy2;
                            }
                            else if (NonFlyerEnemy1 != null)
                            {
                                enemy1 = NonFlyerEnemy1;
                            }


                            if (FlyerEnemy2 != null && enemy1 != FlyerEnemy2)
                            {
                                enemy2 = FlyerEnemy2;
                            }
                            else if (NonFlyerEnemy1 != null && enemy1 != NonFlyerEnemy1)
                            {
                                enemy2 = NonFlyerEnemy1;
                            }
                            else if (NonFlyerEnemy2 != null)
                            {
                                enemy2 = NonFlyerEnemy2;
                            }
                            

                            if (rnd.Next(0, 2) == 0)
                            {                            
                                if(enemy1 != NonFlyerEnemy1)
                                    Things.Add(new Flyer(this, enemy1, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                                else
                                    Things.Add(new NonFlyer(this, enemy1, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                            }
                            else
                            {
                                if (enemy2 != FlyerEnemy2)
                                    Things.Add(new NonFlyer(this, enemy2, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                                else
                                    Things.Add(new Flyer(this, enemy2, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                            }
                        }
                        else
                        {
                            Things.Add(new fish(this, fishSprite, new Vector2(x * size + fishoffsetX, y * size + fishoffsetY), new Rectangle(0, 0, 40, 40)));
                        }                        
                        placepossible = false; //stops it from placing the block
                    }
                    //else if (rnd.Next(0, 10) > 10 - y)
                    //{
                    //    if (y == lastblockY + 1 && maxheight == true) //checks if it is trying to place a block on top of 2 blocks
                    //    {
                    //        placepossible = false; //stops it from placing the block
                    //        maxheight = false;
                    //    }
                    //    else if (y == lastblockY + 1) //checks is there is a block below this point
                    //    {
                    //        maxheight = true; //the blocks can only be a maximum height of 2 for the player to be able to jump over it, so this is true if the heights of blocks is true
                    //    }

                    //    if (placepossible) //makes sure the block will be navigatable
                    //    {
                    //        Things.Add(new Block(this, BlockSprite, new Vector2(x * size + blockoffsetX, y * size + blockoffsetY), new Rectangle(0, 0, size, size)));
                    //        lastblockY = y;
                    //    }
                    //    placepossible = true;
                    //}
                    else
                    {
                        lastblockY = -1;
                    }
                }
            }
            //int Puffins = rnd.Next(1, 5);
            //for (int i = 0; i < Puffins; i++)
            //{
            //    int puffinX = rnd.Next(1, 4500);
            //    int puffinY = rnd.Next(1, 400);
            //    Things.Add(new Enemy(this, PuffinSprite, new Vector2(puffinX, puffinY), new Rectangle(0, 0, 40, 40)));

            //}



            Player = new Player(this, PlayerSprite, new Vector2(PlayerposX, PlayerposY), new Rectangle(2, 2, 35, 48), font);
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

            //spriteBatch.Draw(background1, new Rectangle(0, 0, 1440, 810), Color.White);
            //spriteBatch.Draw(Texture, Position - Game.Offset, LittleBoundingBox, Color.White);


            DeadThings = new List<Thing>();

            foreach (var Thing in Things)
            {
                Thing.Update(gameTime, spriteBatch);

            }

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
