using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content
{
    internal class ModuleScene : Scene
    {
        private const int ScrollStep = 30;
        private const int HeaderHeight = 150;
        private const int ButtonSpacing = 110;

        private Form _form;
        private Theme _theme;

        private ModuleHeader _moduleHeader = new ModuleHeader();
        private List<ButtonLevel> _buttonLevels = new List<ButtonLevel>();

        private int _scrollPosition = 0;
        private int _maxScrollPosition = 0;

        public override void Start(object[] args)
        {
            _form = args[0] as Form;
            _theme = args[1] as Theme;

            AdjustControls(_theme);
            _form.Resize += Form_Resize;
            _form.MouseWheel += Form_MouseWheel;
        }

        public override void Stop()
        {
            _moduleHeader.Dispose();
            foreach (var buttonLevel in _buttonLevels)
            {
                buttonLevel.Dispose();
            }
            _form.Resize -= Form_Resize;
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
            _form.Controls.Add(_moduleHeader);

            var levels = theme.GetLevels();
            bool active = true;
            foreach (var level in levels)
            {
                var button = new ButtonLevel();
                button.ComplitePercent = (float)level.CompletedSublevels / (float)level.TotalSublevels * 100f;
                button.Tag = 1;
                button.Active = active;
                button.Level = level.LevelId;
                button.Click += Button_Click;

                if (level.CompletedSublevels != level.TotalSublevels)
                {
                    active = false;
                }

                _buttonLevels.Add(button);
                _form.Controls.Add(button);
            }

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
            CalculateMaxScrollPosition();
            _scrollPosition = Math.Max(_maxScrollPosition, _scrollPosition);
            _scrollPosition = Math.Min(0, _scrollPosition);
            MoveControls();
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_buttonLevels.Count == 0)
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


        private void Button_Click(object sender, EventArgs e)
        {
            var button = sender as ButtonLevel;
            var level = button.Level;
            SceneManager.LoadScene(new LevelScene(), new object[3] { _form, _theme, level });
        }
    }
}
