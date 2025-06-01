using Game2D.Entities;

namespace Game2D.Environment
{
    public class World
    {
        public Dictionary<Guid, Entity> Entities = new();
        public Weather Weather;

        public World() 
        {
            Weather = new Weather() {
                Temperature = 25,
            };
        }
    }
}
