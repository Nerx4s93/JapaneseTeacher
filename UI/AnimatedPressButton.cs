using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class AnimatedPressButton : Control
    {
        private const int _radius = 8;

        private bool _customAutoSize;

        private bool _active;
        private Color _acriveBackgroundColor;
        private Brush _acriveBackgroundBrush;
        private Brush _acriveBottomBackgroundBrush;

        private bool _mouseDown = false;

        public bool CustomAutoSize
        {
            get
            {
                return _customAutoSize;
            }
            set
            {
                _customAutoSize = value; Invalidate();
            }
        }

        public bool Acrive
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
                return _acriveBackgroundColor;
            }
            set
            {
                _acriveBackgroundColor = value;
                _acriveBackgroundBrush = new SolidBrush(_acriveBackgroundColor);

                var r = Math.Max(value.R - 70, 0);
                var g = Math.Max(value.G - 70, 0);
                var b = Math.Max(value.B - 70, 0);
                var color = Color.FromArgb(r, g, b);
                _acriveBottomBackgroundBrush = new SolidBrush(color);
            }
        }

        public AnimatedPressButton()
        {
            DoubleBuffered = true;
            Font = new Font("Microsoft Sans Serif", 14);
            ForeColor = Color.White;
            CustomAutoSize = true;
            ActiveBackgroundColor = Color.Lime;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            DrawBody(graphics);
            DrawText(graphics);
        }

        private void DrawBody(Graphics graphics)
        {
            var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            rectangle.Y = 6;
            rectangle.Height -= 6;

            if (_mouseDown)
            {
                var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius);
                graphics.FillPath(_acriveBackgroundBrush, path);
            }
            else
            {
                var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius);
                graphics.FillPath(_acriveBottomBackgroundBrush, path);

                rectangle.Y = 0;
                path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius);
                graphics.FillPath(_acriveBackgroundBrush, path);
            }
        }

        private void DrawText(Graphics graphics)
        {
            var textSize = graphics.MeasureString(Text, Font);
            var x = (Width - textSize.Width) / 2;
            var y = (Height - textSize.Height) / 2 - 3;

            using (var brush = new SolidBrush(ForeColor))
            {
                graphics.DrawString(Text, Font, brush, x, y);
            }
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

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (_customAutoSize)
            {
                using (var graphics = CreateGraphics())
                {
                    var textSize = graphics.MeasureString(Text, Font);
                    Size = new Size((int)textSize.Width + 20, (int)textSize.Height + 30);
                }
            }
        }
    }
}
