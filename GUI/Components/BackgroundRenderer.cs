using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JapaneseTeacher.GUI.Components
{
    internal class BackgroundRenderer
    {
        private readonly Form _form;
        private readonly Image _image;

        private readonly Random _random = new Random();
        private readonly int _updateIntervalMs = 16;

        public BackgroundRenderer(Form form, Image image)
        {
            if (form == null)
            {
                throw new ArgumentNullException("Form = null");
            }
            if (image == null)
            {
                throw new ArgumentNullException("Image = null");
            }

            _form = form;
            _image = new Bitmap(image);
            _imageDraw = new Bitmap(image);

            _form.Shown += Form_Shown;
            _form.FormClosing += Form_FormClosing;
            _form.ResizeEnd += Form_ResizeEnd;
            _form.Paint += Form_Paint;
        }

        private Image _imageDraw;

        private volatile bool _isRunning = true;

        private PointF _backgroundLocation = new PointF(0, 0);
        private Point _maxLocation = new Point(0, 0);
        private Point _direction = new Point(-1, -1);
        private PointF _speed = new PointF(0.1f, 0.1f);

        private void Form_Shown(object sender, EventArgs e)
        {
            StartBackgroundTask();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isRunning = false;
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            if (_imageDraw != null)
            {
                _imageDraw.Dispose();
                _imageDraw = null;
            }

            int targetWidth = (int)(_form.ClientSize.Width * 1.2);
            int targetHeight = (int)(_form.ClientSize.Height * 1.2);

            float imageRatio = (float)_image.Width / _image.Height;
            float targetRatio = (float)targetWidth / targetHeight;

            int newWidth, newHeight;

            if (targetRatio > imageRatio)
            {
                newWidth = targetWidth;
                newHeight = (int)(newWidth / imageRatio);
            }
            else
            {
                newHeight = targetHeight;
                newWidth = (int)(newHeight * imageRatio);
            }

            _imageDraw = new Bitmap(_image, newWidth, newHeight);
            _backgroundLocation = new PointF(0, 0);
            _maxLocation = new Point(newWidth, newHeight);
            _form.Invalidate();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(_imageDraw, _backgroundLocation);
        }

        private void StartBackgroundTask()
        {
            Task.Run(async () =>
            {
                while (_isRunning)
                {
                    _backgroundLocation.X += _speed.X * _direction.X;
                    _backgroundLocation.Y += _speed.Y * _direction.Y;

                    if (_backgroundLocation.X < 0)
                    {
                        _direction.X *= -1;
                    }
                    if (_backgroundLocation.Y < 0)
                    {
                        _direction.Y *= -1;
                    }

                    if (_backgroundLocation.X > _maxLocation.X)
                    {
                        _direction.X *= -1;
                    }
                    if (_backgroundLocation.Y > _maxLocation.Y)
                    {
                        _direction.Y *= -1;
                    }

                    if (_isRunning && !_form.IsDisposed)
                    {
                        try
                        {
                            _form.BeginInvoke((Action)(() => _form.Invalidate()));
                        }
                        catch (ObjectDisposedException)
                        {
                            _isRunning = false;
                        }
                    }

                    await Task.Delay(_updateIntervalMs);
                }
            });
        }
    }
}
