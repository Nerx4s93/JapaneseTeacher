using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content
{
    internal class LevelScene : Scene
    {
        private readonly Random _random = new Random();

        private Control _mainControl;

        private Theme _theme;
        private string _levelId;
        private Word _currentWord;

        private FlatProgressBar _flatProgressBar;
        private Label _labelTask;
        private TextBox _textBoxAnswer;
        private RoundedButton _roundedButton;
        private AnswerResultPanel _answerResultPanel;

        private int _totalAnswers;
        private int _wrongAnswers;

        public override void Start(object[] args)
        {
            _mainControl = args[0] as Control;
            _theme = args[1] as Theme;
            _levelId = args[2] as string;
            _currentWord = _theme.GetNextWord(_levelId);
            AdjustControls();
            _mainControl.Resize += Form_Resize;
        }

        public override void Stop()
        {
            _flatProgressBar.Dispose();
            _labelTask.Dispose();
            _textBoxAnswer.Dispose();
            _roundedButton.Dispose();
            _answerResultPanel.Dispose();
            _mainControl.Resize -= Form_Resize;
        }

        #region Проверка ответа

        private void RoundedButton_Click(object sender, EventArgs e)
        {
            CheckAnswer();
        }

        private void TextBoxAnswer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _answerResultPanel.Visible == false)
            {
                CheckAnswer();
                e.SuppressKeyPress = true;
            }
            else if (_answerResultPanel.Visible == true)
            {
                if (_flatProgressBar.Value == _flatProgressBar.MaxValue)
                {
                    _theme.CompliteLevel(_levelId);
                    SceneManager.LoadScene(new LevelResultScene(), new object[4] { _mainControl, _theme, _totalAnswers, _wrongAnswers });
                }
                else
                {
                    _answerResultPanel.Visible = false;
                    _textBoxAnswer.Clear();
                    LoadNewWord();
                }
            }
        }

        private void CheckAnswer()
        {
            string userAnswer = _textBoxAnswer.Text.Trim();

            if (userAnswer.Equals(_currentWord.Translation, StringComparison.OrdinalIgnoreCase))
            {
                _flatProgressBar.Value += 1;
                _answerResultPanel.WasCorrect = true;
                _theme.UpdateWordStats(_currentWord, true);
            }
            else
            {
                _wrongAnswers += 1;
                _flatProgressBar.MaxValue += _random.Next(2);
                _answerResultPanel.WasCorrect = false;
                _answerResultPanel.Text = $"Неверно. Правильный ответ: {_currentWord.Translation}";
                _theme.UpdateWordStats(_currentWord, false);
            }
            _totalAnswers += 1;
            _answerResultPanel.Visible = true;
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
            _flatProgressBar.MaxValue = 30;

            _labelTask = new Label();
            _labelTask.Font = new Font("Microsoft Sans Serif", 28f);
            _labelTask.AutoSize = true;
            _labelTask.Text = $"Напишите перевод: {_currentWord.Text}";

            _textBoxAnswer = new TextBox();
            _textBoxAnswer.Font = new Font("Microsoft Sans Serif", 24f);
            _textBoxAnswer.AutoSize = true;
            _textBoxAnswer.Width = 400;
            _textBoxAnswer.KeyDown += TextBoxAnswer_KeyDown;

            _roundedButton = new RoundedButton();
            _roundedButton.Font = new Font("Microsoft Sans Serif", 18f);
            _roundedButton.Text = "Проверить";
            _roundedButton.Click += RoundedButton_Click;

            _answerResultPanel = new AnswerResultPanel();
            _answerResultPanel.Visible = false;

            _mainControl.Controls.Add(_flatProgressBar);
            _mainControl.Controls.Add(_labelTask);
            _mainControl.Controls.Add(_textBoxAnswer);
            _mainControl.Controls.Add(_roundedButton);
            _mainControl.Controls.Add(_answerResultPanel);
            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            _flatProgressBar.Size = new Size(_mainControl.Width * 8 / 10, 20);
            _flatProgressBar.Location = new Point((_mainControl.Size.Width - _flatProgressBar.Width) / 2, 20);

            using (Graphics graphics = _mainControl.CreateGraphics())
            {
                var labelTextSize = graphics.MeasureString(_labelTask.Text, _labelTask.Font);
                int labelX = (_mainControl.Width - (int)labelTextSize.Width) / 2;
                int labelY = (_mainControl.Height / 2) - 100;
                _labelTask.Location = new Point(labelX, labelY);

                int textBoxX = (_mainControl.Width - _textBoxAnswer.Width) / 2;
                int textBoxY = labelY + (int)labelTextSize.Height + 20;
                _textBoxAnswer.Location = new Point(textBoxX, textBoxY);

                int buttonX = _textBoxAnswer.Right - _roundedButton.Width;
                int buttonY = _textBoxAnswer.Bottom + 20;
                _roundedButton.Location = new Point(buttonX, buttonY);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
