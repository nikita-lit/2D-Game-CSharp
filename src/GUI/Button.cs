using Game2D.Entities;

namespace Game2D.Gui
{
    public class Button : Panel
    {
        public Func<bool> CanUseFunc;

        public event Action<Button, Entity, MouseButton> OnPress;
        public event Action<Button, Entity, MouseButton> OnDown;
        public event Action<Button, Entity, MouseButton> OnRelease;

        public bool CanUse() => CanUseFunc == null || CanUseFunc();

        public Button(
            float width,
            float height,
            Func<bool> canUseFunc = null,
            Action<Panel> onDraw = null
        ) : base(width, height, onDraw)
        {
            CanUseFunc = canUseFunc;
        }

        protected override void OnUpdate()
        {
            if (!CanUse())
                return;

            if (IsHovered())
            {
                for (int i = 0; i < (int)MouseButton.Back; i++)
                {
                    var button = (MouseButton)i;

                    if (Raylib.IsMouseButtonPressed(button))
                        OnPress?.Invoke(this, Program.Player, button);

                    if (Raylib.IsMouseButtonDown(button))
                        OnDown?.Invoke(this, Program.Player, button);

                    if (Raylib.IsMouseButtonReleased(button))
                        OnRelease?.Invoke(this, Program.Player, button);
                }
            }
        }

        protected override void OnDraw()
        {
            Raylib.DrawRectangleRec(Rect, Color.White);
        }
    }
}
