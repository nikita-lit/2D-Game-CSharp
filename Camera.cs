namespace Game2D
{
    public sealed class Camera
    {
        public Camera2D Handle;
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

        public Camera(Vector2 target, Vector2 offset)
        {
            Handle = new()
            {
                Target = target,
                Offset = offset,
                Rotation = 0.0f,
                Zoom = 1.0f,
            };
        }

        public void Update()
        {
            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                Handle.Target.Y -= 0.01f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                Handle.Target.Y += 0.01f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                Handle.Target.X += 0.01f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                Handle.Target.X -= 0.01f;
            }
        }
    }
}
