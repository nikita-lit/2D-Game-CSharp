using Game2D.Classes;

namespace Game2D.Entities
{
    public class Tree : Entity
    {
        public override EntityID EntityID => EntityID.Tree;

        private const float TILE_SIZE = 32f;
        private const float SIZE = 5f;
        private const float SPRITE_SIZE = TILE_SIZE * SIZE;

        private readonly Sprite _sprite;

        public Tree(Vector2 position) 
            : base(position) 
        {
            _sprite = new Sprite("../../assets/textures/tree.png");
            Collider = new RectCollider() {
                Rect = new Rectangle((int)Position.X, (int)Position.Y, 25, 60),
            };
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnDraw()
        {
            var textureOffset = new Vector2(
                (SPRITE_SIZE - RectCollider.Width) / 2f, 
                ((SPRITE_SIZE - RectCollider.Height) / 2f) + 50
            );

            Raylib.DrawTextureEx(_sprite.Texture, Position - textureOffset, 0.0f, SIZE, Color.White);
        }
    }
}
