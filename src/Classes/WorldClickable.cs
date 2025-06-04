using Game2D.Entities;
using Game2D.Interfaces;

namespace Game2D.Classes
{
    public class WorldClickable : IHoverable
    {
        public Vector2 Position;
        public bool UseCenter = true;
        public Rectangle Rect;
        public Action<Entity> OnUse;
        public Func<bool> CanUseFunc;
        public bool CanUse() => CanUseFunc != null && CanUseFunc();

        public bool IsHovered() => CanUse() && Raylib.CheckCollisionPointRec(Program.GetMouseWorldPos(), Rect);
        public MouseCursor Cursor { get; set; } = MouseCursor.PointingHand;

        public WorldClickable(Rectangle rect, 
            Func<bool> canUse, 
            Action<Entity> onUse,
            bool useCenter = true,
            MouseCursor cursor = MouseCursor.PointingHand)
        {
            Rect = rect; 
            OnUse = onUse;
            Cursor = cursor;
            CanUseFunc = canUse;
            UseCenter = useCenter;
            Program.Hoverables.Add(this);
        }

        public void Update()
        {
            if(UseCenter)
                Rect.Position = Position - new Vector2(Rect.Width/2, Rect.Height/2);
            else
                Rect.Position = Position;

            if (!CanUse())
                return;

            if (IsHovered() && Raylib.IsMouseButtonPressed(MouseButton.Left))
                OnUse?.Invoke(Program.Player);
        }

        public void Draw()
        {
            Raylib.DrawRectangleLinesEx(Rect, 1.0f, (CanUse() ? Color.Lime : Color.Red));
        }
    }
}
