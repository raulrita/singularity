namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System.Collections.Generic;

    abstract class Actor : Entity
    {
        public Vector2 Position;
        public Vector2 ScreenPosition;

        public float ActorDepth = 1.0f;

        protected int depth = 0;

        protected bool moving;                
        protected Color color = Color.White;

        Texture2D shadow;

        List<Texture2D> textures;
        List<int> indexes;

        Vector2 start;
        Vector2 target;
        
        float moveTime;

        float offsetX;
        float offsetY;

        int index;
        float animTime;
        const int frameCount = 4;

        float scale = 1.0f;        

        Vector2 pivot;

        Vector2 shadowOffset = new Vector2(-110, 10);

        public Rectangle Bounds = new Rectangle();
        private Texture2D pixel;

        public Actor(Vector2 origin, string filename)
        {
            shadow = ContentManager.Load<Texture2D>("shadow");
            pixel = ContentManager.Load<Texture2D>("pixel");

            textures = new List<Texture2D>();
            indexes = new List<int>();

            for (int h = 0; h < frameCount; h++)
            {
                string fullFilename = string.Format(filename + "\\frame{0}", h);
                Texture2D texture = ContentManager.Load<Texture2D>(fullFilename);

                textures.Add(texture);
                indexes.Add(h);
            }

            pivot = new Vector2(
                (float)textures[0].Width / 2,
                (float)textures[0].Height / 2);

            offsetX = -(float)textures[0].Width / 2;
            offsetY = -(float)textures[0].Height;

            for (int h = frameCount - 2; h > 0; h--)
                indexes.Add(h);
                
            Position = origin;
            ScreenPosition = CalcScreenPosition(origin);
            Name = filename;
            animTime = 0;
            moving = false;
        }

        public override void Update(float delta)
        {
            if (!Globals.Scrolling)
                return;

            Move(delta);

            animTime += delta * Constants.FrameTime;

            if (animTime > Constants.AnimFrameTime)
            {
                animTime = 0.0f;
                index++;

                if (index >= indexes.Count)
                    index = 0;
            }

            Vector2 dissolve = Constants.TileResetPosition;
            dissolve.Y -= 2;
            dissolve.X = depth;
            dissolve = CalcScreenPosition(dissolve);
            if (ScreenPosition.Y > dissolve.Y)
                color *= 0.95f;

            Vector2 end = Constants.TileResetPosition;
            end.Y--;
            end.X = depth;
            end = CalcScreenPosition(end);
            if (ScreenPosition.Y > end.Y)
                EntityManager.Remove(this);

            Bounds.X = (int)(ScreenPosition.X - Constants.TileHalfWidth / 4);
            Bounds.Y = (int)(ScreenPosition.Y + Constants.TileHalfHeight / 4);

            Bounds.Width = Constants.TileHalfWidth / 2;
            Bounds.Height = Constants.TileHalfHeight;
        }

        public override void Draw()
        {
            float d = depth == -1 ? .7f : ((depth == 0) ? .8f : .9f);

            if (this.GetType() == typeof(Hero))
                d += .01f;

            ActorDepth = d;

            SpriteBatch.Draw(shadow,
                ScreenPosition + shadowOffset,
                (Rectangle?)null,
                color,
                0,
                Vector2.Zero,
                scale,
                SpriteEffects.None,
                d - .05f);

            SpriteBatch.Draw(textures[indexes[index]], 
                ScreenPosition, 
                (Rectangle?)null,
                color,
                0,
                pivot, 
                scale, 
                SpriteEffects.None, 
                d);

            if (Globals.ShowBounds)
            SpriteBatch.Draw(pixel,
                          Bounds,
                          (Rectangle?)null,
                          Color.Gray,
                          0, // rotation
                          Vector2.Zero,
                          SpriteEffects.None,
                          1);
        }

        protected Vector2 CalcScreenPosition(Vector2 pos)
        {            
            pos = Helpers.IsoPos(pos);
            
            pos.X -= Constants.TileHalfWidth;
            pos.Y -= Constants.TileHalfHeight;

            pos.X -= offsetX;
            pos.Y -= offsetY;

            return pos;
        }

        protected void Move(float delta)
        {
            if (!moving)
                return;

            moveTime += Constants.FrameTime * delta;

            float percentage = moveTime / Constants.MoveTime;

            Vector2 startScreen = CalcScreenPosition(start);
            Vector2 targetScreen = CalcScreenPosition(target);

            if (percentage < 1.0f)
            {
                ScreenPosition.X = MathHelper.Lerp(
                    startScreen.X,
                    targetScreen.X,
                    percentage);

                ScreenPosition.Y = MathHelper.Lerp(
                    startScreen.Y,
                    targetScreen.Y,
                    percentage);
            }
            else
            {
                ScreenPosition = targetScreen;
                Position = target;
                moving = false;
            }
        }

        protected void Move(Vector2 direction)
        {
            start = target = Position;
            moveTime = 0.0f;
            target += direction;
            moving = true;
        }
    }
}
