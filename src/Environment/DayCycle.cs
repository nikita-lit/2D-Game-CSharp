namespace Game2D.Environment
{
    public enum TimeOfDay
    {
        Morning,
        Day,
        Evening,
        Night,
    }

    public class DayCycle
    {
        public DateTime Time { get; set; }
        public DateTime Time2 { get; set; }
        public float TimeScale = 1f;

        public DayCycle(DateTime time)
        {
            Time = time;
        }

        public void Update()
        {
            Time = Time.AddSeconds(Raylib.GetFrameTime() * TimeScale);
        }

        public TimeOfDay GetTimeOfDay()
        {
            if (Time.Hour >= 0 && Time.Hour <= 5)
                return TimeOfDay.Night;
            else if (Time.Hour >= 6 && Time.Hour < 12)
                return TimeOfDay.Morning;
            else if(Time.Hour >= 12 && Time.Hour < 17)
                return TimeOfDay.Day;
            else if (Time.Hour >= 17 && Time.Hour <= 23)
                return TimeOfDay.Evening;

            return TimeOfDay.Day;
        }
    }
}
