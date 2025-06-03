using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Components
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
            bool active = true;
            foreach (var level in levels)
            {
                var button = new ButtonLevel();
                button.Tag = 1;
                button.Active = active;

                if (level.CompletedSublevels != level.TotalSublevels)
                {
                    active = false;
                }

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
            var dx = 50;
            var dy = 110;

            int[] xOffsets = { 0, dx, 2 * dx, dx };

            for (int i = 0; i < _buttonLevels.Count; i++)
            {
                var button = _buttonLevels[i];
                var x = bodyStartX + xOffsets[i % 4];
                var y = bodyStartY + dy * i;
                button.Location = new Point(x, y);
            }
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
