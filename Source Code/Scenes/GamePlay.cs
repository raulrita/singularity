namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class GamePlay : Scene
    {
        float time;
        int tick;
        int totalTicks = 100;

        List<Trigger> triggers;

        public GamePlay()
        {
            EntityManager.Add(new FrameCounter());

            EntityManager.Add(new Floor());            
            EntityManager.Add(new Hero());

            time = (float)Constants.TileHalfHeight / 2;
            tick = -2;
            
            LoadLevel();
        }

        public override void Update(float delta)
        {
            if (Constants.Debug && Helpers.IsKeyUp(Keys.P))
                Globals.Scrolling = !Globals.Scrolling;

            if (!Globals.Scrolling)
                return;

            if (Constants.Debug && Helpers.IsKeyUp(Keys.B))
                Globals.ShowBounds = !Globals.ShowBounds;

            if (!Globals.Scrolling)
                return;

            time += (float)Constants.TileHalfHeight * Globals.ScrollSpeed * delta;

            if (time > (float)Constants.TileHalfHeight / 2)
            {
                time = 0.0f;
                Tick();
            }
        }

        public override void Draw()
        {
            SpriteBatch.DrawString(Font, String.Format("Tick {0}", tick), new Vector2(100, 500), Color.Green);
        }

        private void LoadLevel()
        {
            string folder = Environment.CurrentDirectory;
            triggers = new List<Trigger>();

            string filename = String.Format("{0}/levels/{1}.lev", folder, Globals.Level);
            string[] contents = File.ReadLines(filename).ToArray();

            Globals.ScrollSpeed = Constants.DefaultScrollSpeed;

            foreach (String line in contents)
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                if (line.StartsWith("#"))
                    continue;

                string[] parts = line.Replace(" ", String.Empty).Split(';');

                if (line.StartsWith("s"))
                {
                    Globals.ScrollSpeed = float.Parse(parts[1]) / 10000.0f;
                    continue;
                }

                if (line.StartsWith("z"))
                {
                    totalTicks = int.Parse(parts[1]);
                    continue;
                }

                Trigger trig = new Trigger();

                trig.Tick = int.Parse(parts[0]);
                trig.Type = parts[1][0];
                trig.Value = int.Parse(parts[2]);

                triggers.Add(trig);
            }
        }

        private void Tick()
        {
            List<Trigger> tickTriggers = triggers.Where(m => m.Tick == tick).ToList();

            bool h0 = false;
            bool h1 = false;
            bool h2 = false;

            foreach (Trigger trigger in tickTriggers)
            {
                switch (trigger.Type)
                {
                    case 'e':
                        EntityManager.Add(new Enemy1(trigger.Value));
                        break;

                    case 'f':
                        EntityManager.Add(new Enemy2(trigger.Value));
                        break;

                    case 'g':
                        EntityManager.Add(new Enemy3(trigger.Value));
                        break;

                    case 'a':
                        EntityManager.Add(new Asteroid(trigger.Value));
                        break;

                    case 't':
                            EntityManager.Add(new Timer(trigger.Value));
                        break;

                    case 'h':
                        switch (trigger.Value)
                        {
                            case -1: h0 = true; break;
                            case 0: h1 = true; break;
                            case 1: h2 = true; break;
                        }
                        break;
                }
            }

            List<Entity> tiles = EntityManager.List(typeof(Tile));

            foreach (Entity entity in tiles)
            {
                Tile tile = (Tile)entity;
                
                if (tile.ToReset)
                {
                    bool tt = tile.Align == AlignType.LEFT ? h0 :
                        ((tile.Align == AlignType.CENTER) ? h1 : h2);

                    tile.Reset(tt ? TileType.HOLE : TileType.FLOOR);
                }
            }

            if (tick >= totalTicks)
                Globals.NextScene = typeof(LevelUp);

            tick++;
        }
    }
}
