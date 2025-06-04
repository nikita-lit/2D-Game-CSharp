using Game2D.Gui;
using Raylib_cs;

namespace Game2D
{
    partial class Program
    {
        public static RenderTexture2D RenderTarget;

        public static void InitRender()
        {
            RenderTarget = Raylib.LoadRenderTexture((int)ScreenSize.X, (int)ScreenSize.Y);
        }

        public static void ReloadRenderTarget()
        {
            Raylib.UnloadRenderTexture(RenderTarget);
            RenderTarget = Raylib.LoadRenderTexture((int)ScreenSize.X, (int)ScreenSize.Y);
        }

        public static void Render()
        {
            Raylib.BeginTextureMode(RenderTarget);
                Raylib.ClearBackground(Color.Black);

                Raylib.BeginMode2D(Camera.Handle);
                DrawWorld();
                Raylib.EndMode2D();

                DrawScreen();
            Raylib.EndTextureMode();

            Raylib.BeginDrawing();
                var texture = RenderTarget.Texture;
                Rectangle source = new Rectangle(0, 0, texture.Width, -texture.Height); // flip Y
                Rectangle dest = new Rectangle(0, 0, texture.Width, texture.Height);
                Vector2 origin = new Vector2(0, 0);

                Raylib.DrawTexturePro(texture, source, dest, origin, 0.0f, Color.White);
            Raylib.EndDrawing();
        }

        public static void DrawScreen()
        {
            GUI.Draw();
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
