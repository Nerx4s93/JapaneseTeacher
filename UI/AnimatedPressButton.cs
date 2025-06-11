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
        private const int _paddingX = 13;
        private const int _paddingY = 25;

        private readonly Brush _noActiveBackgroundBrush = new SolidBrush(Color.FromArgb(229, 229, 229));
        private readonly Brush _noActiveTextBrush = new SolidBrush(Color.FromArgb(175, 175, 175));

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
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

            BackColor = Color.Transparent;
            DoubleBuffered = true;
            Font = new Font("Microsoft Sans Serif", 16);
            ForeColor = Color.White;

            Active = true;
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

            using (var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius))
            {
                if (_active)
                {
                    if (_mouseDown)
                    {
                        graphics.FillPath(_acriveBackgroundBrush, path);
                    }
                    else
                    {
                        graphics.FillPath(_acriveBottomBackgroundBrush, path);

                        rectangle.Y = 0;
                        using (var topPath = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius))
                        {
                            graphics.FillPath(_acriveBackgroundBrush, topPath);
                        }
                    }
                }
                else
                {
                    rectangle.Y = 0;
                    using (var topPath = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius))
                    {
                        graphics.FillPath(_noActiveBackgroundBrush, topPath);
                    }
                }
            }
        }

        private void DrawText(Graphics graphics)
        {
            var textSize = graphics.MeasureString(Text, Font);
            var x = (Width - textSize.Width) / 2;
            var y = (Height - textSize.Height) / 2;

            if (_active)
            {
                var dy = _mouseDown ? 0 : -3;
                using (var brush = new SolidBrush(ForeColor))
                {
                    graphics.DrawString(Text, Font, brush, x, y + dy);
                }
            }
            else
            {
                graphics.DrawString(Text, Font, _noActiveTextBrush, x, y);
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

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (CustomAutoSize && !string.IsNullOrEmpty(Text))
            {
                var size = TextRenderer.MeasureText(Text, Font);

                int width = size.Width + _paddingX * 2;
                int height = size.Height + _paddingY;

                Size = new Size(width, height);
            }
        }
    }
}
