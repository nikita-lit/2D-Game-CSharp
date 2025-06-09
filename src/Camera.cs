using Game2D.Entities;
using Game2D.Utils;

namespace Game2D
{
    public sealed class Camera
    {
        public Camera2D Handle;

        public Entity FollowTarget;

        private const float SPEED = 600f;
        private const int BOUNDS = 400;

        private const float SPEED_ZOOM = 0.1f;
        private const float MIN_ZOOM = 0.6f;
        private const float MAX_ZOOM = 2.0f;

        private static float _followScale = 0;
        private static float _followScaleTime = 0;
        private Vector2 _prevTargetPos;
        private static double _lastZoomTime = 0;

        private float Zoom = 1.0f;
        private float LerpZoom = 1.0f;
        private float BaseZoom => Raylib.GetScreenHeight() / Height;

        private const float Height = 600f;

        public Vector2 Target
        {
            get => Handle.Target;
            set => Handle.Target = value;
        }

        public Camera(Vector2 target, Vector2 offset, float rotation = 0.0f)
        {
            Handle = new()
            {
                Target = target,
                Offset = offset,
                Rotation = rotation,
                Zoom = BaseZoom * Zoom,
            };

            Program.OnScreenResize += (oldSize, newSize) => {
                Handle.Offset = newSize / 2;
                Handle.Zoom = BaseZoom * Zoom;
            };
        }

        public void Update()
        {
            if (FollowTarget != null)
                DoFollowTarget();

            UpdateZoom();
        }

        public void DoFollowTarget()
        {
            var targetPos = FollowTarget.Position;

            if (_prevTargetPos != targetPos)
            {
                _prevTargetPos = targetPos;
                _lastZoomTime = Raylib.GetTime();
            }

            var x = Math.Abs(MathX.Map((Target.X - targetPos.X), -500, 500, -1, 1));
            var y = Math.Abs(MathX.Map((Target.Y - targetPos.Y), -500, 500, -1, 1));
            var scale = (x + y);

            var bounds = 0.1f;
            var followScaleTime = 4f;

            if (Raylib.IsCursorHidden())
            {
                bounds = 0.05f;
                followScaleTime = 60f;
            }

            if ((x <= bounds && y <= bounds))
                scale = 0;

            _followScale = float.Lerp(_followScale, scale, Raylib.GetFrameTime() * 2f);
            _followScaleTime = float.Lerp(_followScaleTime, followScaleTime, Raylib.GetFrameTime() * 4f);

            if (_lastZoomTime > Raylib.GetTime())
                _followScale = 0;

            Target = Vector2.Lerp(Target, targetPos, Raylib.GetFrameTime() * _followScale * _followScaleTime);

            if (Raylib.IsKeyDown(KeyboardKey.Up))
            {
                Handle.Target.Y -= SPEED * Raylib.GetFrameTime();
                _lastZoomTime = Raylib.GetTime() + 4f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Down))
            {
                Handle.Target.Y += SPEED * Raylib.GetFrameTime();
                _lastZoomTime = Raylib.GetTime() + 4f;
            }

            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                Handle.Target.X += SPEED * Raylib.GetFrameTime();
                _lastZoomTime = Raylib.GetTime() + 4f;
            }
            else if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                Handle.Target.X -= SPEED * Raylib.GetFrameTime();
                _lastZoomTime = Raylib.GetTime() + 4f;
            }

            Target = Vector2.Clamp(Target, targetPos + new Vector2(-BOUNDS, -BOUNDS), targetPos + new Vector2(BOUNDS, BOUNDS));
        }

        private void UpdateZoom()
        {
            LerpZoom = float.Lerp(LerpZoom, Zoom, Raylib.GetFrameTime() * 8f);
            Handle.Zoom = BaseZoom * LerpZoom;

            if (Raylib.IsKeyPressed(KeyboardKey.R) || Raylib.IsCursorHidden())
            {
                Zoom = (Raylib.IsCursorHidden() ? 1.3f : 1.0f);
                _lastZoomTime = Raylib.GetTime();
            }

            float wheel = Raylib.GetMouseWheelMove();
            if (wheel == 0 || Raylib.IsCursorHidden()) return;

            Vector2 mouseWorldBefore = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Handle);

            Zoom += wheel * SPEED_ZOOM;
            Zoom = Math.Clamp(Zoom, MIN_ZOOM, MAX_ZOOM);

            Vector2 mouseWorldAfter = Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Handle);
            Vector2 offset = Vector2.Subtract(mouseWorldBefore, mouseWorldAfter);

            Handle.Target = Vector2.Add(Handle.Target, offset);
            _lastZoomTime = Raylib.GetTime() + 2f;
        }
    }
}
