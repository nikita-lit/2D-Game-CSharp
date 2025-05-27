namespace Game2D
{
    public static class MathX
    {
        public static float Map(float input, float inputMin, float inputMax, float min, float max)
        {
            return min + (input - inputMin) * (max - min) / (inputMax - inputMin);
        }
    }
}
