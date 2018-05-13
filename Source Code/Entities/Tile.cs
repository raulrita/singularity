namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    
    class Tile : Entity
    {
        public Vector2 Position;
        public TileType Type;
        public bool ToReset;
        public AlignType Align;

        Texture2D texture;
        bool initialized;            
        Vector2 targetPosition;
        float rotation = 0.0f;
        Vector2 auxScreen;
        float depth;

        public Rectangle Bounds = new Rectangle();
        private Texture2D pixel;

        public Tile()
        {
            initialized = false;
            pixel = ContentManager.Load<Texture2D>("pixel");
        }

        public override void Update(float delta)
        {
            if (!Globals.Scrolling)
                return;

            rotation += delta / 4.0f;

            Position.X -= (float)Constants.TileWidth * Globals.ScrollSpeed * delta;
            Position.Y += (float)Constants.TileHeight * Globals.ScrollSpeed * delta;

            ToReset = Position.Y > targetPosition.Y;
            //if (Position.Y >= targetPosition.Y)
                //Reset(texture, Type, Align, 8);

            Bounds.X = (int)(Position.X + Constants.TileHalfWidth * 0.75f);
            Bounds.Y = (int)(Position.Y + Constants.TileHalfHeight / 2);

            Bounds.Width = Constants.TileHalfWidth / 2;
            Bounds.Height = Constants.TileHalfHeight;
        }

        public override void Draw()
        {
            if (!initialized || Type == TileType.HOLE)
                return;

            auxScreen = Position;

            auxScreen.Y += (float)(Math.Sin(rotation) * Math.PI);
            
            SpriteBatch.Draw(texture,
                auxScreen,
                (Rectangle?)null,
                Color.White,
                0,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                depth);

            if (Globals.ShowBounds)
                SpriteBatch.Draw(pixel,
                              Bounds,
                              (Rectangle?)null,
                              Color.Green,
                              0, // rotation
                              Vector2.Zero,
                              SpriteEffects.None,
                              1);
        }

        public void Reset(Texture2D tex, TileType type, AlignType align, int row)
        {
            initialized = true;
            texture = tex;
            Align = align;

            Reset(type,  row);
        }

        public void Reset(TileType type)
        {
            Reset(type, 7);
        }

        public void Reset(TileType type, int row)
        {
            initialized = true;

            Type = type;

            Position = Constants.TileStartPosition;
            Position.Y -= row;

            targetPosition = Constants.TileResetPosition;
            depth = 0.2f;

            switch (Align)
            {
                case AlignType.LEFT:
                    Position.X--;
                    targetPosition.X--;
                    depth = 0.1f;
                    break;
                case AlignType.RIGHT:
                    Position.X++;
                    targetPosition.X++;
                    depth = 0.3f;
                    break;
            }

            Position = Helpers.IsoPos(Position);
            targetPosition = Helpers.IsoPos(targetPosition);

            //Type = Globals.Random.Next(20) < 2 ? TileType.HOLE : TileType.FLOOR;

            rotation = Globals.Random.Next(360);
        }
    }
}