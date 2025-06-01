namespace Game2D.Gui
{
    public class Panel
    {
        public Panel()
        {
            GUI.Panels.Add(this);
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
