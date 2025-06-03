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

            float slotsOffset = (GUI.SrcW()/2) - ((slotWidth * 5) / 2f);
            var survPlayer = (GUI.LocalPlayer() as SurvivalPlayer);

            for (int i = 0; i < 5; i++)
            {
                var slot = new SlotIcon(survPlayer.Inventory.Slots[i], slotSize, slotSize, null, null);

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

        }

        public void OnScreenResize(Vector2 oldSize, Vector2 newSize)
        {

        }
    }
}
