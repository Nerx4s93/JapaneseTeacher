using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class ModuleHeader : Control
    {
        private const int _radius = 8;

        public Color BackgroundColor { get; set; }

        public string Theme { get; set; }
        public Color ThemeColor { get; set; }
        public string Description { get; set; }
        public Color DescriptionColor { get; set; }

        public ModuleHeader()
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
            DrawBackground(e.Graphics);
            DrawText(e.Graphics);
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
            int x = Width * percentX / 100;
            int y = Height * percentY / 100;
            return new Point(x, y);
        }
    }
}
