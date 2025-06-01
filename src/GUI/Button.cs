namespace Game2D.Gui
{
    public class Button : Panel
    {
        public Rectangle Rect;

        public Button() : base() 
        {
            Rect = new Rectangle(25, 25, 200, 100);
        }

        public override bool IsHovered() 
        {
            if(Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Rect))
                return true;

            return false; 
        }

        public override void Draw()
        {
            Raylib.DrawRectangleRec(Rect, Color.White);
        }
    }
}
