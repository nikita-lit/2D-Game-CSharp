global using Raylib_cs;
using System.Numerics;

namespace Game2D
{
    class Program
    {
        public static Player Player;

        public static void Main()
        {
            Player = new Player();
            Raylib.InitWindow(800, 480, "Hello World");
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
        }

        public static void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Player.Draw();

            //Raylib.DrawText("Hello, world!", 12, 12, 20, Color.Black);
            //Raylib.DrawRectangle(0, 0, 50, 50, Color.Black);

            Raylib.EndDrawing();
        }

        public static void Stop()
        {
            Raylib.CloseWindow();
        }
    }
}