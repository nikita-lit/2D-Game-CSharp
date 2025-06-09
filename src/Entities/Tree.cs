using Game2D.Classes;
using Game2D.Interfaces;
using Game2D.Render;

namespace Game2D.Entities
{
    public class Tree : Entity, IHealth
    {
        public override EntityID EntityID => EntityID.Tree;

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        private readonly Sprite _sprite;

        public Tree(Vector2 position) 
            : base(position) 
        {
            MaxHealth = 100;
            Health = MaxHealth;

            _sprite = new Sprite("tree.png");
            _sprite.Size = 4.0f;

            Collider = new RectCollider() {
                Rect = new Rectangle((int)Position.X, (int)Position.Y, 20, 60),
                IsStatic = true
            };
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnDraw()
        {
            _sprite.Draw(Position - new Vector2(0, 25));
            Render.Draw.AlignedText(Health.ToString(), Position, "Pixel", 16,
                Color.White, scaleFont: false, hAlign: HorizontalAlign.Center, vAlign: VerticalAlign.Center);
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            Health = Math.Clamp(Health, 0, MaxHealth);
        }
    }
}
