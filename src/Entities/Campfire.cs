using Game2D.Classes;
using Game2D.Gui;
using Game2D.Utils;
using Game2D.Render;

namespace Game2D.Entities
{
    public class Campfire : Entity
    {
        public override EntityID EntityID => EntityID.Campfire;

        private Sprite _sprite;
        private Sprite _spriteFire;
        private WorldClickable _clickable;

        public bool IsLit;
        public float Fuel;
        private float _fuelTimer = 0f;

        public HeatSource _heatSource;
        public float Radius;

        public Campfire(Vector2 position) 
            : base(position)
        {
            Fuel = 100;
            Radius = 300f;

            _sprite = new Sprite("campfire.png");
            _sprite.Size = 2.0f;

            _spriteFire = new Sprite("campfire_fire.png");
            _spriteFire.Size = 2.0f;

            Collider = new RectCollider() {
                Rect = new Rectangle((int)Position.X - (25 / 2), (int)Position.Y - (25 / 2), 25, 25),
                IsStatic = true,
            };

            _clickable = new WorldClickable(
                new Rectangle(0, 0, 60, 30),
                () => (!HasFlag(EntityFlag.NotUsable) && Fuel > 0),
                OnUse);

            _heatSource = new HeatSource(position) {
                Parent = this,
                Radius = Radius,
                IsEnabled = true
            };
        }

        public void OnUse(Entity user)
        {
            IsLit = !IsLit;
        }

        protected override void OnUpdate()
        {
            if (IsLit)
            {
                _fuelTimer += Raylib.GetFrameTime();

                if (_fuelTimer >= 1.0f)
                {
                    Fuel -= 1;
                    Fuel = Math.Clamp(Fuel, 0, 100);

                    if (Fuel <= 0)
                        IsLit = false;

                    float fuelFactor = MathX.Map(Fuel, 0, 100, 0.3f, 1.0f);
                    _heatSource.Radius = Radius * fuelFactor;
                    _fuelTimer = 0;
                }
            }

            _heatSource.IsEnabled = IsLit;
            _clickable.Position = Position;
            _clickable.Update();
        }

        protected override void OnDraw()
        {
            if (IsLit)
                _spriteFire.Draw(Position - new Vector2(0, 30));
            else
                _sprite.Draw(Position - new Vector2(0, 30));

            if (_clickable.IsHovered())
                Render.Draw.AlignedText(Fuel.ToString(), _clickable.RectCenter, "PixelBold", 26,
                    Color.White, scaleFont: false, hAlign: HorizontalAlign.Center, vAlign: VerticalAlign.Center);

            _clickable.Draw();
        }

        protected override void OnDestroy()
        {
            _clickable.Dispose();
            _clickable = null;
        }
    }
}
