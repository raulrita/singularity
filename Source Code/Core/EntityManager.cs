namespace Neuro
{
    using Microsoft.Xna.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    class EntityManager
    {
        private List<Entity> entities = new List<Entity>();

        // List...
            //- by type
        // Key unique

        public Entity Find(string name)
        {
            return entities.FirstOrDefault(m => m.Name == name);
        }

        public List<Entity> List(Type type)
        {
            return entities.Where(m => m.GetType() == type).ToList();
        }

        public void Add(Entity entity)
        {
            entities.Add(entity);
        }

        public void Add(string name, Entity entity)
        {
            entity.Name = name;
            Add(entity);
        }

        public void Remove(Entity entity)
        {
            if (entities.Contains(entity))
                entity.Done = true;
        }

        public void Remove(string name)
        {
            Entity entity = Find(name);

            if (entity != null)
                entity.Done = true;
        }

        public void Remove(Type type)
        {
            entities.RemoveAll(m => m.GetType() == type);
        }

        public void Clear()
        {
            entities.Clear();
        }

        public void Update(float delta)
        {
            entities.RemoveAll(m => m.Done);

            try
            {
                foreach (Entity entity in entities)
                    entity.Update(delta);
            }
            catch (Exception) { }
        }

        public void Draw()
        {
            foreach (Entity entity in entities)
                entity.Draw();
        }
    }
}
