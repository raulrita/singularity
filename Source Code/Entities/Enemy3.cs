namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class Enemy3 : Actor
    {
        public Enemy3(int pos)
            : base(new Vector2(0, -9.5f), "enemy3")
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
