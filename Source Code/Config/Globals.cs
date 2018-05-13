namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;

    abstract partial class Globals
    {
        public static Scene CurrentScene;

        public static Type NextScene;

        public static GraphicsDevice GraphicsDevice;

        public static ContentManager ContentManager;

        public static SpriteBatch SpriteBatch;

        public static EntityManager EntityManager;

        public static SpriteFont Font;

        public static Vector2 Offset = Vector2.Zero;

        public static KeyboardState KeyboardState;

        public static KeyboardState KeyboardOldState;

        public static bool Scrolling = true;

        public static Random Random;

        public static int Level = 1;

        public static bool ShowBounds = false;

        public static float ScrollSpeed = 0.0075f;

        public static float Accuracy = 0;
    }
}