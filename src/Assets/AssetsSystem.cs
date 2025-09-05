using System.IO;

namespace Game2D.Assets
{
    public static class AssetsSystem
    {
        public static Dictionary<string, Texture2D> Textures = new();

        public static Texture2D LoadTexture(string texPath)
        {
            string path = "../../assets/textures/" + texPath.ToLower();
            bool isValid = Textures.TryGetValue(path, out Texture2D value);
            bool isGPUValid = Raylib.IsTextureValid(value);

            if (!isValid && isGPUValid)
                throw new Exception("Texture loaded in GPU but not in dictionary");

            if (isValid && isGPUValid)
                return value;

            var texture = Raylib.LoadTexture(path);
            Textures.Add(path, texture);
            return texture;
        }

        public static Dictionary<string, Font> Fonts = new();
        public static Font GetFont(string name) => Fonts.FirstOrDefault(pair => pair.Key == name).Value;

        public static Font RegisterFont(string fontName, string fileName)
        {
            string path = "../../assets/resources/fonts/" + fileName.ToLower();
            bool isValid = Fonts.TryGetValue(path, out Font value);
            bool isFValid = Raylib.IsFontValid(value);

            if (!isValid && isFValid)
                throw new Exception("Font loaded but not in dictionary");

            if (isValid && isFValid)
                return value;

            var font = Raylib.LoadFont(path);
            Fonts.Add(fontName, font);
            return font;
        }

        public static string GetScriptPath(string fileName)
        {
            return "../../assets/scripts/" + fileName;
        }
    }
}
