namespace Game2D
{
    public class Entity
    {
        public Vector2 Position;
        public Guid ID;

        public static Dictionary<Guid, Entity> All = new();

        public Entity(Vector2 position)
        {
            Position = position;
            ID = Guid.NewGuid();
            All.Add(ID, this);
        }

        public virtual void Update() { }
        public virtual void Draw() { }
    }
}
