namespace Game2D.Gui
{
    public class Panel
    {
        public Rectangle Rect;

        private Vector2 _basePosition;
        private Vector2 _baseSize;

        public bool IsEnabled = true;
        public Action<Panel> CustomDraw;

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

        public Panel(float width, float height, Action<Panel> onDraw)
        {
            _baseSize = new Vector2(width, height);
            _basePosition = Vector2.Zero;

            Rect = new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
            CustomDraw = onDraw;

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

        public virtual bool IsHovered() { return false; }

        public virtual void OnScreenResize(Vector2 oldSize, Vector2 newSize)
        {
        }
    }

}
