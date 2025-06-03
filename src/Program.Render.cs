using Game2D.Classes;
using Game2D.Entities;
using Game2D.Gui;

namespace Game2D
{
    partial class Program
    {
        public static float ScreenScale(float x) => x * (ScreenHeight / 720.0f);
        public static int ScreenScale(int x) => (int)(x * (ScreenHeight / 720.0f));

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
            GUI.Draw();
            Raylib.DrawText("HP: " + Player.Vitals.Health, ScreenScale(15), ScreenScale(10), ScreenScale(20), Color.White);
            Raylib.DrawText("°C: " + World.Weather.Temperature, ScreenScale(15), ScreenScale(40), ScreenScale(20), Color.White);
            Raylib.DrawText("Body °C: " + Player.Vitals.Temperature.ToString("0.0"), ScreenScale(15), ScreenScale(65), ScreenScale(20), Color.White);

            Raylib.DrawText("Entities: " + World.Entities.Count, ScreenScale(15), ScreenScale(95), ScreenScale(20), Color.White);
            Raylib.DrawText("HeatSource: " + Player.Vitals.IsNearToHeatSource, ScreenScale(15), ScreenScale(120), ScreenScale(20), Color.White);
        }

        public static void DrawWorld()
        {
            Raylib.DrawRectangle(-500, -500, 1000, 1000, Color.DarkGray);
            foreach (var pair in World.Entities.OrderBy(e => e.Value.Position.Y))
            {
                pair.Value.Draw();
            }

            //Raylib.DrawLine((int)Camera.Target.X, -Raylib.GetScreenHeight() * 10, (int)Camera.Target.X, Raylib.GetScreenHeight() * 10, Color.Green);
            //Raylib.DrawLine(-Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Color.Green);

            GUI.DrawWorld();
        }
    }
}
