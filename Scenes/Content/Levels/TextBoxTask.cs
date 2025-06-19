using System;
using System.Drawing;
using System.Windows.Forms;

namespace JapaneseTeacher.Scenes.Content.Levels
{
    internal class TextBoxTask : SceneLevelContent
    {
        private Control _mainControl;
        private string _task;

        private Label _labelTask;
        private TextBox _textBoxAnswer;

        public override void Start(object[] args)
        {
            _mainControl = args[0] as Control;
            _task = args[1] as string;

            AdjustControls();
            _textBoxAnswer.Focus();
        }

        public override void Stop()
        {
            _labelTask.Dispose();
            _textBoxAnswer.Dispose();

            _mainControl.Resize -= MainControl_Resize;
        }

        private void TextBoxAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CheckAnswer();
                e.SuppressKeyPress = true;
            }
        }

        internal override void CheckAnswer()
        {
            SendMessage([ _textBoxAnswer.Text ]);
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

            _mainControl.Controls.Add(_labelTask);
            _mainControl.Controls.Add(_textBoxAnswer);

            _mainControl.Resize += MainControl_Resize;
            MoveControls();
        }

        private void MoveControls()
        {
            var clientSize = _mainControl.ClientSize;
            using var graphics = _mainControl.CreateGraphics();

            var labelTextSize = graphics.MeasureString(_labelTask.Text, _labelTask.Font);
            var labelX = (clientSize.Width - (int)labelTextSize.Width) / 2;
            var labelY = (clientSize.Height / 2) - 100;
            _labelTask.Location = new Point(labelX, labelY);

            var textBoxX = (clientSize.Width - _textBoxAnswer.Width) / 2;
            var textBoxY = labelY + (int)labelTextSize.Height + 20;
            _textBoxAnswer.Location = new Point(textBoxX, textBoxY);
        }

        private void MainControl_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
