using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace JapaneseTeacher.UI
{
    internal class ContentDivider : Control
    {
        public ContentDivider()
        {
            Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold);
            ForeColor = Color.FromArgb(175, 175, 175);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using var brush = new SolidBrush(ForeColor);
            using var pen = new Pen(ForeColor, 2);

            var textSize = graphics.MeasureString(Text, Font);
            var textX = (Width - textSize.Width) / 2;
            var textY = (Height - textSize.Height) / 2;

            var lineY = Height / 2;

            graphics.DrawLine(pen, 0, lineY, textX - 10, lineY);
            graphics.DrawLine(pen, textX + textSize.Width + 10, lineY, Width, lineY);
            graphics.DrawString(Text, Font, brush, textX, textY);
        }
    }
}
