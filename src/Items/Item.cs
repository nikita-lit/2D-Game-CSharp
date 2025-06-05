using Game2D.Classes;
using Game2D.Entities;
using Game2D.Interfaces;

namespace Game2D.Items
{
    public abstract class Item : Entity
    {
        public override EntityID EntityID => EntityID.Item;

        public virtual string Name => "Item";
        public virtual Vector2 HoldOffset => new Vector2(0, 0);
        public virtual float HoldRotation => 0.0f;

        public virtual float SpriteSize { get; private set; } = 2.0f;
        public Sprite Sprite;
        protected WorldClickable _clickable;

        public Item(Vector2 position)
            : base(position)
        {
        }

        protected override void OnUpdate()
        {
            _clickable.Update();
            _clickable.Position = Position;
        }

        protected override void OnDraw()
        {
            Sprite.Draw(Position);
            _clickable.Draw();
        }

        public virtual void OnUse(Entity user)
        {
            if (user is not IHasInventory userInv)
                return;

            userInv.Inventory.PickUpItem(this);
        }
    }
}
