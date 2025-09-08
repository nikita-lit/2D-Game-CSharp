using Game2D.Environment;
using Game2D.Gui;
using Game2D.Classes;

namespace Game2D.Render
{
    public class Renderer
    {
        public Dictionary<string, RenderTexture2D> RenderTextures = new();

        public bool Init()
        {
            return true;
        }

        public RenderTexture2D CreateRenderTexture(string name, Vector2 size)
        {
            bool isValid = RenderTextures.TryGetValue(name, out RenderTexture2D renderTexture);
            bool isGPUValid = Raylib.IsRenderTextureValid(renderTexture);

            if (!isValid && isGPUValid)
                throw new Exception("Render texture loaded in GPU but not in dictionary.");

            if (isValid && isGPUValid)
                throw new Exception($"Render texture with name [{name}] already exists.");

            RenderTextures[name] = Raylib.LoadRenderTexture((int)size.X, (int)size.Y);
            return RenderTextures[name];
        }

        public RenderTexture2D ReloadRenderTarget(string name, Vector2 size)
        {
            bool isValid = RenderTextures.TryGetValue(name, out RenderTexture2D renderTexture);
            bool isGPUValid = Raylib.IsRenderTextureValid(renderTexture);

            if (isValid && !isGPUValid)
                throw new Exception("Render texture in dictionary but isnʼt loaded in GPU.");

            if (!isValid)
                throw new Exception("Reloading invalid render texture.");

            Raylib.UnloadRenderTexture(RenderTextures[name]);
            RenderTextures[name] = Raylib.LoadRenderTexture((int)size.X, (int)size.Y);
            return RenderTextures[name];
        }

        public void Do(RenderTexture2D renderTexture, Camera camera, World world, bool drawScreen = true)
        {
            Raylib.BeginTextureMode(renderTexture);
                Raylib.ClearBackground(Color.Black);

                Raylib.BeginMode2D(camera.Handle);
                    DrawWorld(world);
                Raylib.EndMode2D();

                if (drawScreen)
                    DrawScreen();
            Raylib.EndTextureMode();
        }

        public void DrawScreen()
        {
            GUI.Draw();

            if (Program.IsDebug)
            {
                if (Program.Focus is Panel panel)
                    Raylib.DrawRectangleLinesEx(panel.Rect, 1.0f, Color.Orange);
            }
        }

        public void DrawWorld(World world)
        {
            world?.Draw();

            if (Program.IsDebug)
            {
                if (Program.Focus is WorldClickable wc)
                    Raylib.DrawRectangleLinesEx(wc.Rect, 1.0f, Color.Orange);
            }

            //Raylib.DrawLine((int)Camera.Target.X, -Raylib.GetScreenHeight() * 10, (int)Camera.Target.X, Raylib.GetScreenHeight() * 10, Color.Green);
            //Raylib.DrawLine(-Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Color.Green);

            GUI.DrawWorld();
        }
    }
}
