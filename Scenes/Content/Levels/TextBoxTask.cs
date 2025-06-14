using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content.Levels
{
    internal class TextBoxTask : Scene
    {
        private Control _mainControl;
        private string _task;

        private Label _labelTask;
        private TextBox _textBoxAnswer;
        private AnimatedPressButton _checkButton;

        public override void Start(object[] args)
        {
            _mainControl = args[0] as Control;
            _task = args[1] as string;
            AdjustControls();
            _mainControl.Resize += Form_Resize;

            _textBoxAnswer.Focus();
        }

        public override void Stop()
        {
            _labelTask.Dispose();
            _textBoxAnswer.Dispose();
            _checkButton.Dispose();
            _mainControl.Resize -= Form_Resize;
        }

        private void CheckButton_Click(object sender, EventArgs e)
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
            SendMessage(new object[1] { _textBoxAnswer.Text });
        }

        #region Настройка элементов управления

        private void AdjustControls()
        {
            _labelTask = new Label
            {
                Font = new Font("Microsoft Sans Serif", 28f),
                Text = _task,
                AutoSize = true,
            };

            _textBoxAnswer = new TextBox
            {
                Font = new Font("Microsoft Sans Serif", 24f),
                AutoSize = true,
                Width = 400
            };
            _textBoxAnswer.KeyDown += TextBoxAnswer_KeyDown;

            _checkButton = new AnimatedPressButton
            {
                Text = "Проверить"
            };
            _checkButton.Click += CheckButton_Click;

            _mainControl.Controls.Add(_labelTask);
            _mainControl.Controls.Add(_textBoxAnswer);
            _mainControl.Controls.Add(_checkButton);

            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            using (var graphics = _mainControl.CreateGraphics())
            {
                var labelTextSize = graphics.MeasureString(_labelTask.Text, _labelTask.Font);
                var labelX = (_mainControl.Width - (int)labelTextSize.Width) / 2;
                var labelY = (_mainControl.Height / 2) - 100;
                _labelTask.Location = new Point(labelX, labelY);

                var textBoxX = (_mainControl.Width - _textBoxAnswer.Width) / 2;
                var textBoxY = labelY + (int)labelTextSize.Height + 20;
                _textBoxAnswer.Location = new Point(textBoxX, textBoxY);
            }

            var clientSize = _mainControl.ClientSize;
            var buttonMargin = 20;
            _checkButton.Location = new Point(
                clientSize.Width - _checkButton.Width - buttonMargin,
                clientSize.Height - _checkButton.Height - buttonMargin
            );
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
