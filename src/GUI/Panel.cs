using Game2D.Interfaces;

namespace Game2D.Gui
{
    public class Panel : IHoverable
    {
        public MouseCursor Cursor { get; set; } = MouseCursor.Default;

        public Panel()
        {
            GUI.Panels.Add(this);
            Program.Hoverables.Add(this);
        }

        public virtual void Update() { }
        public virtual void Draw() { }
        public virtual bool IsHovered() { return false; }

        public virtual void Destroy()
        {
            GUI.Panels.Remove(this);
        }
    }
}
