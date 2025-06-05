using Game2D.Assets;
using Game2D.Gui;

namespace Game2D.Render
{
    public enum HorizontalAlign
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlign
    {
        Top,
        Center,
        Bottom
    }

    public static class Draw
    {
        public static void CornerHighlights(Rectangle rect, int width, int height)
        {
            Raylib.DrawRectangle((int)(rect.Position.X), (int)rect.Position.Y, width, height, Color.White);
            Raylib.DrawRectangle((int)(rect.Position.X), (int)rect.Position.Y, height, width, Color.White);

            Raylib.DrawRectangle((int)(rect.Position.X + rect.Width - width), (int)(rect.Position.Y), width, height, Color.White);
            Raylib.DrawRectangle((int)(rect.Position.X + rect.Width - height), (int)(rect.Position.Y), height, width, Color.White);

            Raylib.DrawRectangle((int)(rect.Position.X + rect.Width - width), (int)(rect.Position.Y + rect.Height - height), width, height, Color.White);
            Raylib.DrawRectangle((int)(rect.Position.X + rect.Width - height), (int)(rect.Position.Y + rect.Height - width), height, width, Color.White);

            Raylib.DrawRectangle((int)(rect.Position.X), (int)(rect.Position.Y + rect.Height - height), width, height, Color.White);
            Raylib.DrawRectangle((int)(rect.Position.X), (int)(rect.Position.Y + rect.Height - width), height, width, Color.White);
        }

        public static void AlignedText(
            string text,
            Vector2 position,
            string fontName,
            int fontSize,
            Color color,
            float rotation = 0.0f,
            float spacing = 0.0f,
            HorizontalAlign hAlign = HorizontalAlign.Left,
            VerticalAlign vAlign = VerticalAlign.Top)
        {
            var font = AssetsSystem.GetFont(fontName);
            var scaledFontSize = (int)(fontSize * GUI.ScaleY());
            var textSize = Raylib.MeasureTextEx(font, text, scaledFontSize, spacing);
            var origin = Vector2.Zero;

            switch (hAlign)
            {
                case HorizontalAlign.Center:
                    origin.X = textSize.X / 2f;
                    break;
                case HorizontalAlign.Right:
                    origin.X = textSize.X;
                    break;
            }

            switch (vAlign)
            {
                case VerticalAlign.Center:
                    origin.Y = textSize.Y / 2f;
                    break;
                case VerticalAlign.Bottom:
                    origin.Y = textSize.Y;
                    break;
            }

            Raylib.DrawTextPro(font, text, position, origin, rotation, scaledFontSize, spacing, color);
        }
    }
}
