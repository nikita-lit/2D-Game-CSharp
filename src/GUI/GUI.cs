namespace Game2D.Gui
{
    public static class GUI
    {
        public static List<Panel> Panels = new();

        public static void Update()
        {
            foreach(var panel in Panels)
            {
                panel.Update();
            }
        }

        public static void Draw()
        {
            foreach (var panel in Panels)
            {
                panel.Draw();
            }
        }

        public static void DrawWorld()
        {

        }
    }
}
