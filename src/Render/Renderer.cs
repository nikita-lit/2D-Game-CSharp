using Game2D.Environment;
using Game2D.Gui;

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

            RenderTextures[name] = renderTexture;
            return Raylib.LoadRenderTexture((int)size.X, (int)size.Y);
        }

        public void ReloadRenderTarget(string name, Vector2 size)
        {
            bool isValid = RenderTextures.TryGetValue(name, out RenderTexture2D renderTexture);
            bool isGPUValid = Raylib.IsRenderTextureValid(renderTexture);

            if (isValid && !isGPUValid)
                throw new Exception("Render texture in dictionary but isnʼt loaded in GPU.");

            if (!isValid)
                return;

            Raylib.UnloadRenderTexture(RenderTextures[name]);
            RenderTextures[name] = Raylib.LoadRenderTexture((int)size.X, (int)size.Y);
        }

        public void Do(RenderTexture2D renderTexture, Camera camera, World world, bool drawScreen = true)
        {
            Raylib.BeginTextureMode(renderTexture);
                Raylib.ClearBackground(new Color(0, 0, 0, 0));

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
