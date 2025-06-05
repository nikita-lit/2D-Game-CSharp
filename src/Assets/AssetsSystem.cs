namespace Game2D.Assets
{
    public static class AssetsSystem
    {
        public static Dictionary<string, Texture2D> Textures = new();

        public static Texture2D LoadTexture(string path)
        {
            if(Textures.TryGetValue(path, out Texture2D value))
                return value;

            var texture = Raylib.LoadTexture("../../assets/textures/" + path.ToLower());
            Textures.Add(path, texture);
            return texture;
        }

        public static Dictionary<string, Font> Fonts = new();
        public static Font GetFont(string name) => Fonts.FirstOrDefault(pair => pair.Key == name).Value;

        public static Font LoadFont(string fontName, string fileName)
        {
            var font = Raylib.LoadFont("../../assets/resources/fonts/"+fileName);
            Fonts.Add(fontName, font);
            return font;
        }

        public static string GetScriptPath(string fileName)
        {
            return "../../assets/scripts/" + fileName;
        }
    }
}
