using Game2D.Interfaces;

namespace Game2D
{
    public partial class Program
    {
        public static List<IHoverable> Hoverables = new();

        public static void UpdateInput()
        {
            foreach (var hoverable in Hoverables)
            {
                if (hoverable.IsHovered())
                {
                    Raylib.SetMouseCursor(hoverable.Cursor);
                    return;
                }
            }

            Raylib.SetMouseCursor(MouseCursor.Default);
        }
    }
}
