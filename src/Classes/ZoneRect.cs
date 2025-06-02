using Game2D.Entities;

namespace Game2D.Classes
{
    public class ZoneRect : Zone
    {
        public Rectangle Rect;
        public bool UsePosition;

        public ZoneRect(Vector2 center, Rectangle rect,
            Func<Entity, bool> filter,
            Action<Entity> onEnter,
            Action<Entity> onExit)
            : base(center, filter, onEnter, onExit)
        {
            Rect = rect;
        }

        public override void Update(IEnumerable<Entity> allEntities)
        {
            Rect.Position = Center - new Vector2(Rect.Width/2, Rect.Height/2);
            base.Update(allEntities);
        }

        public override void Detection(HashSet<Entity> detected, Entity entity)
        {
            if (Filter != null && !Filter(entity))
                return;

            if (entity.RectCollider == null)
                return;

            bool isDetected;
            if (UsePosition)
                isDetected = Raylib.CheckCollisionPointRec(entity.Position, Rect);
            else
                isDetected = Raylib.CheckCollisionRecs(Rect, (entity.RectCollider.Rect));

            if (isDetected)
            {
                detected.Add(entity);
                if (_current.Add(entity))
                    OnEnter?.Invoke(entity);
            }
        }

        public override void Draw()
        {
            Raylib.DrawRectangleLinesEx(Rect, 1.0f, Color.White);
        }
    }
}
