namespace Neuro
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    abstract class Entity : IEntity
    {
        public bool Done { get; set; }

        public string Name { get; set; }

        protected ContentManager ContentManager { get { return Globals.ContentManager; } }

        protected GraphicsDevice GraphicsDevice { get { return Globals.GraphicsDevice; } }

        protected SpriteBatch SpriteBatch { get { return Globals.SpriteBatch; } }

        protected EntityManager EntityManager { get { return Globals.EntityManager; } }

        protected SpriteFont Font { get { return Globals.Font; } }

        public virtual void Update(float delta) { }

        public virtual void Draw() {  }
    }
}
