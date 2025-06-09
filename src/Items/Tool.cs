using Game2D.Entities;

namespace Game2D.Items
{
    public abstract class Tool : Item
    {
        protected double _primaryNextTime;
        protected double _secondaryNextTime;

        public virtual float PrimaryCooldown => 1.0f;
        public virtual float SecondaryCooldown => 1.0f;

        public Tool(Vector2 position) 
            : base(position)
        {
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            if(Parent is Player)
                UpdateControl();
        }

        protected virtual void UpdateControl()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                Primary();
            else if (Raylib.IsMouseButtonPressed(MouseButton.Right))
                Secondary();
        }

        public virtual bool CanPrimary() => _primaryNextTime < Raylib.GetTime();
        public virtual bool CanSecondary() => _secondaryNextTime < Raylib.GetTime();

        private void Primary()
        {
            if (!CanPrimary()) return;

            _primaryNextTime = Raylib.GetTime() + PrimaryCooldown;
            OnPrimary();
        }

        private void Secondary()
        {
            if (!CanSecondary()) return;

            _secondaryNextTime = Raylib.GetTime() + SecondaryCooldown;
            OnSecondary();
        }

        protected virtual void OnPrimary()
        {
        }

        protected virtual void OnSecondary()
        {

        }
    }
}
