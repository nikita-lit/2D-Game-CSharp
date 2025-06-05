using Game2D.Assets;

namespace Game2D
{
    public class Sprite
    {
        public Texture2D Texture;
        public float Width => Texture.Width;
        public float Height => Texture.Height;
        public float Size = 1.0f;

        public Sprite(string path)
        {
            Texture = AssetsSystem.LoadTexture(path);
        }

        public void Draw(Vector2 position, float size = -1.0f, float rotation = 0.0f)
        {
            var _size = Size;
            if(size != -1.0f)
                _size = size;

            var textureOffset = new Vector2(
                (Width * _size) / 2,
                (Height * _size) / 2
            );

            Raylib.DrawTextureEx(Texture, position - textureOffset, rotation, _size, Color.White);
        }
    }
}
