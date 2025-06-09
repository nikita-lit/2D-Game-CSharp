namespace Game2D.Interfaces
{
    public interface IHealth
    {
        public int Health { get; }
        public int MaxHealth { get; }
        public void TakeDamage(int damage);
    }
}
