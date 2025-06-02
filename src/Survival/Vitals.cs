namespace Game2D.Survival
{
    public class Vitals
    {
        public int MaxHealth;
        public int Health;
        public int Hunger;
        public int Thirst;

        public float Temperature;
        public float TempLossRate = 0.1f;
        public float TempGainRate = 0.4f;
        public float MinTemp = 33f;
        public float MaxTemp = 40f;
        public double LastTempDamageTime;

        private int _heatZoneCount = 0;
        public bool IsNearToHeatSource => _heatZoneCount > 0;

        public void EnterHeatZone()
        {
            _heatZoneCount++;
        }

        public void ExitHeatZone()
        {
            _heatZoneCount = Math.Max(0, _heatZoneCount - 1);
        }

        public Vitals(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = MaxHealth;
            Hunger = 100;
            Thirst = 100;
            Temperature = 37;
        }

        public void TakeDamage(int health)
        {
            Health = Math.Clamp(Health - health, 0, MaxHealth);
        }

        public void GiveTemperature(float temperature)
        {
            Temperature = Math.Clamp(Temperature + temperature, MinTemp, MaxTemp);
        }

        public void Update(float ambientTemp)
        {
            float time = Raylib.GetFrameTime() * 0.01f;
            float tempDelta = (ambientTemp - Temperature);

            if (ambientTemp > 30)
                Temperature -= tempDelta * TempGainRate * time;
            else if (ambientTemp < 20)
                Temperature += tempDelta * TempLossRate * time;

            if ((Temperature <= MinTemp || Temperature >= MaxTemp) && LastTempDamageTime < Raylib.GetTime())
            {
                TakeDamage(1);
                LastTempDamageTime = Raylib.GetTime() + 2;
            }

            Temperature = Math.Clamp(Temperature, MinTemp, MaxTemp);
            Health = Math.Clamp(Health, 0, MaxHealth);
        }
    }
}
