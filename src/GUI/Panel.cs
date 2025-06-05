using Game2D.Interfaces;

namespace Game2D.Gui
{
    public class Panel : IHoverable
    {
        public Panel Parent;
        public List<Panel> Children { get; private set; } = new();
        public int ZIndex { get; set; } = 0;

        public Rectangle Rect;
        public Vector2 RectCenter => Rect.Position + new Vector2(Rect.Width/2, Rect.Height/2);

        private Vector2 _basePosition;
        private Vector2 _baseSize;

        public bool IsEnabled = true;
        public virtual bool IsInteractive { get; set; } = true;

        public Action<Panel> CustomDraw;

        public virtual MouseCursor Cursor { get; set; } = MouseCursor.Default;
        public bool IsHovered() => 
            IsEnabled 
            && IsInteractive 
            && !Raylib.IsCursorHidden() 
            && Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Rect);

        public Vector2 Position
        {
            get
            {
                var basePos = new Vector2(GUI.SS(_basePosition.X), GUI.SSY(_basePosition.Y));
                if (Parent != null)
                    basePos += Parent.Position;
                return basePos;
            }
            set
            {
                if (Parent != null)
                    value -= Parent.Position;
                _basePosition = new Vector2(value.X / GUI.ScaleX(), value.Y / GUI.ScaleY());
            }
        }

        public Vector2 Size
        {
            get => new Vector2(GUI.SS(_baseSize.X), GUI.SSY(_baseSize.Y));
            set => _baseSize = new Vector2(value.X / GUI.ScaleX(), value.Y / GUI.ScaleY());
        }

        public Panel(Panel parent, float width, float height, Action<Panel> onDraw = null)
        {
            Parent = parent;

            _baseSize = new Vector2(width, height);
            _basePosition = Vector2.Zero;

            Rect = new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
            CustomDraw = onDraw;

            Program.Hoverables.Add(this);
            GUI.Panels.Add(this);
            Parent?.AddChild(this);
        }

        ~Panel() { Destroy(); }

        public void AddChild(Panel panel)
        {
            Children.Add(panel);
            panel.ZIndex += ZIndex + 1;
        }

        public void Update()
        {
            if (!IsEnabled) return;

            Rect.Position = Position;
            Rect.Size = Size;
            OnUpdate();
        }

        public void Draw()
        {
            if (!IsEnabled) return;

            if (CustomDraw == null)
                OnDraw();
            else
                CustomDraw(this);
        }

        public void Destroy()
        {
            foreach (var child in Children)
                child.Destroy();

            OnDestroy();
            GUI.Panels.Remove(this);
            Program.Hoverables.Remove(this);
            Parent?.Children.Remove(this);
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDraw() { }
        protected virtual void OnDestroy() { }
        public virtual void OnScreenResize(Vector2 oldSize, Vector2 newSize) { }
    }
}
