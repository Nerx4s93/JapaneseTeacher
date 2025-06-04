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
        private const int ScrollStep = 30;
        private const int HeaderHeight = 150;
        private const int ButtonSpacing = 110;

        private readonly Form _form;

        private ModuleHeader _moduleHeader = new ModuleHeader();
        private List<ButtonLevel> _buttonLevels = new List<ButtonLevel>();

        private bool _update;
        private int _scrollPosition = 0;
        private int _maxScrollPosition = 0;

        public ThemeBuilder(Form form)
        {
            _form = form;
            _form.Resize += Form_Resize;
            _form.MouseWheel += Form_MouseWheel;
        }

        public void Build(Theme theme)
        {
            _update = true;
            AdjustControls(theme);
        }

        public void StopHandle()
        {
            _update = false;
            _moduleHeader.Dispose();
            foreach (var buttonLevel in _buttonLevels)
            {
                buttonLevel.Dispose();
            }
            _form.MouseWheel -= Form_MouseWheel;
        }

        private void AdjustControls(Theme theme)
        {
            _moduleHeader.Dispose();
            foreach (var buttonLevel in _buttonLevels)
            {
                buttonLevel.Dispose();
            }
            _buttonLevels.Clear();

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
            _scrollPosition = 0;
            CalculateMaxScrollPosition();
            Form_Resize(null, null);
        }

        private void CalculateMaxScrollPosition()
        {
            if (_buttonLevels.Count == 0)
            {
                return;
            }

            int totalHeight = HeaderHeight + (_buttonLevels.Count * ButtonSpacing);
            _maxScrollPosition = Math.Min(0, _form.ClientSize.Height - totalHeight);
        }

        private void MoveControls()
        {
            var startX = (_form.Size.Width - _moduleHeader.Size.Width) / 2;
            _moduleHeader.Location = new Point(startX, 10);

            var bodyStartX = startX + _moduleHeader.Size.Width / 2 - 200;
            var bodyStartY = HeaderHeight;
            var dx = 50;

            int[] xOffsets = { 0, dx, 2 * dx, dx };

            for (int i = 0; i < _buttonLevels.Count; i++)
            {
                var button = _buttonLevels[i];
                var x = bodyStartX + xOffsets[i % 4];
                var y = bodyStartY + ButtonSpacing * i + _scrollPosition;
                button.Location = new Point(x, y);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (_update)
            {
                CalculateMaxScrollPosition();
                _scrollPosition = Math.Max(_maxScrollPosition, _scrollPosition);
                _scrollPosition = Math.Min(0, _scrollPosition);
                MoveControls();
            }
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!_update || _buttonLevels.Count == 0)
            {
                return;
            }

            int delta = e.Delta > 0 ? ScrollStep : -ScrollStep;
            int newPosition = _scrollPosition + delta;

            newPosition = Math.Max(_maxScrollPosition, newPosition);
            newPosition = Math.Min(0, newPosition);

            if (newPosition != _scrollPosition)
            {
                _scrollPosition = newPosition;
                MoveControls();
            }
        }
    }
}