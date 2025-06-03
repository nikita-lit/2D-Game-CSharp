using Game2D.Classes;
using Game2D.Entities;
using Game2D.Interfaces;

namespace Game2D.Items
{
    public class Item : Entity
    {
        public override EntityID EntityID => EntityID.Item;

        private const float SIZE = 2.0f;
        private readonly Sprite _sprite;
        private readonly RectUse _rectUse;

        public Item(Vector2 position)
            : base(position)
        {
            _sprite = new Sprite("../../assets/textures/tree.png");
            Collider = new RectCollider() {
                Rect = new Rectangle((int)Position.X - (20 / 2), (int)Position.Y - (20 / 2), 20, 20),
            };

            _rectUse = new RectUse(
                new Rectangle((int)Position.X, (int)Position.Y, 40, 40),
                () => !HasFlag(EntityFlag.NotUsable),
                OnUse);
        }

        protected override void OnUpdate()
        {
            _rectUse.Update();
            _rectUse.Position = Position;
        }

        protected override void OnDraw()
        {
            var texturOffset = new Vector2(
                (_sprite.Width * SIZE) / 2,
                (_sprite.Height * SIZE) / 2
            );

            Raylib.DrawTextureEx(_sprite.Texture, Position- texturOffset, 0.0f, SIZE, Color.White);
            _rectUse.Draw();
        }

        public virtual void OnUse(Entity user)
        {
            if (user is not IHasInventory userInv)
                return;

            userInv.Inventory.PickUpItem(this);
        }
    }
}
