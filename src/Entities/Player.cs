namespace Game2D.Entities
{
    public class Player : Entity
    {
        private const float SIZE = 5.0f;
        private const float SPEED = 230f;

        private Sprite _sprite;

        public Player(Vector2 position) : base(position)
        {
            _sprite = new Sprite("../../assets/textures/player.png");
            Rect = new Rectangle(0, 0, 50, 50);
        }

        public override void Update()
        {
            Rect.Position = Position - new Vector2(Rect.Width/2, Rect.Height/2);

            if (Raylib.IsKeyDown(KeyboardKey.W))
                Position.Y -= SPEED * Raylib.GetFrameTime();
            else if(Raylib.IsKeyDown(KeyboardKey.S))
                Position.Y += SPEED * Raylib.GetFrameTime();

            if (Raylib.IsKeyDown(KeyboardKey.D))
                Position.X += SPEED * Raylib.GetFrameTime();
            else if (Raylib.IsKeyDown(KeyboardKey.A))
                Position.X -= SPEED * Raylib.GetFrameTime();
        }

        public override void Draw()
        {
            var texturOffset = new Vector2(
                (_sprite.Width * SIZE) / 2, 
                (_sprite.Height * SIZE) / 2
            );

            Raylib.DrawTextureEx(_sprite.Texture, Position- texturOffset, 0.0f, SIZE, Color.White);
        }
    }
}
