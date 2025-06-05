using Game2D.Entities;
using Game2D.Classes;

namespace Game2D.Items
{
    public class Log : Item
    {
        public override EntityID EntityID => EntityID.Log;

        public override string Name => "Log";
        public override Vector2 HoldOffset => new Vector2(10, 0);
        public override float HoldRotation => 25.0f;

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
