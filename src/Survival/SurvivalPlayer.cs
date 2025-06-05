using Game2D.Entities;
using Game2D.Interfaces;
using Game2D.Items;

namespace Game2D.Survival
{
    public class SurvivalPlayer : Player, IHasInventory
    {
        public Vitals Vitals;

        public SurvivalPlayer(Vector2 position) : base(position)
        {
            Vitals = new Vitals(100);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Vitals.Update(World.Weather.Temperature);

            var keys = (int)KeyboardKey.One;
            for (int i = keys; i < keys + INVENTORY_SLOTS_COUNT; i++)
            {
                if(Raylib.IsKeyPressed((KeyboardKey)i))
                    SelectedSlot = (i - keys);
            }

            if(Raylib.IsKeyPressed(KeyboardKey.Q))
                Inventory.DropItem(SelectedSlot);
        }

        protected override void OnDraw()
        {
            base.OnDraw();

            var item = Inventory.Slots[SelectedSlot].Item;
            if (item != null)
            {
                item.Sprite.Draw(Position-item.HoldOffset, 1.2f, item.HoldRotation);
            }
        }
    }
}
