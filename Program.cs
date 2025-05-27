global using Raylib_cs;
global using System.Numerics;

namespace Game2D
{
    class Program
    {
        public static Player Player;
        public static Camera Camera;

        public static void Main()
        {
            Raylib.InitWindow(1200, 800, "2D Game");
            Player = new(new Vector2(-25.0f/2.0f, -25.0f/2.0f));
            Camera = new(Vector2.Zero, new Vector2(Raylib.GetScreenWidth()/2f, Raylib.GetScreenHeight()/2f));

            Run();
        }

        public static void Run()
        {
            while (!Raylib.WindowShouldClose())
            {
                Update();
                Render();
            }

            Stop();
        }

        public static void Update()
        {
            Player.Update();
            Camera.Update();
        }

        public static void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode2D(Camera.Handle);
                Raylib.DrawRectangle(-250, -250, 500, 500, Color.DarkGray);
                Player.Draw();

                Raylib.DrawLine((int)Camera.Target.X, -Raylib.GetScreenHeight()*10, (int)Camera.Target.X, Raylib.GetScreenHeight()*10, Color.Green);
                Raylib.DrawLine(-Raylib.GetScreenWidth()*10, (int)Camera.Target.Y, Raylib.GetScreenWidth()*10, (int)Camera.Target.Y, Color.Green);

            Raylib.EndMode2D();

            Raylib.EndDrawing();
        }

        public static void Stop()
        {
            Raylib.CloseWindow();
        }
    }
}