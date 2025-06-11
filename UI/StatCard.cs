using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class StatCard : Control
    {
        private readonly Brush _whiteBrush;

        private string _title;

        private Color _bodyColor;

        private Brush _bodyBrush;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                Invalidate();
            }
        }

        public Color BodyColor
        {
            get
            {
                return _bodyColor;
            }
            set
            {
                _bodyColor = value;
                _bodyBrush = new SolidBrush(value);
            }
        }

        public StatCard()
        {
            Font = new Font("Segoe UI Emoji", 12f);

            _whiteBrush = new SolidBrush(Color.White);

            BodyColor = Color.Lime;
            Title = "Заголовок";
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Основной прямоугольник и фон
            var fullRect = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            var fullPath = RoundedRectanglePathCreator.GetRoundRectanglePath(fullRect, 5, 5);
            graphics.FillPath(_bodyBrush, fullPath);

            // Заголовок (сверху)
            var titleSize = graphics.MeasureString(Title, Font);
            var titlePos = new PointF((Width - titleSize.Width) / 2, 8);
            graphics.DrawString(Title, Font, _whiteBrush, titlePos);

            // Нижняя белая область
            var whiteHeight = fullRect.Height * 13 / 20;
            var whiteRect = new Rectangle(2, fullRect.Height - whiteHeight - 2, fullRect.Width - 4, whiteHeight);
            var whitePath = RoundedRectanglePathCreator.GetRoundRectanglePath(whiteRect, 5, 5);
            graphics.FillPath(_whiteBrush, whitePath);

            // Текст внутри белой области
            var valueFont = new Font(Font.FontFamily, 14.25f, FontStyle.Bold);
            var valueSize = graphics.MeasureString(Text, valueFont);
            var valueX = whiteRect.Left + (whiteRect.Width - valueSize.Width) / 2;
            var valueY = whiteRect.Top + (whiteRect.Height - valueSize.Height) / 2;
            graphics.DrawString(Text, valueFont, _bodyBrush, valueX, valueY);
        }
    }
}
