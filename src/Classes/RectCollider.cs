namespace Game2D.Classes
{
    public class RectCollider : Collider
    {
        public Rectangle Rect;
        public float Width => Rect.Width;
        public float Height => Rect.Height;
        public Vector2 HalfRect => new Vector2(Width / 2, Height / 2);
        public Vector2 RectCenter => Rect.Position - new Vector2(Width / 2, Height / 2);

        public override bool CheckCollision(Collider other) 
        { 
            if (!IsActive) return false;

            if(other == this) 
                return false;

            var collRect = (other as RectCollider);
            if (collRect != null)
                return Raylib.CheckCollisionRecs(Rect, collRect.Rect);

            return false; 
        }
    }
}
