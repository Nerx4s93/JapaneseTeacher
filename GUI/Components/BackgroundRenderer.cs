using System.Drawing;
using System.Windows.Forms;

namespace JapaneseTeacher.GUI.Components
{
    internal class BackgroundRenderer
    {
        private readonly Form _form;
        private readonly Image _image;

        public BackgroundRenderer(Form form, Image image)
        {
            _form = form;
            _image = image;
        }
    }
}
