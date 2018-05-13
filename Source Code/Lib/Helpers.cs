namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    static class Helpers
    {
        public static Vector2 IsoPos(Vector2 position)
        {
            Vector2 result = Vector2.Zero;

            result.X = (float)(position.X - position.Y) * Constants.TileHalfWidth;
            result.Y = (float)(position.X + position.Y) * Constants.TileHalfHeight;

            result += Constants.Offset;

            return result;
        }

        public static bool IsKeyUp(Keys key)
        {
            return Globals.KeyboardState.IsKeyUp(key) && 
                Globals.KeyboardOldState.IsKeyDown(key);
        }

        public static bool IsAnyKeyUp()
        {
            return IsKeyUp(Keys.Enter);

            //return Keyboard.GetState().GetPressedKeys().Length > 0;
        }
    }
}
