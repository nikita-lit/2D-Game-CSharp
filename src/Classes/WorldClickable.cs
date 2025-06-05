using Game2D.Entities;
using Game2D.Interfaces;

namespace Game2D.Classes
{
    public class WorldClickable : IHoverable
    {
        public Vector2 Position;
        public Vector2 Offset;
        public int ZIndex { get; set; } = 0;

        public bool UseCenter = true;
        public Rectangle Rect;
        public Vector2 RectCenter => Rect.Position + new Vector2(Rect.Width/2, Rect.Height/2);

        public Action<Entity> OnUse;
        public Func<bool> CanUseFunc;
        public bool CanUse() => CanUseFunc != null && CanUseFunc();
        public float UseDistance = 200.0f;

        public bool IsHovered() => CanUse() 
            && !Raylib.IsCursorHidden()
            && Raylib.CheckCollisionPointRec(Program.GetMouseWorldPos(), Rect) 
            && (Program.Player.Position - Position).Length() < UseDistance;

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
                Rect.Position = Position - new Vector2(Rect.Width/2, Rect.Height/2) + Offset;
            else
                Rect.Position = Position + Offset;

            if (!CanUse() || Program.Focus != this)
                return;

            if (IsHovered() && Raylib.IsMouseButtonPressed(MouseButton.Left))
                OnUse?.Invoke(Program.Player);
        }

        public void Draw()
        {
            //Raylib.DrawRectangleLinesEx(Rect, 1.0f, (CanUse() ? Color.Lime : Color.Red));

            if(IsHovered())
                Render.Draw.CornerHighlights(Rect, 1, 8);
        }
    }
}
