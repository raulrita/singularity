namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    class App : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        float delta = 0.0f;

        Texture2D background;
        Vector2 backgroundPosition;

        public App()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = Constants.ContentRootDirectory;

            graphics.PreferredBackBufferWidth = Constants.DisplayWidth;
            graphics.PreferredBackBufferHeight = Constants.DisplayHeight;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            graphics.IsFullScreen = Constants.FullScreen;
            
            graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

            //HiDef enables usable shaders
            graphics.GraphicsProfile = GraphicsProfile.HiDef;

            Window.Title = String.Format("{0} v{1}", Constants.Title, Constants.Version);
            
            Window.AllowUserResizing = Constants.AllowResize;
            Window.Position = new Point(
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - Constants.DisplayWidth / 2,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - Constants.DisplayHeight / 2);            
                
            IsMouseVisible = Constants.ShowMouse;

            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            int x = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Constants.DisplayWidth) / 2;
            int y = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Constants.DisplayHeight) / 2;

            graphics.GraphicsDevice.Viewport = new Viewport(0, 0, Constants.DisplayWidth, Constants.DisplayHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Globals.GraphicsDevice = GraphicsDevice;            
            Globals.SpriteBatch = spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.ContentManager = Content;
            Globals.EntityManager = new EntityManager();
            Globals.Font = Content.Load<SpriteFont>("gamefont");
            Globals.Random = new Random();

            background = Content.Load<Texture2D>("background");

            backgroundPosition.X = Constants.DisplayWidth / 2 - background.Width / 2;
            backgroundPosition.Y = Constants.DisplayHeight / 2 - background.Height / 2;

            /*
            int x = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - Constants.DisplayWidth) / 2;
            int y = (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - Constants.DisplayHeight) / 2;

            //GraphicsDevice.Viewport = new Viewport(x, y, Constants.DisplayWidth, Constants.DisplayHeight);
            */
            Globals.CurrentScene = (Scene)Activator.CreateInstance(Constants.StartupSceneType);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            Globals.KeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            delta = (float)gameTime.ElapsedGameTime.TotalSeconds / Constants.FrameTime;

            if (Globals.NextScene != null)
            {
                Globals.CurrentScene = null;
                Globals.EntityManager.Clear();
                Globals.CurrentScene = (Scene)Activator.CreateInstance(Globals.NextScene);
                Globals.NextScene = null;
            }

            if (Globals.CurrentScene != null)
                Globals.CurrentScene.Update(delta);

            Globals.EntityManager.Update(delta);

            Globals.KeyboardOldState = Globals.KeyboardState;
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Constants.BackgroundColor);

            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.Draw(background, backgroundPosition, Color.White);

            if (Globals.CurrentScene != null)
                Globals.CurrentScene.Draw();

            Globals.EntityManager.Draw();

            spriteBatch.End();
        }
    }
}
