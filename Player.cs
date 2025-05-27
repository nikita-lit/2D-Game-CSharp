

namespace Game2D
{
    public class Player(Vector2 position) : Entity(position)
    {
        public override void Update()
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

        public override void Draw()
        {
            Raylib.DrawRectangle((int)Position.X, (int)Position.Y, 25, 25, Color.Brown);
            Raylib.DrawText("Player", (int)Position.X-(25/2), (int)Position.Y-20, 16, Color.White);
        }
    }
}
