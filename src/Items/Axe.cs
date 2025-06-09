
using Game2D.Classes;
using Game2D.Entities;

namespace Game2D.Items
{
    public class Axe : Tool
    {
        public override EntityID EntityID => EntityID.Axe;

        public override string Name => "Axe";
        public override Vector2 HoldOffset => new Vector2(8, -18);
        public override float HoldRotation => 90.0f;

        public override float PrimaryCooldown => 0.2f;
        public override float SecondaryCooldown => -1.0f;

        public Axe(Vector2 position)
            : base(position)
        {
            Sprite = new Sprite("axe.png");
            Sprite.Size = 1.5f;

            Collider = new RectCollider()
            {
                Rect = new Rectangle(0, 0, 30, 15),
            };

            _clickable = new WorldClickable(
                new Rectangle(0, 0, 50, 35),
                () => !HasFlag(EntityFlag.NotUsable),
                OnUse);
        }

        protected override void OnPrimary()
        {
            Console.WriteLine("OnPrimary");
        }
    }
}
