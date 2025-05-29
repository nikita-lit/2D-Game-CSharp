namespace Game2D.Entities
{
    public class Tree : Entity
    {
        public Tree() : base() 
        {
            Rect = new Rectangle((int)Position.X, (int)Position.Y, 100, 100);
        }

        public override void Update()
        {
            Rect.Position = Position;
        }

        public override void Draw()
        {
            Raylib.DrawRectanglePro(Rect, Vector2.Zero, 0.0f, Color.Brown);
        }
    }
}
