using Game2D.Entities;
using Game2D.Classes;

namespace Game2D.Items
{
    public class Log : Item
    {
        public override EntityID EntityID => EntityID.Log;

        public override string Name => "Log";
        public override Vector2 HoldOffset => new Vector2(8, -18);
        public override float HoldRotation => 0.0f;
        public override int MaxStack => 8;

        public Log(Vector2 position) 
            : base(position)
        {
            Sprite = new Sprite("log.png");
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
    }
}
