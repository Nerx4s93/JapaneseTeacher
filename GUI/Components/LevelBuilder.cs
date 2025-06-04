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
        private Word _currentWord;

        private FlatProgressBar _flatProgressBar;
        private Label _labelTask;
        private TextBox _textBoxAnswer;
        private RoundedButton _roundedButton;

        public void LoadLevel(Theme theme, string levelId)
        {
            _update = true;
            _theme = theme;
            _levelId = levelId;
            _currentWord = theme.GetNextWord(levelId);
            AdjustControls();
        }

        public void StopHandle()
        {
            _update = false;
            _flatProgressBar.Dispose();
            _labelTask.Dispose();
            _textBoxAnswer.Dispose();
            _roundedButton.Dispose();
        }

        #region Проверка ответа

        private void RoundedButton_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void TextBoxAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckAnswer();
                e.SuppressKeyPress = true;
            }
        }

        private void CheckAnswer()
        {
            string userAnswer = _textBoxAnswer.Text.Trim();

            if (userAnswer.Equals(_currentWord.Translation, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Правильно!", "Результат");
                _theme.UpdateWordStats(_currentWord, true);
                _textBoxAnswer.Clear();
                LoadNewWord();
            }
            else
            {
                MessageBox.Show($"Неверно. Правильный ответ: {_currentWord.Translation}", "Результат");
                _theme.UpdateWordStats(_currentWord, false);
            }
        }

        private void LoadNewWord()
        {
            _currentWord = _theme.GetNextWord(_levelId);
            _labelTask.Text = $"Напишите перевод: {_currentWord.Text}";
        }

        #endregion
        #region Настройка элементов управления

        private void AdjustControls()
        {
            _flatProgressBar = new FlatProgressBar();
            _flatProgressBar.Tag = 2;
            _flatProgressBar.MaxValue = 30;

            _labelTask = new Label();
            _labelTask.Tag = 2;
            _labelTask.Font = new Font("Microsoft Sans Serif", 28f);
            _labelTask.AutoSize = true;
            _labelTask.Text = $"Напишите перевод: {_currentWord.Text}";

            _textBoxAnswer = new TextBox();
            _textBoxAnswer.Tag = 2;
            _textBoxAnswer.Font = new Font("Microsoft Sans Serif", 24f);
            _textBoxAnswer.AutoSize = true;
            _textBoxAnswer.Width = 400;
            _textBoxAnswer.KeyDown += TextBoxAnswer_KeyDown;

            _roundedButton = new RoundedButton();
            _roundedButton.Tag = 2;
            _roundedButton.Font = new Font("Microsoft Sans Serif", 18f);
            _roundedButton.Text = "Проверить";
            _roundedButton.Click += RoundedButton_Click;

            _form.Controls.Add(_flatProgressBar);
            _form.Controls.Add(_labelTask);
            _form.Controls.Add(_textBoxAnswer);
            _form.Controls.Add(_roundedButton);
            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            _flatProgressBar.Size = new Size(_form.Width * 8 / 10, 20);
            _flatProgressBar.Location = new Point((_form.Size.Width - _flatProgressBar.Width) / 2, 20);

            using (Graphics graphics = _form.CreateGraphics())
            {
                var labelTextSize = graphics.MeasureString(_labelTask.Text, _labelTask.Font);
                int labelX = (_form.Width - (int)labelTextSize.Width) / 2;
                int labelY = (_form.Height / 2) - 100;
                _labelTask.Location = new Point(labelX, labelY);

                int textBoxX = (_form.Width - _textBoxAnswer.Width) / 2;
                int textBoxY = labelY + (int)labelTextSize.Height + 20;
                _textBoxAnswer.Location = new Point(textBoxX, textBoxY);

                int buttonX = _textBoxAnswer.Right - _roundedButton.Width;
                int buttonY = _textBoxAnswer.Bottom + 20;
                _roundedButton.Location = new Point(buttonX, buttonY);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            if (_update)
            {
                MoveControls();
            }
        }

        #endregion
    }
}
