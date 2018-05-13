namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using Microsoft.Xna.Framework.Input;
    using System.Collections.Generic;

    class Burst : Entity
    {
        public Vector2 Position;

        private Texture2D texture;

        private Hero hero;
       
        private const int inc = 50;
       
        public Rectangle Bounds = new Rectangle();
        private Texture2D pixel;

        float depth = 1.0f;

        public Burst()
        {
            pixel = ContentManager.Load<Texture2D>("pixel");
            texture = ContentManager.Load<Texture2D>("burst");

            Position = new Vector2(Constants.DisplayWidth / 2, Constants.DisplayHeight / 2);
        }

        public override void Update(float delta)
        {
            if (this.Done)
                return;

            if (hero == null)
                hero = (Hero)EntityManager.Find("hero");
            else
            {
                Position = hero.ScreenPosition;// Helpers.IsoPos(hero.Position);
                depth = (float)hero.ActorDepth - .01f;
               // Position.Y -= Constants.TileHalfHeight;
            }

            Globals.Accuracy -= delta / 100;

            if (Globals.Accuracy <= 0)
            {
                Globals.Accuracy = 0.0f;
                this.Done = true;
                return;
            }

            List<Entity> enemies = EntityManager.List(typeof(Enemy1));
            KillEnemies(enemies);

            enemies = EntityManager.List(typeof(Enemy2));
            KillEnemies(enemies);

            enemies = EntityManager.List(typeof(Enemy3));
            KillEnemies(enemies);

            enemies = EntityManager.List(typeof(Asteroid));
            KillEnemies(enemies);

            base.Update(delta);
        }

        private void KillEnemies(List<Entity> enemies)
        {
            foreach (Entity entity in enemies)
            {
                Actor enemy = (Actor)entity;

                if (Rectangle.Intersect(Bounds, enemy.Bounds) != Rectangle.Empty)
                    enemy.Done = true;
            }
        }

        public override void Draw()
        {
            if (this.Done)
                return;            

            Rectangle destination = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);

            destination.X -= texture.Width / 2;
            destination.Y -= texture.Height / 2;

            DrawCircle(destination, Color.White);

            Color result = Color.White;
            
            DrawCircle(destination, Color.White);

            destination.X -= inc;
            destination.Y -= inc;
            destination.Width += inc * 2;
            destination.Height += inc * 2;
   
            DrawCircle(destination, Color.Blue);

            destination.X -= inc;
            destination.Y -= inc;
            destination.Width += inc * 2;
            destination.Height += inc * 2;

            DrawCircle(destination, Color.White);

            destination.X -= inc;
            destination.Y -= inc;
            destination.Width += inc * 2;
            destination.Height += inc * 2;

            DrawCircle(destination, Color.Blue);

            if (Globals.ShowBounds)
                SpriteBatch.Draw(pixel,
                              Bounds,
                              (Rectangle?)null,
                              Color.Black,
                              0, // rotation
                              Vector2.Zero,
                              SpriteEffects.None,
                              1);
        }

        private void DrawCircle(Rectangle destination, Color color)
        {
            // hammer time
            if (color == Color.Blue)
                Bounds = destination;

            SpriteBatch.Draw(texture,
                destination,
                (Rectangle?)null,
                color,
                0, // rotation
                Vector2.Zero,                
                SpriteEffects.None,
                depth);
        }
    }
}
