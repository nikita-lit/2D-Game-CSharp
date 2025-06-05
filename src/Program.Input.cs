using Game2D.Interfaces;

namespace Game2D
{
    public partial class Program
    {
        public static List<IHoverable> Hoverables = new();
        public static IHoverable Focus { get; private set; }

        public static void UpdateInput()
        {
            if(Raylib.IsKeyPressed(KeyboardKey.F3))
            {
                if(Raylib.IsCursorHidden())
                    Raylib.EnableCursor();
                else
                    Raylib.DisableCursor();
            }

            if (Raylib.IsCursorHidden())
            {
                Raylib.SetMousePosition((int)ScreenSize.X / 2, (int)ScreenSize.Y / 2);
                return;
            }

            Focus = null;

            var sorted = Hoverables.OrderBy(h => h.ZIndex);
            foreach (var hoverable in sorted.Reverse())
            {
                if (hoverable.IsHovered())
                {
                    Focus = hoverable;
                    Raylib.SetMouseCursor(hoverable.Cursor);
                    return;
                }
            }

            Raylib.SetMouseCursor(MouseCursor.Default);
        }
    }
}
