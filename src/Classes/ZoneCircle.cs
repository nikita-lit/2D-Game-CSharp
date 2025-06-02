using Game2D.Entities;

namespace Game2D.Classes
{
    public class ZoneCircle : Zone
    {
        public float Radius = 0.0f;

        public ZoneCircle(Vector2 center, float radius,
            Func<Entity, bool> filter, 
            Action<Entity> onEnter, 
            Action<Entity> onExit) 
            : base(center, filter, onEnter, onExit)
        {
            Radius = radius;
        }

        public override void Detection(HashSet<Entity> detected, Entity entity)
        {
            if (!Filter(entity))
                return;

            float radiusSq = Radius * Radius;
            Vector2 diff = entity.Position - Center;

            if (diff.LengthSquared() <= radiusSq)
            {
                detected.Add(entity);
                if (_current.Add(entity))
                    OnEnter?.Invoke(entity);
            }
        }

        public override void Draw() 
        {
            Raylib.DrawCircleLines((int)Center.X, (int)Center.Y, Radius, Color.White);
        }
    }
}
