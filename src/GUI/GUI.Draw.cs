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
            int fontSize,
            Color color,
            HorizontalAlign hAlign = HorizontalAlign.Left,
            VerticalAlign vAlign = VerticalAlign.Top)
        {
            int scaledFontSize = (int)(fontSize * ScaleY());
            int textWidth = Raylib.MeasureText(text, scaledFontSize);
            int textHeight = scaledFontSize;

            float x = position.X;
            float y = position.Y;

            switch (hAlign)
            {
                case HorizontalAlign.Center:
                    x -= textWidth / 2f;
                    break;
                case HorizontalAlign.Right:
                    x -= textWidth;
                    break;
                case HorizontalAlign.Left:
                    break;
            }

            switch (vAlign)
            {
                case VerticalAlign.Center:
                    y -= textHeight / 2f;
                    break;
                case VerticalAlign.Bottom:
                    y -= textHeight;
                    break;
                case VerticalAlign.Top:
                    break;
            }

            Raylib.DrawText(text, (int)x, (int)y, scaledFontSize, color);
        }

    }
}
