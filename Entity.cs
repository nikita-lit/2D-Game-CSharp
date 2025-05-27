namespace Game2D
{
    public class Entity(Vector2 position)
    {
        public Vector2 Position = position;

        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
