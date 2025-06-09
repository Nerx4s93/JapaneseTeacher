using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class LevelInformation : Control
    {
        private const int _radius = 8;
        private const string _noActiveText = "Пройдите все уровни выше,\nчтобы открыть доступ!";

        private readonly Brush _noActiveBackgroundBrush = new SolidBrush(Color.FromArgb(247, 247, 247));
        private readonly Brush _noActiveOverlayBrush = new SolidBrush(Color.FromArgb(229, 229, 229));
        private readonly Brush _noActiveTextBrush = new SolidBrush(Color.FromArgb(175, 175, 175));

        private ButtonLevel _parent;

        private bool _active;
        private Color _activeBackgroundColor;
        private Brush _activeBackgroundBrush;
        private int _compliteSublevels;
        private int _totalSublevels;

        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                Invalidate();
            }
        }

        public Color ActiveBackgroundColor
        {
            get
            {
                return _activeBackgroundColor;
            }
            set
            {
                _activeBackgroundColor = value;
                _activeBackgroundBrush?.Dispose();
                _activeBackgroundBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public int CompliteSublevels
        {
            get
            {
                return _compliteSublevels;
            }
            set
            {
                _compliteSublevels = value;
                Invalidate();
            }
        }

        public int TotalSublevels
        {
            get
            {
                return _totalSublevels;
            }
            set
            {
                _totalSublevels = value;
                Invalidate();
            }
        }

        public LevelInformation()
        {
            ForeColor = Color.White;
            Font = new Font("Segoe UI Emoji", 16);

            ActiveBackgroundColor = Color.Orange;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            DrawBackground(graphics);
            DrawTitle(graphics);
            DrawText(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius);

            var brush = _active ? _activeBackgroundBrush : _noActiveBackgroundBrush;
            graphics.FillPath(brush, path);

            if (!_active)
            {
                var pen = new Pen(_noActiveOverlayBrush, 2)
                {
                    Alignment = PenAlignment.Inset
                };
                graphics.DrawPath(pen, path);
            }
        }

        private void DrawTitle(Graphics graphics)
        {
            var font = new Font(Font, FontStyle.Bold);
            var brush = _active ? new SolidBrush(ForeColor) : _noActiveTextBrush;
            var point = new Point(10, 10);

            graphics.DrawString(Text, font, brush, point);
        }

        private void DrawText(Graphics graphics)
        {
            var text = _active ? GetActiveText() : _noActiveText;
            var brush = _active ? new SolidBrush(ForeColor) : _noActiveTextBrush;
            var point = new Point(10, 45);

            graphics.DrawString(text, Font, brush, point);
        }

        private string GetActiveText()
        {
            if (_compliteSublevels == _totalSublevels)
            {
                return "Вы уже прошли этот уровень!";
            }
            return $"Урок {_compliteSublevels + 1} из {_totalSublevels}";
        }

        public void SetParent(ButtonLevel parent)
        {
            _parent = parent;
        }
    }
}
