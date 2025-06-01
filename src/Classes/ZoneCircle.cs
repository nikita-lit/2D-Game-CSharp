using Game2D.Entities;

namespace Game2D.Classes
{
    public class ZoneCircle
    {
        public float Radius = 0.0f;
        public Vector2 Center;

        private readonly HashSet<Entity> _current = new();
        public IReadOnlyCollection<Entity> EntitiesInside => _current;

        public Action<Entity> OnEnter;
        public Action<Entity> OnExit;
        public Func<Entity, bool> Filter;

        public ZoneCircle(float radius, Vector2 center,
            Func<Entity, bool> filter,
            Action<Entity> onEnter,
            Action<Entity> onExit)
        {
            Radius = radius;
            Center = center;
            Filter = filter;
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public void Update(IEnumerable<Entity> allEntities)
        {
            float radiusSq = Radius * Radius;
            HashSet<Entity> detected = new();

            foreach (var entity in allEntities)
            {
                if (!Filter(entity)) 
                    continue;

                Vector2 diff = entity.Position - Center;
                if (diff.LengthSquared() <= radiusSq)
                {
                    detected.Add(entity);
                    if (_current.Add(entity))
                        OnEnter?.Invoke(entity);
                }
            }

            foreach (var entity in _current)
            {
                if (!detected.Contains(entity))
                    OnExit?.Invoke(entity);
            }

            _current.Clear();
            foreach (var entity in detected)
                _current.Add(entity);
        }

        public void Draw()
        {
            Raylib.DrawCircleLines((int)Center.X, (int)Center.Y, Radius, Color.White);
        }
    }
}
