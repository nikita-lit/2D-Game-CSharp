using Game2D.Entities;
using Game2D.Environment;
using Game2D.Survival;

namespace Game2D.Gui
{
    public class HUD
    {
        private List<SlotIcon> _slots = new();

        public HUD()
        {
            float slotSize = 80f;
            float slotSpacing = 10f;
            float slotWidth = slotSize + slotSpacing;

            float slotsOffset = (GUI.SrcW()/2) - ((slotWidth * Player.INVENTORY_SLOTS_COUNT) / 2f);
            var survPlayer = (GUI.LocalPlayer() as SurvivalPlayer);

            for (int i = 0; i < Player.INVENTORY_SLOTS_COUNT; i++)
            {
                var slot = new SlotIcon(survPlayer.Inventory.Slots[i], slotSize, slotSize, () => true);

                slot.OnPress += (panel, user, button) =>
                {
                    var sloticon = panel as SlotIcon;
                    if (user is Player player)
                        player.SelectedSlot = sloticon.Slot.ID;
                };

                slot.Position = new Vector2(
                    slotsOffset + (slotWidth * i),
                    GUI.SrcH() - (slotSize + 10f)
                );

                _slots.Add(slot);
            }
        }

        public void Update()
        {

        }

        public void Draw()
        {
            var pos = new Vector2(GUI.SS(20), GUI.SS(15));

            GUI.DrawAlignedText("HP: " + Program.Player.Vitals.Health,
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            GUI.DrawAlignedText("C: " + Program.World.Weather.Temperature,
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            GUI.DrawAlignedText("Body C: " + Program.Player.Vitals.Temperature.ToString("0.0"),
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            GUI.DrawAlignedText("Entities: " + Program.World.Entities.Count,
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            GUI.DrawAlignedText("HeatSource: " + Program.Player.Vitals.IsNearToHeatSource,
                pos, "PixelBold", 36, Color.White);
        }

        public void OnScreenResize(Vector2 oldSize, Vector2 newSize)
        {

        }
    }
}
