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

        public HeatSource _heatSource;

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

            _heatSource = new HeatSource(position) {
                Parent = this,
                Radius = 300.0f,
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
            if (isHovered && Raylib.IsMouseButtonPressed(MouseButton.Left))
                Toggle();

            if (IsLit)
                _heatSource.Update();
        }

        public override void Draw()
        {
            var textureOffset = new Vector2(
                SPRITE_SIZE/2,
                SPRITE_SIZE/2
            );

            Raylib.DrawTextureEx((IsLit ? _spriteFire.Texture : _sprite.Texture), Position - textureOffset, 0.0f, SIZE, Color.White);
            //Raylib.DrawRectangleLinesEx(UseRect, 1.0f, Color.Lime);
        }
    }
}
