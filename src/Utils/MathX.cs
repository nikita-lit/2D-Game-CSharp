namespace Game2D.Utils
{
    public static class MathX
    {
        public static float Map(float input, float inputMin, float inputMax, float min, float max)
        {
            return min + (input - inputMin) * (max - min) / (inputMax - inputMin);
        }

        public static float AngleLerp(float from, float to, float t)
        {
            float delta = (to - from + 540f) % 360f - 180f;
            return from + delta * t;
        }

        public static Vector2 RotateVector(Vector2 v, float angleDeg)
        {
            float rad = angleDeg * Raylib.DEG2RAD;
            float cos = MathF.Cos(rad);
            float sin = MathF.Sin(rad);
            return new Vector2(
                v.X * cos - v.Y * sin,
                v.X * sin + v.Y * cos
            );
        }
    }
}
