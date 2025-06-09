using Game2D.Entities;
using Game2D.Interfaces;
using Game2D.Items;
using Game2D.Utils;

namespace Game2D.Survival
{
    public enum LegsState
    {
        Stand,
        Left,
        Right,
    }

    public class SurvivalPlayer : Player, IHasInventory
    {
        private const float SIZE = 1.5f;

        public Vitals Vitals;
        public Sprite BodySprite;
        public Sprite BodySprite2;

        public Sprite LegsSprite;
        public Sprite LegsSprite2;
        public Sprite LegsSprite3;
        public Sprite LegsSprite4;
        public Sprite LegsSprite5;
        public Sprite LegsSprite6;

        private double _legTime = 0;
        private LegsState LegsState = LegsState.Stand;
        private bool _isBack = false;

        public SurvivalPlayer(Vector2 position) : base(position)
        {
            Vitals = new Vitals(100);
            BodySprite = new Sprite("player/player1.png");
            BodySprite2 = new Sprite("player/player2.png");

            LegsSprite = new Sprite("player/player_legs1.png");
            LegsSprite2 = new Sprite("player/player_legs2.png");
            LegsSprite3 = new Sprite("player/player_legs3.png");
            LegsSprite4 = new Sprite("player/player_legs4.png");
            LegsSprite5 = new Sprite("player/player_legs5.png");
            LegsSprite6 = new Sprite("player/player_legs6.png");
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            Vitals.Update(World.Weather.Temperature);

            var keys = (int)KeyboardKey.One;
            for (int i = keys; i < keys + INVENTORY_SLOTS_COUNT; i++)
            {
                if(Raylib.IsKeyPressed((KeyboardKey)i))
                    SelectedSlot = (i - keys);
            }

            if(Raylib.IsKeyPressed(KeyboardKey.Q))
                Inventory.DropItem(SelectedSlot);

            if(Velocity.Length() > 20)
            {
                if (_legTime < Raylib.GetTime())
                {
                    _legTime = Raylib.GetTime() + (Raylib.IsKeyDown(KeyboardKey.LeftShift) ? 0.15f : 0.25f);

                    if (LegsState == LegsState.Left)
                        LegsState = LegsState.Right;
                    else if (LegsState == LegsState.Right)
                        LegsState = LegsState.Left;
                    else
                        LegsState = LegsState.Left;
                }
            }
            else
                LegsState = LegsState.Stand;
        }

        protected override void OnDraw()
        {
            var legsSprite = (_isBack ? LegsSprite2 : LegsSprite);
            var sprite = (_isBack ? BodySprite2 : BodySprite);

            if (Velocity.Y > 0)
            {
                if (LegsState == LegsState.Left)
                    legsSprite = LegsSprite5;
                else if (LegsState == LegsState.Right)
                    legsSprite = LegsSprite6;

                _isBack = false;
            }
            else if (Velocity.Y < 0)
            {
                if (LegsState == LegsState.Left)
                    legsSprite = LegsSprite4;
                else if (LegsState == LegsState.Right)
                    legsSprite = LegsSprite3;

                _isBack = true;
            }
            else if (Velocity.X < 0 || Velocity.X > 0)
            {
                if (LegsState == LegsState.Left)
                    legsSprite = LegsSprite5;
                else if (LegsState == LegsState.Right)
                    legsSprite = LegsSprite6;

                _isBack = false;
            }

            var item = Inventory.Slots[SelectedSlot].Item;

            //if (item != null)
            //    sprite = BodySprite2;

            Rectangle source = new Rectangle(0, 0, sprite.Width, sprite.Height);
            Rectangle dest = new Rectangle(Position.X, Position.Y, sprite.Width * SIZE, sprite.Height * SIZE);
            Vector2 origin = new Vector2(dest.Width / 2, dest.Height / 2);

            if (item != null && _isBack)
                item.Sprite.Draw(Position - MathX.RotateVector(item.HoldOffset, Rotation), 1.2f, item.HoldRotation + Rotation, true);

            Raylib.DrawTexturePro(
                legsSprite.Texture, source, dest, origin, Rotation, Color.White
            );

            if (item != null && !_isBack)
                item.Sprite.Draw(Position - MathX.RotateVector(item.HoldOffset, Rotation), 1.2f, item.HoldRotation + Rotation, true);

            Raylib.DrawTexturePro(
                sprite.Texture, source, dest, origin, Rotation, Color.White
            );
        }
    }
}
