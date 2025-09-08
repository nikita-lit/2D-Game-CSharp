using Game2D.Assets;
using Game2D.Entities;
using Game2D.Survival;

namespace Game2D.Gui
{
    public class HUD
    {
        public Panel Root;
        private List<SlotIcon> _slots = new();
        private bool _isDebugVisible = true;

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
                    (GUI.OSrcH() - slotSize) - slotSpacing
                );

                _slots.Add(slot);
            }
        }

        public void Update()
        {
        }

        public void Draw()
        {
            if (Program.IsDebug)
            {
                var pos = new Vector2(GUI.SS(15), GUI.SS(15));

                Render.Draw.AlignedText("HP: " + Program.Player.Vitals.Health,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("C: " + Program.World.Weather.Temperature,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("Body C: " + Program.Player.Vitals.Temperature.ToString("0.0"),
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("Entities: " + Program.World.Entities.Count,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("HeatSource: " + Program.Player.Vitals.IsNearToHeatSource,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("Pos: " + Program.Player.Position.ToString("0.00"),
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(40);

                Render.Draw.AlignedText("FPS: " + Raylib.GetFPS(),
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("VSync Enabled: " + Program.VSyncEnabled,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("RT Count: " + Program.Renderer.RenderTextures.Count,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("Textures Count: " + AssetsSystem.Textures.Count,
                    pos, "PixelBold", 26, Color.White);

                pos.Y += GUI.SS(20);

                Render.Draw.AlignedText("Fonts Count: " + AssetsSystem.Fonts.Count,
                    pos, "PixelBold", 26, Color.White);
            }
        }

        public void OnScreenResize(Vector2 oldSize, Vector2 newSize)
        {
            //Console.WriteLine();
            //foreach (var slot in _slots)
            //{
            //    Console.WriteLine();
            //    Console.WriteLine(slot.Rect);
            //}
            //Console.WriteLine();
        }
    }
}
