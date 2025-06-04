using Game2D.Utils;
using Game2D.Classes;

namespace Game2D.Entities
{
    public class Campfire : Entity
    {
        public override EntityID EntityID => EntityID.Campfire;

        private const float TILE_SIZE = 32f;
        private const float SIZE = 2f;
        private const float SPRITE_SIZE = TILE_SIZE * SIZE;

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
            _spriteFire = new Sprite("campfire_fire.png");

            Collider = new RectCollider() {
                Rect = new Rectangle((int)Position.X - (25 / 2), (int)Position.Y - (25 / 2), 25, 25),
            };

            _clickable = new WorldClickable(
                new Rectangle((int)Position.X - (50 / 2), (int)Position.Y - (50 / 2), 50, 50),
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
            _heatSource.Update();

            _clickable.Position = Position;
            _clickable.Update();
        }

        protected override void OnDraw()
        {
            var textureOffset = new Vector2(
                SPRITE_SIZE/2,
                SPRITE_SIZE/2
            );

            Raylib.DrawTextureEx((IsLit ? _spriteFire.Texture : _sprite.Texture), Position - textureOffset, 0.0f, SIZE, Color.White);
            Raylib.DrawText(Fuel.ToString(), (int)Position.X, (int)Position.Y, 26, Color.White);

            _clickable.Draw();
        }
    }
}
