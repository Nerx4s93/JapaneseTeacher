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
        private const int ScrollStep = 65;
        private const int HeaderHeight = 150;
        private const int ButtonSpacing = 110;

        private Control _mainControl;
        private Module _module;

        private ThemeHeader _moduleHeader = new ThemeHeader();
        private List<Control> _buttonLevels = new List<Control>();

        private int _scrollPosition = 0;
        private int _maxScrollPosition = 0;

        public override void Start(object[] args)
        {
            _mainControl = args[0] as Control;
            _module = args[1] as Module;
            AdjustControls();
            _mainControl.Resize += Form_Resize;
            _mainControl.MouseWheel += Form_MouseWheel;
        }

        public override void Stop()
        {
            _moduleHeader.Dispose();
            foreach (var buttonLevel in _buttonLevels)
            {
                buttonLevel.Dispose();
            }
            _mainControl.Resize -= Form_Resize;
            _mainControl.MouseWheel -= Form_MouseWheel;
        }

        private void Form_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_buttonLevels.Count == 0)
            {
                return;
            }

            var delta = e.Delta > 0 ? ScrollStep : -ScrollStep;
            var newPosition = _scrollPosition + delta;

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
            /*var button = sender as ButtonLevel;

            if (button.Active)
            {
                var level = button.Level;
                SceneManager.LoadScene(new LevelScene(), new object[4] { _mainControl, _module, button.Tag, level });
            }*/
        }

        #region Настройка элементов управления

        private void AdjustControls()
        {
            _moduleHeader = new ThemeHeader
            {
                Size = new Size(700, 110)
            };
            _mainControl.Controls.Add(_moduleHeader);

            for (var i = 0; i < _module.Themes.Count; i++)
            {
                var theme = _module.Themes[i];

                var levels = theme.GetLevels();
                bool active = true;
                foreach (var level in levels)
                {
                    var button = new ButtonLevel
                    {
                        ComplitePercent = (float)level.CompletedSublevels / (float)level.TotalSublevels * 100f,
                        Tag = theme,
                        Active = active,
                        Level = level.LevelId,
                        DescriptionActiveBackgroundColor = theme.Color,
                        CompliteSublevels = level.CompletedSublevels,
                        TotalSublevels = level.TotalSublevels,
                        Description = level.Description
                    };
                    button.Click += Button_Click;

                    if (level.CompletedSublevels < level.TotalSublevels)
                    {
                        active = false;
                    }

                    _buttonLevels.Add(button);
                    _mainControl.Controls.Add(button);
                }

                if (i < _module.Themes.Count - 1)
                {
                    var width = _mainControl.ClientSize.Width * 4 / 5;
                    var contentDivider = new ContentDivider
                    {
                        Text = _module.Themes[i + 1].Description,
                        Size = new Size(width, 25),
                        Tag = theme
                    };

                    _buttonLevels.Add(contentDivider);
                    _mainControl.Controls.Add(contentDivider);
                }
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

            var totalHeight = HeaderHeight + _buttonLevels.Count * ButtonSpacing;
            _maxScrollPosition = Math.Min(0, _mainControl.ClientSize.Height - totalHeight);
        }

        private void MoveControls()
        {
            var startX = (_mainControl.Size.Width - _moduleHeader.Size.Width) / 2;
            _moduleHeader.Location = new Point(startX, 10);

            var bodyStartX = startX + _moduleHeader.Size.Width / 2 - 200;
            var bodyStartY = HeaderHeight;
            var dx = 50;

            var xOffsets = new int[4]{ 0, dx, 2 * dx, dx };
            var findTheme = true;
            for (int i = 0; i < _buttonLevels.Count; i++)
            {
                var control = _buttonLevels[i];

                if (_buttonLevels[i] is ButtonLevel)
                {
                    var x = bodyStartX + xOffsets[i % 4];
                    var y = bodyStartY + ButtonSpacing * i + _scrollPosition;
                    control.Location = new Point(x, y);
                }
                else if (_buttonLevels[i] is ContentDivider)
                {
                    var x = (_mainControl.Size.Width - control.Width) / 2;
                    var y = bodyStartY + ButtonSpacing * i + _scrollPosition + 40;
                    control.Location = new Point(x, y);
                }

                if (findTheme && control.Location.Y > 0)
                {
                    var theme = control.Tag as Theme;
                    _moduleHeader.Theme = theme.Name;
                    _moduleHeader.Description = theme.Description;
                    _moduleHeader.BackgroundColor = theme.Color;
                    _moduleHeader.Invalidate();
                    findTheme = !findTheme;
                }
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            CalculateMaxScrollPosition();
            _scrollPosition = Math.Max(_maxScrollPosition, _scrollPosition);
            _scrollPosition = Math.Min(0, _scrollPosition);
            MoveControls();
        }

        #endregion
    }
}
