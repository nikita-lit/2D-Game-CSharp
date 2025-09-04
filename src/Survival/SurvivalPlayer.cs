using Game2D.Entities;
using Game2D.Interfaces;
using Game2D.Items;
using Game2D.Utils;

namespace Game2D.Survival
{
    public class SurvivalPlayer : Player, IHasInventory
    {
        private const float SIZE = 1.5f;

        public Vitals Vitals;
        public Sprite BodySprite;

        public SurvivalPlayer(Vector2 position) : base(position)
        {
            Vitals = new Vitals(100);
            BodySprite = new Sprite("player/player_old.png");
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
            var sprite = BodySprite;

            Rectangle source = new Rectangle(0, 0, sprite.Width, sprite.Height);
            Rectangle dest = new Rectangle(Position.X, Position.Y, sprite.Width * SIZE, sprite.Height * SIZE);
            Vector2 origin = new Vector2(dest.Width / 2, dest.Height / 2);

            var item = Inventory.Slots[SelectedSlot].Item;
            if (item != null)
                item.Sprite.Draw(Position - MathX.RotateVector(item.HoldOffset, Rotation), 1.2f, item.HoldRotation + Rotation, true);

            Raylib.DrawTexturePro(
                sprite.Texture, source, dest, origin, Rotation, Color.White
            );
        }
    }
}
