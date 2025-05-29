namespace Game2D
{
    public sealed class Camera
    {
        public Camera2D Handle;
        private const float _SPEED = 250f;

        public Vector2 Target 
        {
            get
            {
                return Handle.Target;
            }
            set
            {
                Handle.Target = value;
            }
        }

        public Camera(Vector2 target, Vector2 offset, float rotation = 0.0f, float zoom = 1.0f)
        {
            Handle = new()
            {
                Target = target,
                Offset = offset,
                Rotation = rotation,
                Zoom = zoom,
            };
        }

        public void Update()
        {
            Handle.Zoom = MathF.Exp(MathF.Log(Handle.Zoom) + ((float)Raylib.GetMouseWheelMove() * 0.1f));

            if (Handle.Zoom > 2.0f) 
                Handle.Zoom = 2.0f;
            else if (Handle.Zoom < 0.1f) 
                Handle.Zoom = 0.1f;

            if (Raylib.IsKeyPressed(KeyboardKey.R))
                Handle.Zoom = 1.0f;

            if (Raylib.IsKeyDown(KeyboardKey.Up))
                Handle.Target.Y -= _SPEED * Raylib.GetFrameTime();
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
                Handle.Target.Y += _SPEED * Raylib.GetFrameTime();

            if (Raylib.IsKeyDown(KeyboardKey.Right))
                Handle.Target.X += _SPEED * Raylib.GetFrameTime();
            else if (Raylib.IsKeyDown(KeyboardKey.Left))
                Handle.Target.X -= _SPEED * Raylib.GetFrameTime();
        }
    }
}
