using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class RoundedButton : Control
    {
        private Color _currentColor;
        private bool _mouseDown;
        private bool _mouseEnter;

        private bool _customAutoSize;
        protected int _paddingX;
        protected int _paddingY;
        private int _radius;
        private Color _defaultColor;
        private Color _mouseDownColor;
        private Color _mouseEnterColor;

        public RoundedButton()
        {
            DoubleBuffered = true;

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            BackColor = Color.Transparent;

            ForeColor = Color.White;
            CustomAutoSize = true;
            Text = "RoundedButton";
            PaddingX = 30;
            PaddingY = 15;
            Radius = 5;
            DefaultColor = Color.FromArgb(50, 150, 220);
            MouseDownColor = Color.FromArgb(70, 170, 240);
            MouseEnterColor = Color.FromArgb(80, 180, 240);
        }

        public bool CustomAutoSize 
        {
            get { return _customAutoSize; }
            set { _customAutoSize = value; Invalidate(); }
        }
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; Invalidate(); }
        }
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = value; Invalidate(); }
        }
        public bool DrawText { get; set; } = true;

        public int PaddingX
        {
            get { return _paddingX; }
            set {  _paddingX = value; Invalidate(); }
        }
        public int PaddingY
        {
            get { return _paddingY; }
            set { _paddingY = value; Invalidate(); }
        }
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; Invalidate(); }
        }
        public Color DefaultColor
        {
            get { return _defaultColor; }
            set { _defaultColor = value; _currentColor = value; Invalidate(); }
        }
        public Color MouseDownColor
        {
            get { return _mouseDownColor; }
            set { _mouseDownColor = value; Invalidate(); }
        }
        public Color MouseEnterColor
        {
            get { return _mouseEnterColor; }
            set { _mouseEnterColor = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            DrawBackGround(graphics);
            if (DrawText)
            {
                DefaultDrawText(graphics);
            }
        }

        private void DrawBackGround(Graphics graphics)
        {
            var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, Radius, Radius);

            var brush = new SolidBrush(_currentColor);
            graphics.FillPath(brush, path);
        }

        private void DefaultDrawText(Graphics graphics)
        {
            var textSize = graphics.MeasureString(Text, Font);
            var textPosition = new PointF(
                (Width - textSize.Width) / 2,
                (Height - textSize.Height) / 2);

            using (var textBrush = new SolidBrush(ForeColor))
            {
                graphics.DrawString(Text, Font, textBrush, textPosition);
            }
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (CustomAutoSize && !string.IsNullOrEmpty(Text))
            {
                using (var g = CreateGraphics())
                {
                    var textSize = g.MeasureString(Text, Font);


                    int width = (int)Math.Ceiling(textSize.Width) + _paddingX * 2;
                    int height = (int)Math.Ceiling(textSize.Height) + _paddingY;

                    Size = new Size(width, height);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _currentColor = MouseDownColor;
            _mouseDown = true;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            _currentColor = _mouseEnter ? MouseEnterColor : DefaultColor;
            _mouseDown = false;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (!_mouseDown)
            {
                _currentColor = MouseEnterColor;
                _mouseEnter = true;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (!_mouseDown)
            {
                _currentColor = DefaultColor;
                _mouseEnter = false;
                Invalidate();
            }
        }
    }
}
