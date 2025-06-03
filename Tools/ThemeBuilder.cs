using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Tools
{
    internal class ThemeBuilder
    {
        private readonly Form _form;

        private readonly ModuleHeader _moduleHeader = new ModuleHeader();

        public ThemeBuilder(Form form)
        {
            _form = form;

            _form.Controls.Add(_moduleHeader);
            _moduleHeader.Tag = 1;

            _form.Resize += Form_Resize;
        }

        private bool _update;

        public void Build(Theme theme)
        {
            _update = true;
            AdjustControls(theme);

        }

        public void StopHandle()
        {
            _update = false;
        }

        private void AdjustControls(Theme theme)
        {
            _moduleHeader.Theme = theme.Name;
            _moduleHeader.Description = theme.Description;
            _moduleHeader.Size = new Size(700, 110);

            Form_Resize(null, null);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            var x = (_form.Size.Width - _moduleHeader.Size.Width) / 2;
            _moduleHeader.Location = new Point(x, 10);
        }
    }
}
