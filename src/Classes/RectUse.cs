using Game2D.Entities;
using Game2D.Interfaces;

namespace Game2D.Classes
{
    public class RectUse : IHoverable
    {
        public Vector2 Position;
        public Rectangle Rect;
        public Action<Entity> OnUse;

        public bool IsHovered() => Raylib.CheckCollisionPointRec(Program.GetMouseWorldPos(), Rect);
        public MouseCursor Cursor { get; set; } = MouseCursor.PointingHand;

        public RectUse(Rectangle rect, Action<Entity> onUse, MouseCursor cursor = MouseCursor.PointingHand)
        { 
            Rect = rect; 
            OnUse = onUse;
            Cursor = cursor;
            Program.Hoverables.Add(this);
        }

        public void Update()
        {
            Rect.Position = Position - new Vector2(Rect.Width/2, Rect.Height/2);

            if (IsHovered() && Raylib.IsMouseButtonPressed(MouseButton.Left))
                OnUse?.Invoke(Program.Player);
        }

        public void Draw()
        {
            Raylib.DrawRectangleLinesEx(Rect, 1.0f, Color.Lime);
        }
    }
}
