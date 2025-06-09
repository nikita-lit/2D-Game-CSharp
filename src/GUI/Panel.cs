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

        public bool IsEnabled = true;
        public virtual bool IsInteractive { get; set; } = true;

        public Action<Panel> CustomDraw;

        public virtual MouseCursor Cursor { get; set; } = MouseCursor.Default;
        public bool IsHovered() => 
            IsEnabled 
            && IsInteractive 
            && !Raylib.IsCursorHidden() 
            && Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Rect);

        private Vector2 _basePosition;
        public Vector2 BasePosition 
        { 
            get => _basePosition;
            set
            {
                _basePosition = value;
                Position = new Vector2(GUI.SS(_basePosition.X), GUI.SSY(_basePosition.Y));
                Rect.X = Position.X; Rect.Y = Position.Y;
            }
        }

        private Vector2 _position;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                Rect.X = Position.X; Rect.Y = Position.Y;
            }
        }

        private Vector2 _baseSize;
        public Vector2 BaseSize
        {
            get => _baseSize;
            set
            {
                _baseSize = value;
                Size = new Vector2(GUI.SS(_baseSize.X), GUI.SSY(_baseSize.Y));
                Rect.Width = Size.X; Rect.Height = Size.Y;
            }
        }

        private Vector2 _size;
        public Vector2 Size
        {
            get => _size;
            set
            {
                _size = value;
                Rect.Width = Size.X; Rect.Height = Size.Y;
            }
        }

        public Panel(Panel parent, float width, float height, Action<Panel> onDraw = null)
        {
            Parent = parent;

            BaseSize = new Vector2(width, height);
            BasePosition = Vector2.Zero;

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
            foreach (var child in Children.ToList())
                child.Destroy();

            OnDestroy();
            GUI.Panels.Remove(this);
            Program.Hoverables.Remove(this);
            Parent?.Children.Remove(this);
        }

        protected virtual void OnUpdate() { }
        protected virtual void OnDraw() { }
        protected virtual void OnDestroy() { }
        public virtual void OnScreenResize(Vector2 oldSize, Vector2 newSize) 
        {
            Position = new Vector2(GUI.SS(_basePosition.X), GUI.SSY(_basePosition.Y));
            Size = new Vector2(GUI.SS(_baseSize.X), GUI.SSY(_baseSize.Y));

            Rect.X = Position.X;
            Rect.Y = Position.Y;
            Rect.Width = Size.X;
            Rect.Height = Size.Y;
        }
    }
}
