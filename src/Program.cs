global using Raylib_cs;
global using System.Numerics;
using Game2D.Entities;
using Game2D.Gui;
using Game2D.Survival;
using Game2D.Environment;
using Game2D.Utils;
using Game2D.Classes;
using Game2D.Items;

namespace Game2D
{
    partial class Program
    {
        public static int ScreenWidth = 0;
        public static int ScreenHeight = 0;

        public static World World;
        public static SurvivalPlayer Player;
        public static Camera Camera;

        public static Vector2 GetMouseWorldPos() => Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), Camera.Handle);

        public static void Main()
        {
            Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
            Raylib.InitWindow(1280, 800, "2D Game");

            ScreenWidth = Raylib.GetScreenWidth();
            ScreenHeight = Raylib.GetScreenHeight();

            World = new World();
            Player = new SurvivalPlayer(Vector2.Zero);
            Camera = new Camera(Vector2.Zero, new Vector2(Raylib.GetScreenWidth()/2f, Raylib.GetScreenHeight()/2f));

            _ = new Campfire(new Vector2(0, 100));

            //for (int i = 0; i < 10; i++)
            //    _ = new Tree(new Vector2(Random.Shared.Next(-500, 500), Random.Shared.Next(-500, 500)));

            _ = new Item(new Vector2(25, 25));

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

        private static float _cameraFollowScale = 0;

        public static void Update()
        {
            UpdateInput();
            GUI.Update();

            if (Raylib.IsWindowResized() && !Raylib.IsWindowFullscreen())
            {
                var newWidth = Raylib.GetScreenWidth();
                var newHeight = Raylib.GetScreenHeight();
                OnWindowResize?.Invoke(ScreenWidth, ScreenHeight, newWidth, newHeight);
                ScreenWidth = newWidth;
                ScreenHeight = newHeight;
            }

            // collision detection
            var entities = World.Entities.Values.ToList();
            for (int i = 0; i < entities.Count; i++)
            {
                var entity1 = entities[i];
                var collider1 = entity1.Collider;
                entity1.Update();

                if (collider1 == null || !collider1.Active) continue;
                var rect1 = (collider1 as RectCollider).Rect;

                var center1 = new Vector2(rect1.X + rect1.Width / 2, rect1.Y + rect1.Height / 2);
                var hs1 = new Vector2(rect1.Width * 0.5f, rect1.Height * 0.5f);

                for (int j = i + 1; j < entities.Count; j++)
                {
                    var entity2 = entities[j];
                    var collider2 = entity2.Collider;
                    if (collider2 == null || !collider2.Active) continue;

                    if (!collider1.CheckCollision(collider2)) 
                        continue;

                    var rect2 = (collider2 as RectCollider).Rect;
                    var center2 = new Vector2(rect2.X + rect2.Width / 2, rect2.Y + rect2.Height / 2);
                    var delta = center1 - center2;
                    var hs2 = new Vector2(rect2.Width * 0.5f, rect2.Height * 0.5f);

                    float minDistX = hs1.X + hs2.X - MathF.Abs(delta.X);
                    float minDistY = hs1.Y + hs2.Y - MathF.Abs(delta.Y);

                    if (minDistX < minDistY)
                        entity1.Position.X += MathF.CopySign(minDistX, delta.X);
                    else
                        entity1.Position.Y += MathF.CopySign(minDistY, delta.Y);
                }
            }

            UpdateCamera();
        }

        public static void UpdateCamera()
        {
            var x = Math.Abs(MathX.Map((Camera.Target.X - Player.Position.X), -500, 500, -1, 1));
            var y = Math.Abs(MathX.Map((Camera.Target.Y - Player.Position.Y), -500, 500, -1, 1));
            var scale = (x + y);

            if (x <= 0.15f && y <= 0.15f)
                scale = 0;

            _cameraFollowScale = float.Lerp(_cameraFollowScale, scale, Raylib.GetFrameTime() * 2f);
            Camera.Target = Vector2.Lerp(Camera.Target, Player.Position, Raylib.GetFrameTime() * _cameraFollowScale * 4f);
            Camera.Update();
        }

        public static void Stop()
        {
            Raylib.CloseWindow();
        }

        public delegate void OnWindowResizeEvent(int oldWidth, int oldHeight, int width, int height);
        public static event OnWindowResizeEvent OnWindowResize;
    }
}