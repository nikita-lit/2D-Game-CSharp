using Game2D.Environment;
using Game2D.Classes;

namespace Game2D.Entities
{
    public enum EntityID
    {
        None,
        Player,
        Tree,
        Campfire,
        HeatSource,
    }

    public class Entity
    {
        public virtual EntityID EntityID => EntityID.None;

        public World World { get; set; }
        public Entity Parent { get; set; }

        public Collider Collider;
        public RectCollider RectCollider => Collider as RectCollider;

        public Vector2 Position;
        public Vector2 Velocity;
        public Guid ID;

        public Entity(Vector2 position)
        {
            World = Program.World;
            Position = position;
            ID = Guid.NewGuid();
            Program.World.Entities.Add(ID, this);
        }

        ~Entity() { Destroy(); }

        public virtual void Update() { }
        public virtual void Draw() { }
        
        public void Destroy()
        {
            Program.World.Entities.Remove(ID);
        }
    }
}
