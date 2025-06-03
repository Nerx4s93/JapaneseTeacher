using System;
using System.Drawing;
using System.Windows.Forms;

namespace JapaneseTeacher.UI
{
    internal class ProgressBar : Control
    {
        private Color _filledColor;
        private Color _unfilledColor;
        private Brush _filledBrush;
        private Brush _unfilledBrush;

        public int MaxValue { get; set; }
        public int Value { get; set; }

        public Color FilledColor
        {
            get
            {
                return _filledColor;
            }
            set
            {
                _filledColor = value;
                _filledBrush = new SolidBrush(value);
            }
        }
        public Color UnfilledColor
        {
            get
            {
                return _unfilledColor;
            }
            set
            {
                _unfilledColor = value;
                _unfilledBrush = new SolidBrush(value);
            }
        }

        public ProgressBar()
        {
            DoubleBuffered = true;

            MaxValue = 100;
            FilledColor = Color.FromArgb(88, 204, 2);
            UnfilledColor = Color.FromArgb(229, 229, 229);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            float ratio = (MaxValue == 0 || Value <= 0) ? 0 : Math.Min(1, (float)Value / MaxValue);

            var filledWidth = Width * ratio;
            var unfilledWidth = Width - filledWidth;

            if (_filledBrush != null && _unfilledBrush != null)
            {
                graphics.FillRectangle(_filledBrush, 0, 0, filledWidth, Height);
                graphics.FillRectangle(_unfilledBrush, filledWidth, 0, unfilledWidth, Height);
            }
        }
    }
}
