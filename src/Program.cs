global using Raylib_cs;
global using System.Numerics;
using Game2D.Classes;
using Game2D.Entities;
using Game2D.Environment;
using Game2D.Gui;
using Game2D.Items;
using Game2D.Survival;

namespace Game2D
{
    partial class Program
    {
        public static Vector2 ScreenSize = Vector2.Zero;

        public static World World;
        public static SurvivalPlayer Player;
        public static Camera Camera;

        public static Vector2 GetMouseWorldPos() => Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Camera.Handle);

        public static void Main()
        {
            Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
            Raylib.InitWindow(1920, 1080, "2D Game");

            ScreenSize.X = Raylib.GetScreenWidth();
            ScreenSize.Y = Raylib.GetScreenHeight();

            InitRender();

            World = new World();
            Player = new SurvivalPlayer(Vector2.Zero);
            Camera = new Camera(Vector2.Zero, new Vector2(ScreenSize.X / 2f, ScreenSize.Y / 2f));
            Camera.FollowTarget = Player;

            _ = new Campfire(new Vector2(0, 100));

            Vector2 center = new(0, 0);
            for (int i = 0; i < 10; i++)
            {
                float angle = (float)(Random.Shared.NextDouble() * 2 * Math.PI);
                Console.WriteLine(angle);
                float radius = 300 + (float)(Random.Shared.NextDouble() * 200);
                float x = center.X + radius * MathF.Cos(angle);
                float y = center.Y + radius * MathF.Sin(angle);

                Vector2 pos = new(x, y);
                _ = new Tree(pos);
            }

            _ = new Log(new Vector2(25, 25));
            _ = new Axe(new Vector2(25, 25));

            GUI.Init();
            Run();
        }

        public static void Run()
        {
            while (!Raylib.WindowShouldClose())
            {
                Update();
                Render();
            }

            Stop();
        }

        public static void Update()
        {
            UpdateInput();
            GUI.Update();

            if (Raylib.IsWindowResized() && !Raylib.IsWindowFullscreen())
            {
                var newSize = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                OnScreenResize?.Invoke(ScreenSize, newSize);
                ScreenSize = newSize;
                ReloadRenderTarget();
            }

            // collision detection
            var entities = World.Entities.Values.ToList();
            for (int i = 0; i < entities.Count; i++)
            {
                var entity1 = entities[i];
                var collider1 = entity1.Collider;
                entity1.Update();

                if (collider1 == null || !collider1.IsActive) continue;

                var rect1 = (collider1 as RectCollider).Rect;

                var center1 = new Vector2(rect1.X + rect1.Width / 2, rect1.Y + rect1.Height / 2);
                var hs1 = new Vector2(rect1.Width * 0.5f, rect1.Height * 0.5f);

                for (int j = i + 1; j < entities.Count; j++)
                {
                    var entity2 = entities[j];
                    var collider2 = entity2.Collider;
                    if (collider2 == null || !collider2.IsActive) continue;

                    if (!collider1.CheckCollision(collider2)) 
                        continue;

                    var rect2 = (collider2 as RectCollider).Rect;
                    var center2 = new Vector2(rect2.X + rect2.Width / 2, rect2.Y + rect2.Height / 2);
                    var delta = center1 - center2;
                    var hs2 = new Vector2(rect2.Width * 0.5f, rect2.Height * 0.5f);

                    float overlapX = hs1.X + hs2.X - MathF.Abs(delta.X);
                    float overlapY = hs1.Y + hs2.Y - MathF.Abs(delta.Y);

                    if (overlapX <= 0 || overlapY <= 0)
                        continue;

                    if (overlapX < overlapY)
                    {
                        float correction = MathF.CopySign(overlapX, delta.X);
                        ApplyPositionCorrection(entity1, entity2, new Vector2(correction, 0));
                    }
                    else
                    {
                        float correction = MathF.CopySign(overlapY, delta.Y);
                        ApplyPositionCorrection(entity1, entity2, new Vector2(0, correction));
                    }
                }
            }

            Camera.Update();
        }

        private static void ApplyPositionCorrection(Entity e1, Entity e2, Vector2 correction)
        {
            bool e1Static = e1.Collider.IsStatic;
            bool e2Static = e2.Collider.IsStatic;

            if (e1Static && e2Static) return;

            if (e2Static)
                e1.Position += correction;
            else
            {
                if(!e1Static)
                    e1.Position += correction * 0.5f;

                e2.Position -= correction * 0.5f;
            }
        }

        public static void Stop()
        {
            Raylib.CloseWindow();
        }

        public delegate void OnScreenResizeEvent(Vector2 oldSize, Vector2 newSize);
        public static event OnScreenResizeEvent OnScreenResize;
    }
}