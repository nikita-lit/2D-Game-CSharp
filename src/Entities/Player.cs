namespace Game2D.Entities
{
    public class Player : Entity
    {
        private const float _SPEED = 230f;

        private Texture2D _texture;

        public Player() : base()
        {
            _texture = Raylib.LoadTexture("../../assets/textures/player.png");
            Rect = new Rectangle(0, 0, 60, 80);
        }

        public override void Update()
        {
            Rect.Position = Position - new Vector2(Rect.Width/2, Rect.Height/2);

            if (Raylib.IsKeyDown(KeyboardKey.W))
                Position.Y -= _SPEED * Raylib.GetFrameTime();
            else if(Raylib.IsKeyDown(KeyboardKey.S))
                Position.Y += _SPEED * Raylib.GetFrameTime();

            if (Raylib.IsKeyDown(KeyboardKey.D))
                Position.X += _SPEED * Raylib.GetFrameTime();
            else if (Raylib.IsKeyDown(KeyboardKey.A))
                Position.X -= _SPEED * Raylib.GetFrameTime();
        }

        public override void Draw()
        {
            Raylib.DrawTextureEx(_texture, Position-new Vector2((16f * 5.0f) / 2, (16f * 5.0f) / 2), 0.0f, 5.0f, Color.White);
        }
    }
}
