using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class ThemeHeader : Control
    {
        private const int _radius = 8;

        private string _theme;
        private string _description;

        private Color _backgroundColor;
        private Color _descriptionColor;

        private Brush _backgroundBrush;
        private Brush _themeTextBrush;
        private Brush _descriptionTextBrush;

        public string Theme
        {
            get
            {
                return _theme;
            }
            set
            {
                _theme = value;
                Invalidate();
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                _backgroundBrush = new SolidBrush(value);

                _themeTextBrush?.Dispose();
                _themeTextBrush = new SolidBrush(ControlPaint.Light(value, 1.5f));

                Invalidate();
            }
        }

        public Color DescriptionTextColor
        {
            get
            {
                return _descriptionColor;
            }
            set
            {
                _descriptionColor = value;
                _descriptionTextBrush?.Dispose();
                _descriptionTextBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public ThemeHeader()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            BackColor = Color.Transparent;
            Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
            DoubleBuffered = true;

            BackgroundColor = Color.Orange;
            Theme = "Тема";
            Description = "Описание";
            DescriptionTextColor = Color.FromArgb(255, 255, 255);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            DrawBackground(graphics);
            DrawText(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius, 2);

            graphics.FillPath(_backgroundBrush, path);
        }

        private void DrawText(Graphics graphics)
        {
            var point = PercentToPixels(5, 20);
            graphics.DrawString(Theme, Font, _themeTextBrush, point);

            point = PercentToPixels(5, 50);
            graphics.DrawString(Description, Font, _descriptionTextBrush, point);
        }

        private Point PercentToPixels(int percentX, int percentY)
        {
            var x = Width * percentX / 100;
            var y = Height * percentY / 100;
            return new Point(x, y);
        }
    }
}
