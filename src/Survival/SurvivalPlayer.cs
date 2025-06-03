using Game2D.Entities;
using Game2D.Interfaces;
using Game2D.Items;

namespace Game2D.Survival
{
    public class SurvivalPlayer : Player, IHasInventory
    {
        public Vitals Vitals;
        public Inventory Inventory { get; private set; }

        public SurvivalPlayer(Vector2 position) : base(position)
        {
            Vitals = new Vitals(100);
            Inventory = new Inventory(this, 5);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Vitals.Update(World.Weather.Temperature);
        }

        protected override void OnDraw()
        {
            base.OnDraw();
        }
    }
}
