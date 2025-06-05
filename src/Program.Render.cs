using Game2D.Classes;
using Game2D.Gui;

namespace Game2D
{
    partial class Program
    {
        public static RenderTexture2D RenderTarget;
        //public static RenderTexture2D RenderTarget2;

        public static void InitRender()
        {
            RenderTarget = Raylib.LoadRenderTexture((int)ScreenSize.X, (int)ScreenSize.Y);
            //RenderTarget2 = Raylib.LoadRenderTexture((int)ScreenSize.X, (int)ScreenSize.Y);
        }

        public static void ReloadRenderTarget()
        {
            Raylib.UnloadRenderTexture(RenderTarget);
            //Raylib.UnloadRenderTexture(RenderTarget2);
            RenderTarget = Raylib.LoadRenderTexture((int)ScreenSize.X, (int)ScreenSize.Y);
            //RenderTarget2 = Raylib.LoadRenderTexture((int)ScreenSize.X, (int)ScreenSize.Y);
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

            //Raylib.BeginTextureMode(RenderTarget2);
            //    Raylib.ClearBackground(Color.SkyBlue);

            //    Raylib.BeginMode2D(Camera2.Handle);
            //        DrawWorld();
            //    Raylib.EndMode2D();
            //Raylib.EndTextureMode();

            Raylib.BeginDrawing();
                var texture = RenderTarget.Texture;
                Rectangle source = new Rectangle(0, 0, texture.Width, -texture.Height); // flip Y
                Rectangle dest = new Rectangle(0, 0, texture.Width, texture.Height);
                Raylib.DrawTexturePro(texture, source, dest, Vector2.Zero, 0.0f, Color.White);

                //texture = RenderTarget2.Texture;
                //source = new Rectangle(0, 0, texture.Width, -texture.Height); // flip Y
                //dest = new Rectangle(0, 0, texture.Width/2, texture.Height/2);
                //Raylib.DrawTexturePro(texture, source, dest, Vector2.Zero, 0.0f, Color.White);
            Raylib.EndDrawing();
        }

        public static void DrawScreen()
        {
            GUI.Draw();

            //if (Focus is Panel panel)
            //    Raylib.DrawRectangleLinesEx(panel.Rect, 1.0f, Color.Orange);
        }

        public static void DrawWorld()
        {
            Raylib.DrawRectangle(-500, -500, 1000, 1000, Color.DarkGray);
            foreach (var pair in World.Entities.OrderBy(e => e.Value.Position.Y))
            {
                pair.Value.Draw();
            }

            //if (Focus is WorldClickable wc)
            //    Raylib.DrawRectangleLinesEx(wc.Rect, 1.0f, Color.Orange);

            //Raylib.DrawLine((int)Camera.Target.X, -Raylib.GetScreenHeight() * 10, (int)Camera.Target.X, Raylib.GetScreenHeight() * 10, Color.Green);
            //Raylib.DrawLine(-Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Color.Green);

            GUI.DrawWorld();
        }
    }
}
