using Game2D.Interfaces;

namespace Game2D.Gui
{
    public class Panel : IHoverable
    {
        public Rectangle Rect;

        private Vector2 _basePosition;
        private Vector2 _baseSize;

        public bool IsEnabled = true;
        public Action<Panel> CustomDraw;

        public MouseCursor Cursor { get; set; } = MouseCursor.PointingHand;
        public bool IsHovered() => Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Rect);

        public Vector2 Position
        {
            get => new Vector2(GUI.SS(_basePosition.X), GUI.SSY(_basePosition.Y));
            set => _basePosition = new Vector2(value.X / GUI.ScaleX(), value.Y / GUI.ScaleY());
        }

        public Vector2 Size
        {
            get => new Vector2(GUI.SS(_baseSize.X), GUI.SSY(_baseSize.Y));
            set => _baseSize = new Vector2(value.X / GUI.ScaleX(), value.Y / GUI.ScaleY());
        }

        public Panel(float width, float height, Action<Panel> onDraw = null)
        {
            _baseSize = new Vector2(width, height);
            _basePosition = Vector2.Zero;

            Rect = new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
            CustomDraw = onDraw;

            Program.Hoverables.Add(this);
            GUI.Panels.Add(this);
        }

        public void Update()
        {
            Rect.Position = Position;
            Rect.Size = Size;
            OnUpdate();
        }

        public void Draw()
        {
            if (CustomDraw == null)
                OnDraw();
            else
                CustomDraw(this);
        }

        public void Destroy()
        {
            OnDestroy();
            GUI.Panels.Remove(this);
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDraw() { }
        protected virtual void OnDestroy() { }
        public virtual void OnScreenResize(Vector2 oldSize, Vector2 newSize) { }
    }
}
