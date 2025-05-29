global using Raylib_cs;
global using System.Numerics;
using Game2D.Entities;

namespace Game2D
{
    class Program
    {
        public static int ScreenWidth = 0;
        public static int ScreenHeight = 0;

        public static Player Player;
        public static Camera Camera;
        public static Rectangle Button;

        public static void Main()
        {
            Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
            Raylib.InitWindow(1920, 1080, "2D Game");

            ScreenWidth = Raylib.GetScreenWidth();
            ScreenHeight = Raylib.GetScreenHeight();

            Button = new Rectangle(25, 25, 200, 100);
            Player = new Player(Vector2.Zero);
            Camera = new Camera(Vector2.Zero, new Vector2(Raylib.GetScreenWidth()/2f, Raylib.GetScreenHeight()/2f));

            for (int i = 0; i < 10; i++)
            {
                var tree = new Tree(new Vector2(Random.Shared.Next(-500, 500), Random.Shared.Next(-500, 500)));
            }

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
            var mousePoint = Raylib.GetMousePosition();
            //Console.WriteLine((bool)Raylib.CheckCollisionPointRec(mousePoint, Button));
            if (Raylib.CheckCollisionPointRec(mousePoint, Button))
            {
            }

            if (Raylib.IsWindowResized() && !Raylib.IsWindowFullscreen())
            {
                var newWidth = Raylib.GetScreenWidth();
                var newHeight = Raylib.GetScreenHeight();
                OnWindowResize?.Invoke(ScreenWidth, ScreenHeight, newWidth, newHeight);
                ScreenWidth = newWidth;
                ScreenHeight = newHeight;
            }


            // collision detection
            var entities = Entity.All.Values.ToList();
            for (int i = 0; i < entities.Count; i++)
            {
                var entity1 = entities[i];
                entity1.Update();

                var rect1 = entity1.Rect;
                var center1 = new Vector2(rect1.X + rect1.Width / 2, rect1.Y + rect1.Height / 2);
                var hs1 = new Vector2(rect1.Width * 0.5f, rect1.Height * 0.5f);

                for (int j = i + 1; j < entities.Count; j++)
                {
                    var entity2 = entities[j];
                    var rect2 = entity2.Rect;

                    if (!Raylib.CheckCollisionRecs(rect1, rect2)) 
                        continue;

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

            var x = Math.Abs(MathX.Map((Camera.Target.X - Player.Position.X), -500, 500, -1, 1));
            var y = Math.Abs(MathX.Map((Camera.Target.Y - Player.Position.Y), -500, 500, -1, 1));
            var scale = (x + y);

            if (x <= 0.15f && y <= 0.15f)
                scale = 0;

            _cameraFollowScale = float.Lerp(_cameraFollowScale, scale, Raylib.GetFrameTime() * 2f); 
            Camera.Target = Vector2.Lerp(Camera.Target, Player.Position, Raylib.GetFrameTime() * _cameraFollowScale * 4f);
            Camera.Update();
        }

        public static void Render()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);

            Raylib.BeginMode2D(Camera.Handle);
                DrawWorld();
            Raylib.EndMode2D();

            DrawScreen();

            Raylib.EndDrawing();
        }

        public static void DrawScreen()
        {
            Raylib.DrawRectangleRec(Button, Color.White);
        }

        public static void DrawWorld()
        {
            Raylib.DrawRectangle(-500, -500, 1000, 1000, Color.DarkGray);
            foreach (var pair in Entity.All.OrderBy(e => e.Value.Position.Y))
            {
                var entity = pair.Value;
                var rect1 = entity.Rect;
                entity.Draw();
                //Raylib.DrawRectangleLinesEx(rect1, 1.0f, Color.White);
            }

            //Raylib.DrawLine((int)Camera.Target.X, -Raylib.GetScreenHeight() * 10, (int)Camera.Target.X, Raylib.GetScreenHeight() * 10, Color.Green);
            //Raylib.DrawLine(-Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Raylib.GetScreenWidth() * 10, (int)Camera.Target.Y, Color.Green);
        }

        public static void Stop()
        {
            Raylib.CloseWindow();
        }

        public delegate void OnWindowResizeEvent(int oldWidth, int oldHeight, int width, int height);
        public static event OnWindowResizeEvent OnWindowResize;
    }
}