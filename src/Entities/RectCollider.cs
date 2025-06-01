namespace Game2D.Entities
{
    public class RectCollider : Collider
    {
        public Rectangle Rect;
        public float Width => Rect.Width;
        public float Height => Rect.Height;
        public Vector2 HalfRect => new Vector2(Width / 2, Height / 2);

        public override bool CheckCollision(Collider other) 
        { 
            if(other == this) 
                return true;

            var collRect = (other as RectCollider);
            if (collRect != null)
                return Raylib.CheckCollisionRecs(Rect, collRect.Rect);

            return false; 
        }
    }
}
