using Game2D.Classes;
using Game2D.Items;

namespace Game2D.Entities
{
    public class Player : Entity
    {
        public const int INVENTORY_SLOTS_COUNT = 6;

        public override EntityID EntityID => EntityID.Player;

        public virtual float Speed => 150f;
        public virtual float WalkFactor => 1.0f;
        public virtual float RunFactor => 1.5f;

        public Inventory Inventory { get; private set; }
        public int SelectedSlot = 0;

        private float _targetRotation = 0.0f;

        public Player(Vector2 position) 
            : base(position)
        {
            Collider = new RectCollider() {
                Rect = new Rectangle(0, 0, 50, 50),
            };

            Inventory = new Inventory(this, INVENTORY_SLOTS_COUNT);
        }

        protected override void OnUpdate()
        {
            float dt = Raylib.GetFrameTime();
            Vector2 input = Vector2.Zero;

            if (Raylib.IsKeyDown(KeyboardKey.W)) input.Y -= 1;
            if (Raylib.IsKeyDown(KeyboardKey.S)) input.Y += 1;
            if (Raylib.IsKeyDown(KeyboardKey.D)) input.X += 1;
            if (Raylib.IsKeyDown(KeyboardKey.A)) input.X -= 1;

            if (input != Vector2.Zero)
            {
                input = Vector2.Normalize(input);
                float runFactor = WalkFactor;
                if (Raylib.IsKeyDown(KeyboardKey.LeftShift))
                    runFactor = RunFactor;

                Velocity = input * Speed * runFactor;
                _targetRotation = MathF.Atan2(input.Y, input.X) * Raylib.RAD2DEG + -90f;
            }
            else
                Velocity = Vector2.Zero;

            //Rotation = MathX.AngleLerp(Rotation, _targetRotation, dt * 10f);
            Position += Velocity * dt;
        }
    }
}
