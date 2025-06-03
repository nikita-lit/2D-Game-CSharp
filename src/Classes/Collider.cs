namespace Game2D.Classes
{
    public class Collider
    {
        public bool Active;
        public virtual bool CheckCollision(Collider other) { return false; }
    }
}
