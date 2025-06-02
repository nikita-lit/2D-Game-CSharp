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
        public Rectangle UseRect;

        public bool IsLit;
        public float Fuel;
        private double _fuelTime;

        public HeatSource _heatSource;
        public float Radius;

        public Campfire(Vector2 position) 
            : base(position)
        {
            _sprite = new Sprite("../../assets/textures/campfire.png");
            _spriteFire = new Sprite("../../assets/textures/campfire_fire.png");

            Collider = new RectCollider() {
                Rect = new Rectangle((int)Position.X - (25 / 2), (int)Position.Y - (25 / 2), 25, 25),
            };

            UseRect = new Rectangle((int)Position.X - (50 / 2), (int)Position.Y - (50 / 2), 50, 50);
            Fuel = 100;
            Radius = 300f;

            _heatSource = new HeatSource(position) {
                Parent = this,
                Radius = Radius,
                IsEnabled = true
            };
        }

        public void Toggle()
        {
            IsLit = !IsLit;
        }

        public override void Update()
        {
            RectCollider.Rect.Position = Position - RectCollider.HalfRect;

            bool isHovered = Raylib.CheckCollisionPointRec(Program.GetMouseWorldPos(), UseRect);
            Raylib.SetMouseCursor(isHovered ? MouseCursor.PointingHand : MouseCursor.Default);
            if (isHovered && Raylib.IsMouseButtonPressed(MouseButton.Left) && Fuel > 0)
                Toggle();

            if (IsLit)
            {
                if (_fuelTime < Raylib.GetTime())
                {
                    Fuel -= 1;
                    Fuel = Math.Clamp(Fuel, 0, 100);

                    if (Fuel <= 0)
                        IsLit = false;

                    float fuelFactor = MathX.Map(Fuel, 0, 100, 0.3f, 1.0f);
                    _heatSource.Radius = Radius * fuelFactor;
                    _fuelTime = Raylib.GetTime() + 1.0f;
                }
            }

            _heatSource.IsEnabled = IsLit;
            _heatSource.Update();
        }

        public override void Draw()
        {
            var textureOffset = new Vector2(
                SPRITE_SIZE/2,
                SPRITE_SIZE/2
            );

            Raylib.DrawTextureEx((IsLit ? _spriteFire.Texture : _sprite.Texture), Position - textureOffset, 0.0f, SIZE, Color.White);
            Raylib.DrawText(Fuel.ToString(), (int)Position.X, (int)Position.Y, 26, Color.White);
            //Raylib.DrawRectangleLinesEx(UseRect, 1.0f, Color.Lime);
        }
    }
}
