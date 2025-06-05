namespace Game2D.Classes
{
    public class Collider
    {
        public bool IsActive;
        public bool IsStatic;
        public virtual bool CheckCollision(Collider other) { return false; }
    }
}
