namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    class Menu : Scene
    {
        Texture2D title;
        Vector2 titlePosition;

        Texture2D button;
        Texture2D button1;
        Vector2 buttonPosition;

        bool first;
        float time;    
        const float BLINK = 450.0f / 1000.0f;

        public Menu()
        {
            title = ContentManager.Load<Texture2D>("menu\\title");
            button = ContentManager.Load<Texture2D>("menu\\play");
            button1 = ContentManager.Load<Texture2D>("menu\\play1");

            titlePosition = new Vector2(
                Constants.DisplayWidth / 2 - title.Width / 2,
                100
                );

            buttonPosition = new Vector2(
                Constants.DisplayWidth / 2 - button.Width / 2,
                600
                );

            time = 0.0f;
            first = false;
            Globals.Level = 1;
        }

        public override void Update(float delta)
        {
            time += delta * Constants.FrameTime;

            if (time > BLINK)
            {
                time = 0.0f;
                first = !first;
            }

            if (Helpers.IsAnyKeyUp())
                Globals.NextScene = typeof(GamePlay);

            base.Update(delta);
        }

        public override void Draw()
        {            
            SpriteBatch.Draw(title, titlePosition, Color.White);

            SpriteBatch.Draw(
                first ? button : button1, 
                buttonPosition, 
                Color.White);
        }
    }
}
