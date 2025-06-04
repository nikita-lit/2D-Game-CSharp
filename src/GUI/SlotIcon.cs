using Game2D.Entities;
using Game2D.Items;

namespace Game2D.Gui
{
    public class SlotIcon : Button
    {
        public InventorySlot Slot;

        public SlotIcon(
            InventorySlot slot, 
            float width,
            float height,
            Func<bool> canUseFunc = null,
            Action<Panel> onDraw = null
        ) : base(width, height, canUseFunc, onDraw)
        {
            Slot = slot;
        }

        protected override void OnDraw()
        {
            if (Slot == null) return;

            Raylib.DrawRectangleRounded(Rect, 0.2f, 5, new Color(15, 15, 15, 100));
            if(Slot.Inventory.Parent is Player player && player.SelectedSlot == Slot.ID)
                Raylib.DrawRectangleRoundedLinesEx(Rect, 0.2f, 5, 3.0f, new Color(220, 220, 220, 255));
            else
                Raylib.DrawRectangleRoundedLinesEx(Rect, 0.2f, 5, 2.0f, new Color(100, 100, 100, 255));

            if (Slot.Item != null)
            {
                GUI.DrawAlignedText("Item"+(Slot.ID+1), Position + new Vector2(Rect.Width / 2, Rect.Height / 2), "Pixel", 26, 
                    Color.White, hAlign: HorizontalAlign.Center, vAlign: VerticalAlign.Center);
            }
        }
    }
}
