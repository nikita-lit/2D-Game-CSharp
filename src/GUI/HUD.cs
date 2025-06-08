using Game2D.Entities;
using Game2D.Survival;

namespace Game2D.Gui
{
    public class HUD
    {
        public Panel Root;
        private List<SlotIcon> _slots = new();

        public HUD(Panel root)
        {
            Root = new Panel(root, GUI.OSrcW(), GUI.OSrcH());

            float slotSize = 50f;
            float slotSpacing = 5f;
            float slotWidth = slotSize + slotSpacing;

            float slotsOffset = (GUI.OSrcW()/2) - ((slotWidth * Player.INVENTORY_SLOTS_COUNT) / 2f);
            var survPlayer = (GUI.LocalPlayer() as SurvivalPlayer);

            for (int i = 0; i < Player.INVENTORY_SLOTS_COUNT; i++)
            {
                var slot = new SlotIcon(Root, survPlayer.Inventory.Slots[i], slotSize, slotSize, () => true);

                slot.OnPress += (panel, user, button) =>
                {
                    var sloticon = panel as SlotIcon;
                    if (user is Player player)
                        player.SelectedSlot = sloticon.Slot.ID;
                };

                slot.BasePosition = new Vector2(
                    slotsOffset + (slotWidth * i),
                    GUI.OSrcH() - slotSize
                );

                Console.WriteLine(slot.Rect);
                Console.WriteLine();

                _slots.Add(slot);
            }
        }

        public void Update()
        {

        }

        public void Draw()
        {
            var pos = new Vector2(GUI.SS(20), GUI.SS(15));

            Render.Draw.AlignedText("HP: " + Program.Player.Vitals.Health,
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            Render.Draw.AlignedText("C: " + Program.World.Weather.Temperature,
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            Render.Draw.AlignedText("Body C: " + Program.Player.Vitals.Temperature.ToString("0.0"),
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            Render.Draw.AlignedText("Entities: " + Program.World.Entities.Count,
                pos, "PixelBold", 36, Color.White);

            pos.Y += GUI.SS(35);

            Render.Draw.AlignedText("HeatSource: " + Program.Player.Vitals.IsNearToHeatSource,
                pos, "PixelBold", 36, Color.White);
        }

        public void OnScreenResize(Vector2 oldSize, Vector2 newSize)
        {

        }
    }
}
