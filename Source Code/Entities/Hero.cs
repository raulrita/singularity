namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    class Hero : Actor
    {
        KeyboardState old;
        bool dead;

        public Hero()
            : base(new Vector2(0, 1), "hero")
        {
            dead = false;
        }

        public override void Update(float delta)
        {
            if (!dead && !moving)
            {
                if (depth != -1 && Helpers.IsKeyUp(Keys.Left))
                {
                    depth--;
                    Move(new Vector2(-1, 0));
                }
                else if (depth != 1 && Helpers.IsKeyUp(Keys.Right))
                {
                    depth++;
                    Move(new Vector2(1, 0));
                }
              
                old = Keyboard.GetState();
            }

            base.Update(delta);

            //Debug.WriteLine(Constants.DisplayWidth / 2 + " " + screenPosition.X);

            if (dead)
                color *= .95f;

            if (color == Color.Transparent)
            {
                Debug.WriteLine("restart");
                //throw new Exception("over");
                Globals.NextScene = typeof(Over);
            }

            if (! dead)
                Logic();
        }   

        private void Logic()
        {
            // holes
            // enemies            
            List<Entity> holes = EntityManager.List(typeof(Tile));
            foreach (Entity entity in holes)
            {
                Tile hole = (Tile)entity;
                if (hole.Type != TileType.HOLE)
                    continue;

                if (Rectangle.Intersect(Bounds, hole.Bounds) != Rectangle.Empty)
                    dead = true;
            }

            List<Entity> enemies = EntityManager.List(typeof(Enemy1));
            Kill(enemies);

            enemies = EntityManager.List(typeof(Enemy2));
            Kill(enemies);

            enemies = EntityManager.List(typeof(Enemy3));
            Kill(enemies);

            enemies = EntityManager.List(typeof(Asteroid));
            Kill(enemies);
        }

        private void Kill(List<Entity> enemies)
        {
            foreach (Entity entity in enemies)
            {
                Actor enemy = (Actor)entity;

                if (Rectangle.Intersect(Bounds, enemy.Bounds) != Rectangle.Empty)
                    dead = true;
            }
        }
    }
}
