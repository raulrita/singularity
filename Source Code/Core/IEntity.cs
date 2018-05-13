namespace Neuro
{
    interface IEntity
    {
        string Name { get; set; }

        void Update(float delta);

        void Draw();
    }
}
