using Game2D.Entities;
using Game2D.Items;

namespace Game2D.Survival
{
    public class SurvivalPlayer : Player
    {
        public Vitals Vitals;
        public Inventory Inventory;

        public SurvivalPlayer(Vector2 position) : base(position)
        {
            Vitals = new Vitals(100);
            Inventory = new Inventory(5);
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
