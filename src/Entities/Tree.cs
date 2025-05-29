namespace Game2D.Entities
{
    public class Tree : Entity
    {
        private const float TILE_SIZE = 32f;
        private const float SIZE = 5f;
        private const float SPRITE_SIZE = TILE_SIZE * SIZE;

        private Sprite _sprite;

        public Tree(Vector2 position) : base(position) 
        {
            _sprite = new Sprite("../../assets/textures/tree.png");
            Rect = new Rectangle((int)Position.X, (int)Position.Y, 25, 60);
        }

        public override void Update()
        {
            Rect.Position = Position;
        }

        public override void Draw()
        {
            var textureOffset = new Vector2(
                (SPRITE_SIZE - Rect.Width) / 2f, 
                ((SPRITE_SIZE - Rect.Height) / 2f) + 50
            );

            Raylib.DrawTextureEx(_sprite.Texture, Position - textureOffset, 0.0f, SIZE, Color.White);
        }
    }
}
