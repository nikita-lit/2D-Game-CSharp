using Game2D.Entities;
using Game2D.Items;
using Game2D.Survival;

namespace Game2D.Gui
{
    public class SlotIcon : Button
    {
        public InventorySlot Slot;

        public SlotIcon(InventorySlot slot, float width, float height, Action<Entity> onUse, Action<Panel> onDraw) 
            : base(width, height, onUse, onDraw)
        {
            Slot = slot;
        }

        protected override void OnDraw()
        {
            if (Slot == null) return;

            Raylib.DrawRectangleRounded(Rect, 0.2f, 5, new Color(15, 15, 15, 100));
            Raylib.DrawRectangleRoundedLinesEx(Rect, 0.2f, 5, 1.5f, new Color(100, 100, 100, 255));

            if (Slot.Item != null)
            {
                GUI.DrawAlignedText(Slot.Item.ToString(), Position + new Vector2(Rect.Width / 2, Rect.Height / 2), 22, 
                    Color.White, HorizontalAlign.Center, VerticalAlign.Center);
            }
        }
    }
}
