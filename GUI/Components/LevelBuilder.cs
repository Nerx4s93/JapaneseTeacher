using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.GUI.Components
{
    internal class LevelBuilder
    {
        private readonly Form _form;

        public LevelBuilder(Form form)
        {
            _form = form;
            _form.Resize += Form_Resize;
        }

        private bool _update;

        private Theme _theme;
        private string _levelId;

        private FlatProgressBar _flatProgressBar;

        public void LoadLevel(Theme theme, string levelId)
        {
            _update = true;
            _theme = theme;
            _levelId = levelId;
            AdjustControls();
        }

        public void StopHandle()
        {
            _update = false;
        }

        private void AdjustControls()
        {
            _flatProgressBar = new FlatProgressBar();
            _flatProgressBar.MaxValue = 30;
            _flatProgressBar.Tag = 2;

            _form.Controls.Add(_flatProgressBar);
            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            _flatProgressBar.Size = new Size(_form.Width * 8 / 10, 20);
            _flatProgressBar.Location = new Point((_form.Size.Width - _flatProgressBar.Width) / 2, 20);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (_update)
            {
                MoveControls();
            }
        }
    }
}
