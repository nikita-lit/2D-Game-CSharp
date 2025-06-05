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
        Log,
        Axe,
    }

    [Flags]
    public enum EntityFlag
    {
        NoDraw = 1 << 0,
        NotUsable = 1 << 1,
        DontCollide = 1 << 2,
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
            if(Parent != null) 
                Position = Parent.Position;

            if(RectCollider != null)
                RectCollider.Rect.Position = Position - RectCollider.HalfRect;
               
            if(Collider != null)
                Collider.IsActive = !HasFlag(EntityFlag.DontCollide);

            OnUpdate();
        }

        public void Draw()
        {
            if (!Flags.HasFlag(EntityFlag.NoDraw))
                OnDraw();

            //if (Collider is RectCollider rectCollider)
            //    Raylib.DrawRectangleLinesEx(rectCollider.Rect, 1.0f, (rectCollider.IsActive ? Color.LightGray : Color.Gray));
        }

        public void Destroy()
        {
            OnDestroy();
            Program.World.Entities.Remove(ID);
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDraw() { }
        protected virtual void OnDestroy() { }

        public bool HasFlag(EntityFlag flag) => Flags.HasFlag(flag);
        public void AddFlag(EntityFlag flag) => Flags |= flag;
        public void RemoveFlag(EntityFlag flag) => Flags &= ~flag;

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Entity other = (Entity)obj;
            return ID == other.ID;
        }

        public override int GetHashCode()
        {
            return (ID).GetHashCode();
        }
    }
}
