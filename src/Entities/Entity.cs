namespace Game2D.Entities
{
    public class Entity
    {
        public Rectangle Rect; // for collision detection
        public Vector2 Position;
        public Guid ID;

        public static Dictionary<Guid, Entity> All = new();

        public Entity()
        {
            ID = Guid.NewGuid();
            All.Add(ID, this);
        }

        ~Entity() { Destroy(); }

        public virtual void Update() { }
        public virtual void Draw() { }
        
        public void Destroy()
        {
            All.Remove(ID);
        }
    }
}
