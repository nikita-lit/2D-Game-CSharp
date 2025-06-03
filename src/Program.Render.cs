using Game2D.Classes;
using Game2D.Entities;
using Game2D.Gui;

namespace Game2D
{
    partial class Program
    {
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
            Raylib.DrawText("HP: " + Player.Vitals.Health, GUI.SS(15), GUI.SS(10), GUI.SS(20), Color.White);
            Raylib.DrawText("°C: " + World.Weather.Temperature, GUI.SS(15), GUI.SS(40), GUI.SS(20), Color.White);
            Raylib.DrawText("Body °C: " + Player.Vitals.Temperature.ToString("0.0"), GUI.SS(15), GUI.SS(65), GUI.SS(20), Color.White);

            Raylib.DrawText("Entities: " + World.Entities.Count, GUI.SS(15), GUI.SS(95), GUI.SS(20), Color.White);
            Raylib.DrawText("HeatSource: " + Player.Vitals.IsNearToHeatSource, GUI.SS(15), GUI.SS(120), GUI.SS(20), Color.White);
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
