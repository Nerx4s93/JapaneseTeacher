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

        
        private void Form_Shown(object sender, EventArgs e)
        {
            StartBackgroundTask();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            if (_imageDraw != null)
            {
                _imageDraw.Dispose();
                _imageDraw = null;
            }

            int targetWidth = (int)(_form.Width * 1.3);
            int targetHeight = (int)(_form.Height * 1.3);

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
            _form.Invalidate();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            graphics.DrawImage(_imageDraw, backgroundX, backgroundY);
        }

        private void StartBackgroundTask()
        {

        }
    }
}
