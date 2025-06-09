using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class ThemeHeader : Control
    {
        private const int _radius = 8;

        private Color _backgroundColor;
        private string _theme;
        private Color _themeColor;
        private string _description;
        private Color _descriptionColor;

        public Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
        }

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

        public Color ThemeColor
        {
            get
            {
                return _themeColor;
            }
            set
            {
                _themeColor = value;
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

        public Color DescriptionColor
        {
            get
            {
                return _descriptionColor;
            }
            set
            {
                _descriptionColor = value;
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
            ThemeColor = Color.FromArgb(255, 224, 179);
            Description = "Описание";
            DescriptionColor = Color.FromArgb(255, 255, 255);
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

            using (var brush = new SolidBrush(BackgroundColor))
            {
                graphics.FillPath(brush, path);
            }
        }

        private void DrawText(Graphics graphics)
        {
            var point = PercentToPixels(5, 20);
            using (var brush = new SolidBrush(ThemeColor))
            {
                graphics.DrawString(Theme, Font, brush, point);
            }

            point = PercentToPixels(5, 50);
            using (var brush = new SolidBrush(DescriptionColor))
            {
                graphics.DrawString(Description, Font, brush, point);
            }
        }

        private Point PercentToPixels(int percentX, int percentY)
        {
            var x = Width * percentX / 100;
            var y = Height * percentY / 100;
            return new Point(x, y);
        }
    }
}
