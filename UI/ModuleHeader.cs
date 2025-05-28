using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class ModuleHeader : Control
    {
        private const int _radius = 8;

        public ModuleHeader()
        {
            DoubleBuffered = true;
            BackgroundColor = Color.Orange;
        }

        public Color BackgroundColor {  get; set; }
        public string Theme { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;
            DrawBackground(graphics);
        }

        private void DrawBackground(Graphics graphics)
        {
            var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius);

            using (var brush = new SolidBrush(BackgroundColor))
            {
                graphics.FillPath(brush, path);
            }
        }
    }
}
