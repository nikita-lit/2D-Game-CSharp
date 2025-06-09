using Game2D.Environment;
using Game2D.Gui;

namespace Game2D.Render
{
    public class Renderer
    {
        public RenderTexture2D RenderTarget;

        public void Init(Vector2 screenSize)
        {
            RenderTarget = Raylib.LoadRenderTexture((int)screenSize.X, (int)screenSize.Y);
        }

        public void ReloadRenderTarget(Vector2 screenSize)
        {
            Raylib.UnloadRenderTexture(RenderTarget);
            RenderTarget = Raylib.LoadRenderTexture((int)screenSize.X, (int)screenSize.Y);
        }

        public void Do(Camera camera, World world)
        {
            Raylib.BeginTextureMode(RenderTarget);
                Raylib.ClearBackground(Color.Black);

                Raylib.BeginMode2D(camera.Handle);
                    DrawWorld(world);
                Raylib.EndMode2D();

                DrawScreen();
            Raylib.EndTextureMode();


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

        public void DrawScreen()
        {
            GUI.Draw();

            //if (Focus is Panel panel)
            //    Raylib.DrawRectangleLinesEx(panel.Rect, 1.0f, Color.Orange);
        }

        public void DrawWorld(World world)
        {
            Raylib.DrawRectangle(-500, -500, 1000, 1000, Color.DarkGray);

            foreach (var pair in world.Entities.OrderBy(e => e.Value.Position.Y))
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
