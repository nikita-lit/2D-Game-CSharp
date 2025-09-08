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

        public void Draw()
        {
            Raylib.DrawRectangle(-500, -500, 1000, 1000, Color.DarkGray);
            Raylib.DrawRectanglePro(new Rectangle(200, 200, 100, 100), new Vector2(50, 50), 30f * (float)Raylib.GetTime(), Color.DarkBlue);

            foreach (var pair in Entities.OrderBy(e => e.Value.Position.Y))
            {
                pair.Value.Draw();
            }
        }
    }
}
