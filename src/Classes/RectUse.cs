using Game2D.Entities;
using Game2D.Interfaces;
using System;

namespace Game2D.Classes
{
    public class RectUse : IHoverable
    {
        public Vector2 Position;
        public Rectangle Rect;
        public Action<Entity> OnUse;
        public Func<bool> CanUseFunc;
        public bool CanUse() => CanUseFunc != null && CanUseFunc();

        public bool IsHovered() => CanUse() && Raylib.CheckCollisionPointRec(Program.GetMouseWorldPos(), Rect);
        public MouseCursor Cursor { get; set; } = MouseCursor.PointingHand;

        public RectUse(Rectangle rect, Func<bool> canUse, Action<Entity> onUse, MouseCursor cursor = MouseCursor.PointingHand)
        {
            Rect = rect; 
            OnUse = onUse;
            Cursor = cursor;
            CanUseFunc = canUse;
            Program.Hoverables.Add(this);
        }

        public void Update()
        {
            Rect.Position = Position - new Vector2(Rect.Width/2, Rect.Height/2);

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
