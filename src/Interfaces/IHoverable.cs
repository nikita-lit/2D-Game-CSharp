namespace Game2D.Interfaces
{
    public interface IHoverable
    {
        public MouseCursor Cursor { get; set; }
        public bool IsHovered();
    }
}
