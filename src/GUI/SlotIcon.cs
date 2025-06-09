using Game2D.Entities;
using Game2D.Items;
using Game2D.Render;

namespace Game2D.Gui
{
    public class SlotIcon : Button
    {
        public InventorySlot Slot;

        public SlotIcon(
            Panel parent,
            InventorySlot slot, 
            float width,
            float height,
            Func<bool> canUseFunc = null,
            Action<Panel> onDraw = null
        ) : base(parent, width, height, canUseFunc, onDraw)
        {
            Slot = slot;
        }

        protected override void OnDraw()
        {
            if (Slot == null) return;

            Raylib.DrawRectangleRec(Rect, new Color(15, 15, 15, 100));
            if(Slot.Inventory.Parent is Player player && player.SelectedSlot == Slot.ID)
                Raylib.DrawRectangleLinesEx(Rect, 3.0f, new Color(220, 220, 220, 255));
            else
                Raylib.DrawRectangleLinesEx(Rect, 2.0f, new Color(100, 100, 100, 255));

            if (Slot.Item != null)
            {
                var sprite = Slot.Item.Sprite;
                var textureOffset = new Vector2(
                    (sprite.Width * GUI.SS(1.3f)) / 2.0f,
                    (sprite.Height * GUI.SS(1.3f)) / 2.0f
                );

                Raylib.DrawTextureEx(sprite.Texture, RectCenter - textureOffset, 0.0f, GUI.SS(1.3f), Color.White);

                if(IsHovered())
                    Render.Draw.AlignedText(Slot.Item.Name, RectCenter, "Pixel", 16, 
                        Color.White, hAlign: HorizontalAlign.Center, vAlign: VerticalAlign.Center);

                Render.Draw.AlignedText("x" + Slot.Item.Stack, Position + new Vector2(Rect.Width - GUI.SS(4), Rect.Height - GUI.SS(2)), "Pixel", 16, 
                    Color.White, hAlign: HorizontalAlign.Right, vAlign: VerticalAlign.Bottom);
            }
        }
    }
}
