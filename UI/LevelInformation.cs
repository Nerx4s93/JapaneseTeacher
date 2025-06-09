using JapaneseTeacher.Tools;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace JapaneseTeacher.UI
{
    internal class LevelInformation : Control
    {
        private const int _radius = 8;
        private const int _padingX = 10;
        private const int _padingY = 10;
        private const string _noActiveText = "Пройдите все уровни выше,\nчтобы открыть доступ!";

        private readonly Brush _noActiveBackgroundBrush = new SolidBrush(Color.FromArgb(247, 247, 247));
        private readonly Brush _noActiveOverlayBrush = new SolidBrush(Color.FromArgb(229, 229, 229));
        private readonly Brush _noActiveTextBrush = new SolidBrush(Color.FromArgb(175, 175, 175));

        private ButtonLevel _parent;
        private AnimatedPressButton _animatedPressButton;

        private bool _active;
        private Color _activeBackgroundColor;
        private Brush _activeBackgroundBrush;
        private int _compliteSublevels;
        private int _totalSublevels;

        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (_animatedPressButton != null)
                {
                    _animatedPressButton.Active = value;
                    _animatedPressButton.Text = value ? "Начать" : "НЕДОСТУПНО";
                }

                _active = value;
                Invalidate();
            }
        }

        public Color ActiveBackgroundColor
        {
            get
            {
                return _activeBackgroundColor;
            }
            set
            {
                _activeBackgroundColor = value;
                _activeBackgroundBrush?.Dispose();
                _activeBackgroundBrush = new SolidBrush(value);
                Invalidate();
            }
        }

        public int CompliteSublevels
        {
            get
            {
                return _compliteSublevels;
            }
            set
            {
                _compliteSublevels = value;
                Invalidate();
            }
        }

        public int TotalSublevels
        {
            get
            {
                return _totalSublevels;
            }
            set
            {
                _totalSublevels = value;
                Invalidate();
            }
        }

        public LevelInformation()
        {
            DoubleBuffered = true;
            ForeColor = Color.White;
            Font = new Font("Segoe UI Emoji", 16);

            ActiveBackgroundColor = Color.Orange;

            UpdateAnimatedButtonPosition();
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();
            _animatedPressButton = new AnimatedPressButton
            {
                CustomAutoSize = false
            };
            Controls.Add(_animatedPressButton);
        }

        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            _animatedPressButton.Dispose();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateAnimatedButtonPosition();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            DrawBackground(graphics);
            DrawTitle(graphics);
            DrawText(graphics);
            UpdateAnimatedButtonPosition();
        }

        private void DrawBackground(Graphics graphics)
        {
            var rectangle = RoundedRectanglePathCreator.GetRecrangleWithSize(Size);
            var path = RoundedRectanglePathCreator.GetRoundRectanglePath(rectangle, _radius, _radius);

            var brush = _active ? _activeBackgroundBrush : _noActiveBackgroundBrush;
            graphics.FillPath(brush, path);

            if (!_active)
            {
                var pen = new Pen(_noActiveOverlayBrush, 2)
                {
                    Alignment = PenAlignment.Inset
                };
                graphics.DrawPath(pen, path);
            }
        }

        private void DrawTitle(Graphics graphics)
        {
            using (var font = new Font(Font, FontStyle.Bold))
            {
                var brush = _active ? new SolidBrush(ForeColor) : _noActiveTextBrush;
                var point = new Point(_padingX, _padingY);

                graphics.DrawString(Text, font, brush, point);

                if (_active)
                {
                    brush.Dispose();
                }
            }
        }

        private void DrawText(Graphics graphics)
        {
            var titleHeight = TextRenderer.MeasureText(Text, new Font(Font, FontStyle.Bold)).Height;

            var text = _active ? GetActiveText() : _noActiveText;
            var brush = _active ? new SolidBrush(ForeColor) : _noActiveTextBrush;
            var point = new Point(_padingX, _padingY + titleHeight + _padingY / 2);

            graphics.DrawString(text, Font, brush, point);

            if (_active)
            {
                brush.Dispose();
            }
        }

        private string GetActiveText()
        {
            if (_compliteSublevels == _totalSublevels)
            {
                return "Вы уже прошли этот уровень!";
            }
            return $"Урок {_compliteSublevels + 1} из {_totalSublevels}";
        }

        private void UpdateAnimatedButtonPosition()
        {
            if (_animatedPressButton == null)
            {
                return;
            }

            var titleHeight = TextRenderer.MeasureText(Text, new Font(Font, FontStyle.Bold)).Height;
            var text = _active ? GetActiveText() : _noActiveText;
            var textHeight = TextRenderer.MeasureText(text, Font, new Size(Width - 2 * _padingX, 0), TextFormatFlags.WordBreak).Height;

            var buttonY = _padingY + titleHeight + _padingY / 2 + textHeight + _padingY / 2;

            _animatedPressButton.Width = Width - 2 * _padingX;
            _animatedPressButton.Height = 50;
            _animatedPressButton.Location = new Point(_padingX, buttonY);

            Height = _padingY * 5 / 2 + titleHeight + textHeight + _animatedPressButton.Height;
        }

        public void SetParent(ButtonLevel parent)
        {
            _parent = parent;
        }
    }
}
