using Game2D.Environment;
using Game2D.Classes;

namespace Game2D.Entities
{
    public enum EntityID
    {
        None = 0,
        Player,
        Tree,
        Campfire,
        HeatSource,
        Item,
    }

    [Flags]
    public enum EntityFlag
    {
        NoDraw = 1,
        NotUsable = 2,
    }

    public class Entity
    {
        public virtual EntityID EntityID => EntityID.None;
        public EntityFlag Flags;

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

        public void Update() 
        {
            OnUpdate();
        }

        public void Draw()
        {
            if (!Flags.HasFlag(EntityFlag.NoDraw))
                OnDraw();

            if (Collider is RectCollider rectCollider)
            {
                var rect1 = rectCollider.Rect;
                Raylib.DrawRectangleLinesEx(rect1, 1.0f, Color.White);
            }
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDraw() { }

        public void Destroy()
        {
            Program.World.Entities.Remove(ID);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            Entity other = (Entity)obj;
            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return (ID).GetHashCode();
        }
    }
}
