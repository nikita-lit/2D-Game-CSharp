using System.ComponentModel;
using System.IO;

namespace Game2D
{
    public class Player : Entity
    {
        private const float _SPEED = 150f;
        private Texture2D _texture;

        public Player(Vector2 position) : base(position)
        {
            Position = position;
            ID = Guid.NewGuid();
            All.Add(ID, this);

            string path = Path.Combine(AppContext.BaseDirectory, "..", "..", "assets/");
            _texture = Raylib.LoadTexture(path + "textures/player.png");
        }

        public override void Update()
        {
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
            //Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 50, 50, Color.Brown);
        }
    }
}
