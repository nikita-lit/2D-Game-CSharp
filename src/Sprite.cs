using Game2D.Assets;

namespace Game2D
{
    public class Sprite : IDisposable
    {
        public Texture2D Texture;
        public float Width => Texture.Width;
        public float Height => Texture.Height;
        public float Size = 1.0f;

        public Sprite(string path)
        {
            Texture = AssetsSystem.LoadTexture(path);
        }

        public void Draw(Vector2 position, float size = -1.0f, float rotation = 0.0f, bool invert = false)
        {
            float _size = size != -1.0f ? size : Size;

            Rectangle source = new Rectangle(
                0,
                0,
                invert ? -Width : Width,
                Height
            );

            Rectangle dest = new Rectangle(
                position.X,
                position.Y,
                Width * _size,
                Height * _size
            );

            Vector2 origin = new Vector2(dest.Width / 2f, dest.Height / 2f);
            Raylib.DrawTexturePro(Texture, source, dest, origin, rotation, Color.White);
        }


        public void Dispose() => Raylib.UnloadTexture(Texture);
    }
}
