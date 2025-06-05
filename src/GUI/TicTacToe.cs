using Game2D.Render;

namespace Game2D.Gui.TTT
{
    public enum TicTacToeShape
    {
        None,
        X,
        Circle,
        Draft,
    };

    public class TicTacToe : Panel
    {
        public Dictionary<Vector2, Button> Buttons = new(9);
        public Dictionary<Vector2, TicTacToeShape> Shapes = new(9);
        public Dictionary<TicTacToeShape, int> Points = new(2);

        public TicTacToeShape CurrentShape = TicTacToeShape.X;
        private double _resetTime;
        private TicTacToeShape _winner = TicTacToeShape.None;

        public TicTacToe(
            Panel parent,
            float width, 
            float height, 
            Action<Panel> onDraw = null
            ) : base(parent, width, height, onDraw)
        {
            var spacing = 15;
            var buttonsWidth = (200 + spacing) * 3;
            var offset = new Vector2(GUI.SrcW() / 2, GUI.SrcH() / 2) - new Vector2((buttonsWidth / 2), (buttonsWidth / 2));

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Shapes.Add(new Vector2(i, j), TicTacToeShape.None);
                }
            }

            Points.Add(TicTacToeShape.X, 0);
            Points.Add(TicTacToeShape.Circle, 0);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var button = new Button(this, 200, 200, onDraw: (panel) =>
                    {
                        var pos = GetButtonPos(panel as Button);
                        Raylib.DrawRectangleRounded(panel.Rect, 0.1f, 6, new Color(255, 255, 255, 255));

                        if(Shapes[pos] == TicTacToeShape.X)
                        {
                            Raylib.DrawLineEx(
                                new Vector2(panel.Rect.X + 10, panel.Rect.Y + 10),
                                new Vector2(panel.Rect.X + panel.Rect.Width - 10, panel.Rect.Y + panel.Rect.Height - 10),
                                GUI.SS(6.0f), Color.Red);

                            Raylib.DrawLineEx(
                                new Vector2(panel.Rect.X + 10, panel.Rect.Y + panel.Rect.Height - 10),
                                new Vector2(panel.Rect.X + panel.Rect.Width - 10, panel.Rect.Y + 10),
                                GUI.SS(6.0f), Color.Red);
                        }
                        else if (Shapes[pos] == TicTacToeShape.Circle)
                        {
                            Raylib.DrawCircle( 
                                (int)(panel.Rect.X + (panel.Rect.Width / 2)), (int)(panel.Rect.Y + (panel.Rect.Height / 2)), (
                                panel.Rect.Width/2) - GUI.SS(25), Color.Blue);

                            Raylib.DrawCircle( 
                                (int)(panel.Rect.X + (panel.Rect.Width / 2)), (int)(panel.Rect.Y + (panel.Rect.Height / 2)), 
                                (panel.Rect.Width/2) - GUI.SS(30), Color.White);
                        }
                    });

                    var x = (200 + spacing) * i;
                    var y = (200 + spacing) * j;

                    button.Position = new Vector2(x, y) + offset;
                    button.OnPress += (panel, user, button) =>
                    {
                        if (_winner != TicTacToeShape.None) return;

                        var pos = GetButtonPos(panel);
                        if (Shapes[pos] != TicTacToeShape.None) return;
                        Shapes[pos] = CurrentShape;

                        CheckShape(CurrentShape);

                        if (CurrentShape == TicTacToeShape.X)
                            CurrentShape = TicTacToeShape.Circle;
                        else
                            CurrentShape = TicTacToeShape.X;
                    };

                    Buttons.Add(new Vector2(i, j), button);
                }
            }
        }

        private Vector2 GetButtonPos(Button button)
        {
            var pos = Vector2.Zero;
            foreach (var pair in Buttons)
                if (pair.Value == button)
                    pos = pair.Key;

            return pos;
        }

        private void CheckShape(TicTacToeShape shape)
        {
            var win = false;

            for (int z = 0; z < 2; z++)
            {
                var list = new List<int>();
                for (int x = 0; x < 3; x++)
                {
                    var count = 0;
                    for (int y = 0; y < 3; y++)
                    {
                        if(z == 0)
                        {
                            if (Shapes[new Vector2(x, y)] == shape)
                                count++;
                        }
                        else
                        {
                            if (Shapes[new Vector2(y, x)] == shape)
                                count++;
                        }
                    }
                    list.Add(count);
                }

                foreach (var count in list)
                {
                    if (count == 3)
                    {
                        win = true;
                        break;
                    }
                }
            }

            // x
            //   x
            //     x
            var countDig = 0;
            for (int x = 0; x < 3; x++)
            {
                if (Shapes[new Vector2(x, x)] == shape)
                    countDig++;
            }

            //    x
            //  x 
            //x
            var countDig2 = 0;
            for (int x = 0; x < 3; x++)
            {
                if (Shapes[new Vector2(2 - x, x)] == shape)
                    countDig2++;
            }

            if (countDig == 3 || countDig2 == 3)
                win = true;

            //-------------------------------------------------
            if (win)
            {
                _resetTime = Raylib.GetTime() + 2.0f;
                _winner = shape;
                Points[shape]++;
                return;
            }

            var countNone = 0;
            foreach (var pair in Shapes)
            {
                if(pair.Value == TicTacToeShape.None)
                    countNone++;
            }

            if (countNone == 0)
            {
                _resetTime = Raylib.GetTime() + 2.0f;
                _winner = TicTacToeShape.Draft;
            }
        }

        protected override void OnUpdate()
        {
            if(_resetTime < Raylib.GetTime() && _winner != TicTacToeShape.None)
            {
                Shapes.ToList().ForEach(pair => Shapes[pair.Key] = TicTacToeShape.None);
                _winner = TicTacToeShape.None;
            }
        }

        protected override void OnDraw()
        {
            Raylib.DrawRectangleRec(Rect, new Color(25, 25, 25, 255));
            var text = "TURN: " + CurrentShape;
            var color = Color.White;

            if(_winner == TicTacToeShape.Draft)
            {
                text = "DRAFT";
                color = Color.Red;
            }
            else if (_winner != TicTacToeShape.None)
            {
                text = "WINNER: " + _winner;
                color = Color.Green;
            }

            Render.Draw.AlignedText(text, 
                new Vector2(GUI.SrcW() / 2, 100), 
                "PixelBold", 
                70, color, hAlign: HorizontalAlign.Center, vAlign: VerticalAlign.Center);

            var num = 0;
            foreach (var pair in Points)
            {
                Render.Draw.AlignedText(pair.Key+": "+pair.Value,
                    new Vector2(25, 25 + (60 * num)),
                    "Pixel",
                    70, Color.White, hAlign: HorizontalAlign.Left, vAlign: VerticalAlign.Top);

                num++;
            }
        }
    }
}
