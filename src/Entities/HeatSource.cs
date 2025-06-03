using Game2D.Classes;
using Game2D.Survival;

namespace Game2D.Entities
{
    public class HeatSource : Entity
    {
        public override EntityID EntityID => EntityID.HeatSource;

        public float Radius = 0;
        public float Strength = 1.0f;
        public bool IsEnabled = false;

        private readonly ZoneCircle _zone;

        public HeatSource(Vector2 position) 
            : base(position)
        {
            _zone = new ZoneCircle(
                radius: Radius,
                center: Position,
                filter: e => e != this && e.EntityID == EntityID.Player,
                onEnter: e => (e as SurvivalPlayer)?.Vitals.EnterHeatZone(),
                onExit: e => (e as SurvivalPlayer)?.Vitals.ExitHeatZone()
            );
        }

        protected override void OnUpdate()
        {
            _zone.IsEnabled = IsEnabled;
            _zone.Center = Position;
            _zone.Radius = Radius;
            _zone.Update(World.Entities.Values);

            float dt = Raylib.GetFrameTime() * 0.01f;
            foreach (var entity in _zone.EntitiesInside)
            {
                if (entity is not SurvivalPlayer player) 
                    continue;

                Vector2 diff = player.Position - Position;
                float heatFactor = 1f - diff.Length() / Radius;
                float heatAmount = Strength * heatFactor * dt;
                player.Vitals.GiveTemperature(heatAmount);
            }
        }

        protected override void OnDraw()
        {
            if(IsEnabled)
                _zone.Draw();

            Raylib.DrawCircle((int)_zone.Center.X, (int)_zone.Center.Y, 5, Color.Red);
        }
    }
}
