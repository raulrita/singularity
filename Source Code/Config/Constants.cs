namespace Neuro
{
    using Microsoft.Xna.Framework;
    using System;

    abstract class Constants
    {
        public const string Title = "Neuro";

        public const string Version = "0.1";

        public const string ContentRootDirectory = "Content";

        public const string DefaultFont = "DefaultFont";

        public const float FPS = 60; // frames per second

        public const float FrameTime = 1.0f / FPS; // time per each frame

        public const int DisplayWidth = 1920;//1920;

        public const int DisplayHeight= 1080;//1080;

        public const int WindowWidth = 1920;// / 3 * 2;

        public const int WindowHeight = 1080;// / 3 * 2;

        public const bool FullScreen = true;

        public const bool AllowResize = false;

        public const bool ShowMouse = false;

        public const bool Debug = true;

        public static readonly Type StartupSceneType = typeof(Menu);

        public static readonly Color BackgroundColor = new Color(12, 12, 12);

        public const int TileWidth = 270;

        public const int TileHeight = 158;

        public const int TileHalfWidth = TileWidth / 2;

        public const int TileHalfHeight = TileHeight / 2;
        
        public const int LevelCount = 3;

        public const float MoveTime = 450.0f / 1000;        

        public const float ShowTime = 1.0f; // seconds

        public const int TileTextureCount = 1;

        public static readonly Vector2 Offset = new Vector2(650, 525);

        public static readonly Vector2 TileStartPosition = new Vector2(0, 0);
        public static readonly Vector2 TileResetPosition = new Vector2(0, 7);

        public static readonly float TileTime = 1450.0f / 1000;

        public static readonly float AnimFrameTime = 1.0f / 10;

        public const float DefaultScrollSpeed = 0.0075f;
    }
}
