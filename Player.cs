using System.Numerics;

namespace Game2D
{
    public class Player
    {
        public Vector2 Position;

        public void Update()
        {
            if (Raylib.IsKeyDown(KeyboardKey.W))
            {
                Position.Y -= 0.01f;
            }
            else if(Raylib.IsKeyDown(KeyboardKey.S))
            {
                Position.Y += 0.01f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.D))
            {
                Position.X += 0.01f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.A))
            {
                Position.X -= 0.01f;
            }
        }

        public void Draw()
        {
            Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 25, 25, Color.Brown);
        }
    }
}
