using Game2D.Assets;

namespace Game2D.Gui
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

    public static partial class GUI
    {
        public static void DrawAlignedText(
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
            int scaledFontSize = (int)(fontSize * ScaleY());
            int textWidth = Raylib.MeasureText(text, scaledFontSize);
            int textHeight = scaledFontSize;

            switch (hAlign)
            {
                case HorizontalAlign.Center:
                    position.X -= textWidth / 2f;
                    break;
                case HorizontalAlign.Right:
                    position.X -= textWidth;
                    break;
                case HorizontalAlign.Left:
                    break;
            }

            switch (vAlign)
            {
                case VerticalAlign.Center:
                    position.Y -= textHeight / 2f;
                    break;
                case VerticalAlign.Bottom:
                    position.Y -= textHeight;
                    break;
                case VerticalAlign.Top:
                    break;
            }

            Raylib.DrawTextPro(AssetsSystem.GetFont(fontName), text, position, Vector2.Zero, rotation, scaledFontSize, spacing, color);
        }

    }
}
