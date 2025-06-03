using Game2D.Classes;

namespace Game2D.Entities
{
    public class Player : Entity
    {
        public override EntityID EntityID => EntityID.Player;

        private const float SIZE = 5.0f;
        private const float SPEED = 300f;

        private readonly Sprite _sprite;

        public Player(Vector2 position) 
            : base(position)
        {
            _sprite = new Sprite("../../assets/textures/player.png");
            Collider = new RectCollider() {
                Rect = new Rectangle(0, 0, 50, 50),
            };
        }

        protected override void OnUpdate()
        {
            float dt = Raylib.GetFrameTime();
            Vector2 input = Vector2.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.W)) input.Y -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.S)) input.Y += 1;
            if (Raylib.IsKeyDown(KeyboardKey.D)) input.X += 1;
            if (Raylib.IsKeyDown(KeyboardKey.A)) input.X -= 1;

            if (input != Vector2.Zero)
            {
                input = Vector2.Normalize(input);
                float runFactor = 1.0f;
                if (Raylib.IsKeyDown(KeyboardKey.LeftShift))
                    runFactor = 1.5f;

                Velocity = input * SPEED * runFactor;
            }
            else
                Velocity = Vector2.Zero;

            Position += Velocity * dt;
        }

        protected override void OnDraw()
        {
            var texturOffset = new Vector2(
                (_sprite.Width * SIZE) / 2, 
                (_sprite.Height * SIZE) / 2
            );

            Raylib.DrawTextureEx(_sprite.Texture, Position- texturOffset, 0.0f, SIZE, Color.White);
        }
    }
}
