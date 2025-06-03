using Game2D.Classes;
using Game2D.Entities;

namespace Game2D.Gui
{
    public class Button : Panel
    {
        public RectUse RectUse;

        public Button(float width, float height, Action<Entity> onUse, Action<Panel> onDraw) : base(width, height, onDraw) 
        {
            RectUse = new RectUse(Rect, () => IsEnabled, onUse, false);
        }

        protected override void OnUpdate()
        {
            RectUse.Update();
            RectUse.Position = Position;
        }

        protected override void OnDraw()
        {
            Raylib.DrawRectangleRec(Rect, Color.White);
        }
    }
}
