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
            Raylib.InitWindow(1920, 1080, "2D Game");

            Player = new Player(Vector2.Zero);
            Camera = new Camera(Vector2.Zero, new Vector2(Raylib.GetScreenWidth()/2f, Raylib.GetScreenHeight()/2f));

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
            foreach (var entity in Entity.All)
                entity.Value.Update();

            var x = MathX.Map((Camera.Target.X - Player.Position.X), -500, 500, -1, 1);
            var y = MathX.Map((Camera.Target.Y - Player.Position.Y), -500, 500, -1, 1);

            if (x >= 1.0f || x <= -1.0f)
                Camera.Target -= new Vector2(x, 0);

            Camera.Update();
        }

        public static void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode2D(Camera.Handle);
                DrawWorld();
            Raylib.EndMode2D();

            DrawScreen();

            Raylib.EndDrawing();
        }

        public static void DrawScreen()
        {

        }

        public static void DrawWorld()
        {
            Raylib.DrawRectangle(-250, -250, 500, 500, Color.DarkGray);
            foreach (var entity in Entity.All)
                entity.Value.Draw();

            Raylib.DrawLine((int)Camera.Target.X, -Raylib.GetScreenHeight() * 10, (int)Camera.Target.X, Raylib.GetScreenHeight() * 10, Color.Green);
            Raylib.DrawLine(-Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Color.Green);
        }

        public static void Stop()
        {
            Raylib.CloseWindow();
        }
    }
}