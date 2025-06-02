using Game2D.Entities;

namespace Game2D.Classes
{
    public abstract class Zone
    {
        public Vector2 Center;
        public bool IsEnabled = true;

        protected readonly HashSet<Entity> _current = new();
        public IReadOnlyCollection<Entity> EntitiesInside => _current;

        public Action<Entity> OnEnter;
        public Action<Entity> OnExit;
        public Func<Entity, bool> Filter;

        public Zone(Vector2 center,
            Func<Entity, bool> filter,
            Action<Entity> onEnter,
            Action<Entity> onExit)
        {
            Center = center;
            Filter = filter;
            OnEnter = onEnter;
            OnExit = onExit;
        }

        public virtual void Update(IEnumerable<Entity> allEntities)
        {
            if (!IsEnabled)
            {
                foreach (var entity in _current)
                    OnExit?.Invoke(entity);

                _current.Clear();
                return;
            }

            HashSet<Entity> detected = new();

            foreach (var entity in allEntities)
            {
                Detection(detected, entity);
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

        public virtual void Detection(HashSet<Entity> detected, Entity entity) => throw new NotImplementedException();
        public virtual void Draw() {}

        public void Disable()
        {
            if (!IsEnabled) return;

            foreach (var entity in _current)
                OnExit?.Invoke(entity);

            _current.Clear();
            IsEnabled = false;
        }

        public void Enable()
        {
            if (IsEnabled) return;

            _current.Clear();
            IsEnabled = true;
        }
    }
}
