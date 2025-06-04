using Game2D.Assets;

namespace Game2D
{
    public class Sprite
    {
        public Texture2D Texture;
        public float Width => Texture.Width;
        public float Height => Texture.Height;

        public Sprite(string path)
        {
            Texture = AssetsSystem.LoadTexture(path);
        }
    }
}
