using Game2D.Entities;

namespace Game2D.Survival
{
    public class SurvivalPlayer : Player
    {
        public Vitals Vitals;

        public SurvivalPlayer(Vector2 position) : base(position)
        {
            Vitals = new Vitals(100);
        }

        public override void Update()
        {
            base.Update();
            Vitals.Update(World.Weather.Temperature);
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
