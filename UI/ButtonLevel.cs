using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class ButtonLevel : Control
    {
        private Pen _pen1;
        private Pen _pen2;
        private Brush _brush1;
        private Brush _brush2;
        private Brush _starBrush;

        private Color _defaultColor;
        private Color _defaultBottomColor;
        private Color _iconColor;

        private bool _mouseDown;

        public Color DefaultColor
        {
            get
            {
                return _defaultColor;
            }
            set
            {
                _defaultColor = value;
                _pen1 = new Pen(new SolidBrush(_defaultColor), 6);
                _brush1 = new SolidBrush(_defaultColor);
            }
        }

        public Color DefaultBottomColor
        {
            get
            {
                return _defaultBottomColor;
            }
            set
            {
                _defaultBottomColor = value;
                _pen2 = new Pen(new SolidBrush(_defaultBottomColor), 6);
                _brush2 = new SolidBrush(_defaultBottomColor);
            }
        }

        public Color StartColor
        {
            get
            {
                return _iconColor;
            }
            set
            {
                _iconColor = value;
                _starBrush = new SolidBrush(_iconColor);
            }
        }

        public float ComplitePercent { get; set; }

        public ButtonLevel()
        {
            DoubleBuffered = true;
            Size = new Size(100, 100);

            DefaultColor = Color.Lime;
            DefaultBottomColor = Color.Green;
            StartColor = Color.Cornsilk;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            int x = PercentToPixels(3);
            graphics.DrawEllipse(_pen2, x, x, Width - 2 * x, Height - 2 * x);
            graphics.DrawPie(_pen1, x, x, Width - 2 * x, Height - 2 * x, 0, 360f * ComplitePercent / 100f);

            x = PercentToPixels(20);
            if (_mouseDown)
            {
                graphics.FillEllipse(_brush1, x, x + x / 6, Width - 2 * x, Width - 2 * x);
                DrawStar(graphics, + x / 6);
            }
            else
            {
                graphics.FillEllipse(_brush2, x, x + x / 6, Width - 2 * x, Width - 2 * x);
                graphics.FillEllipse(_brush1, x, x - x / 6, Width - 2 * x, Width - 2 * x);
                DrawStar(graphics, 0);
            }
        }

        private void DrawStar(Graphics graphics, int deltaY)
        {
            int x = PercentToPixels(30);
            var point = new PointF(x, x + deltaY);
            var size = new Size(Width - 2 * x, Width - 2 * x);
            DrawingTool.FillStar(graphics, point, size, _starBrush);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _mouseDown = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _mouseDown = false;
            Invalidate();
        }

        private int PercentToPixels(int percent)
        {
            return Width * percent / 100;
        }
    }
}
