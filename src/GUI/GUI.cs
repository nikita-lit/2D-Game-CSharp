using Game2D.Assets;
using Game2D.Entities;

namespace Game2D.Gui
{
    public static partial class GUI
    {
        public static Panel Root;

        public static float SrcW() => Program.ScreenSize.X;
        public static float SrcH() => Program.ScreenSize.Y;

        public static float OSrcW() => 640.0f;
        public static float OSrcH() => 480.0f;

        public static float ScaleX() => SrcW() / OSrcW();
        public static float ScaleY() => SrcH() / OSrcH();

        public static float SS(float x) => x * ScaleX();
        public static int SS(int x) => (int)(x * ScaleX());

        public static float SSY(float x) => x * ScaleY();
        public static int SSY(int x) => (int)(x * ScaleY());

        public static Player LocalPlayer() => Program.Player;

        public static List<Panel> Panels = new();
        public static HUD HUD;

        public static void Init()
        {
            Root = new Panel(null, SrcW(), SrcH());
            Root.IsInteractive = false;

            HUD = new HUD(Root);
            HUD.Root.IsInteractive = false;

            AssetsSystem.LoadFont("Pixel", "PixelOperator.ttf");
            AssetsSystem.LoadFont("PixelBold", "PixelOperator-Bold.ttf");
            //_ = new TTT.TicTacToe(Root, SrcW(), SrcH());
            Program.OnScreenResize += OnScreenResize;
        }

        public static void Update()
        {
            foreach(var panel in Panels)
            {
                panel.Update();
            }
        }

        public static void Draw()
        {
            HUD.Draw();
            foreach (var panel in Panels)
                panel.Draw();
        }

        public static void DrawWorld()
        {
        }

        public static void OnScreenResize(Vector2 oldSize, Vector2 newSize)
        {
            foreach (var panel in Panels)
                panel.OnScreenResize(oldSize, newSize);

            HUD.OnScreenResize(oldSize, newSize);
        }
    }
}
