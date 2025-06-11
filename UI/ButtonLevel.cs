using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using JapaneseTeacher.Tools;

namespace JapaneseTeacher.UI
{
    internal class ButtonLevel : Control
    {
        private readonly Brush _noActiveBodyColor = new SolidBrush(Color.FromArgb(229, 229, 229));
        private readonly Brush _noActiveBottomBodyColor = new SolidBrush(Color.FromArgb(183, 183, 183));
        private readonly Brush _noActiveIconBodyColor = new SolidBrush(Color.FromArgb(175, 175, 175));

        private bool _active = false;

        private Pen _pen1;
        private Pen _pen2;
        private Brush _brush1;
        private Brush _brush2;
        private Brush _starBrush;

        private Color _defaultColor;
        private Color _defaultBottomColor;
        private Color _iconColor;

        private bool _mouseDown;

        private LevelInformation _buttonLevelInformation;

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

        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                _buttonLevelInformation.Active = value;
            }
        }

        public string Level { get; set; }
        
        public Color DescriptionActiveBackgroundColor
        {
            get
            {
                return _buttonLevelInformation.ActiveBackgroundColor;
            }
            set
            {
                _buttonLevelInformation.ActiveBackgroundColor = value;
            }
        }

        public int CompliteSublevels
        {
            get
            {
                return _buttonLevelInformation.CompliteSublevels;
            }
            set
            {
                _buttonLevelInformation.CompliteSublevels = value;
                Invalidate();
            }
        }

        public int TotalSublevels
        {
            get
            {
                return _buttonLevelInformation.TotalSublevels;
            }
            set
            {
                _buttonLevelInformation.TotalSublevels = value;
                Invalidate();
            }
        }


        public string Description
        {
            get
            {
                return _buttonLevelInformation.Text;
            }
            set
            {
                _buttonLevelInformation.Text = value;
            }
        }

        public ButtonLevel()
        {
            DoubleBuffered = true;
            Size = new Size(100, 100);

            DefaultColor = Color.Lime;
            DefaultBottomColor = Color.Green;
            StartColor = Color.Cornsilk;

            _buttonLevelInformation = new LevelInformation
            {
                Visible = false,
                Size = new Size(360, 175)
            };
            _buttonLevelInformation.SetParent(this);
        }

        #region ButtonLevelInformation

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Parent.Controls.Add(_buttonLevelInformation);

            Parent.MouseDown += Control_MouseDown;
            var controls = Parent.Controls;
            foreach (Control control in controls)
            {
                control.MouseDown += Control_MouseDown;
            }
            MouseDown -= Control_MouseDown;
        }

        protected override void DestroyHandle()
        {
            base.DestroyHandle();
            _buttonLevelInformation.Dispose();
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            _buttonLevelInformation.Visible = false;
        }

        #endregion

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            var x = (Location.X + Width) / 2;
            var y = Location.Y + Height + 10;
            _buttonLevelInformation.Location = new Point(x, y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            var x = PercentToPixels(3);
            if (Active)
            {
                graphics.DrawEllipse(_pen2, x, x, Width - 2 * x, Height - 2 * x);
                graphics.DrawPie(_pen1, x, x, Width - 2 * x, Height - 2 * x, 0, 360f * ComplitePercent / 100f);
            }

            var brush1 = Active ? _brush1 : _noActiveBodyColor;
            var brush2 = Active ? _brush2 : _noActiveBottomBodyColor;

            x = PercentToPixels(20);
            if (_mouseDown)
            {
                graphics.FillEllipse(brush1, x, x + x / 6, Width - 2 * x, Width - 2 * x);
                DrawStar(graphics, + x / 6);
            }
            else
            {
                graphics.FillEllipse(brush2, x, x + x / 6, Width - 2 * x, Width - 2 * x);
                graphics.FillEllipse(brush1, x, x - x / 6, Width - 2 * x, Width - 2 * x);
                DrawStar(graphics, 0);
            }
        }

        private void DrawStar(Graphics graphics, int deltaY)
        {
            var brush = Active ? _starBrush : _noActiveIconBodyColor;

            var x = PercentToPixels(30);
            var point = new PointF(x, x + deltaY);
            var size = new Size(Width - 2 * x, Width - 2 * x);
            DrawingTool.FillStar(graphics, point, size, brush);
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

        protected override void OnMouseClick(MouseEventArgs e)
        {
            _buttonLevelInformation.Visible = !_buttonLevelInformation.Visible;
        }

        private int PercentToPixels(int percent)
        {
            return Width * percent / 100;
        }

        public void MessageLoadLevel()
        {
            OnLoadLevel?.Invoke(this);
        }

        public delegate void LoadLevel(object sender);
        public event LoadLevel OnLoadLevel;
    }
}
