namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class Asteroid : Actor
    {
        public Asteroid(int pos)
            : base(new Vector2(0, -9.5f), "rock")
        {
            Position.X += pos; // -1, 0, 1
            ScreenPosition = CalcScreenPosition(Position);
        }

        public override void Update(float delta)
        {
            if (!Globals.Scrolling)
                return;

            ScreenPosition.X -= (float)Constants.TileWidth * Globals.ScrollSpeed * delta;
            ScreenPosition.Y += (float)Constants.TileHeight * Globals.ScrollSpeed * delta;

            base.Update(delta);
        }
    }
}
