using Game2D.Classes;

namespace Game2D.Entities
{
    public class Tree : Entity
    {
        public override EntityID EntityID => EntityID.Tree;

        private readonly Sprite _sprite;

        public Tree(Vector2 position) 
            : base(position) 
        {
            _sprite = new Sprite("../../assets/textures/tree.png");
            _sprite.Size = 2.5f;

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
            _sprite.Draw(Position-new Vector2(0, 25));
        }
    }
}
