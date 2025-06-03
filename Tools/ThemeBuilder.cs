using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Tools
{
    internal class ThemeBuilder
    {
        private readonly Form _form;

        public ThemeBuilder(Form form)
        {
            _form = form;
            _form.Resize += Form_Resize;
        }

        private ModuleHeader _moduleHeader = new ModuleHeader();
        private List<ButtonLevel> _buttonLevels = new List<ButtonLevel>();

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
            _moduleHeader.Dispose();
            foreach (var buttonLevel in _buttonLevels)
            {
                buttonLevel.Dispose();
            }

            _moduleHeader = new ModuleHeader();
            _moduleHeader.Size = new Size(700, 110);
            _moduleHeader.Tag = 1;
            _moduleHeader.Theme = theme.Name;
            _moduleHeader.Description = theme.Description;

            var levels = theme.GetLevels();
            foreach (var level in levels)
            {
                var button = new ButtonLevel();
                _buttonLevels.Add(button);
                _form.Controls.Add(button);
            }

            _form.Controls.Add(_moduleHeader);
            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            var startX = (_form.Size.Width - _moduleHeader.Size.Width) / 2;
            _moduleHeader.Location = new Point(startX, 10);

            var bodyStartX = startX + _moduleHeader.Size.Width / 2 - 200;
            var bodyStartY = 150;
            var dx = 90;
            var dy = 150;
            
            for (int i = 0; i < _buttonLevels.Count; i++)
            {
                var button = _buttonLevels[i];

                int x = bodyStartX;
                int y = bodyStartY + dy * i;

                int t = i % 4;
                switch(t)
                {
                    case 1:
                    case 3:
                        {
                            x += dx;
                            break;
                        }
                    case 2:
                        {
                            x += dx * 2;
                            break;
                        }
                }

                button.Location = new Point(x, y);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }
    }
}
