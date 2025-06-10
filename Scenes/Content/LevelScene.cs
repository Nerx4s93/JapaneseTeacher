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

        private Module _module;
        private Theme _theme;
        private string _levelId;
        private Word _currentWord;
        private string _task;   
        private string _answer;

        private FlatProgressBar _flatProgressBar;
        private Label _labelTask;
        private TextBox _textBoxAnswer;
        private AnimatedPressButton _animatedPressButton;
        private AnswerResultPanel _answerResultPanel;

        private int _totalAnswers;
        private int _wrongAnswers;

        public override void Start(object[] args)
        {
            _mainControl = args[0] as Control;
            _module = args[1] as Module;
            _theme = args[2] as Theme;
            _levelId = args[3] as string;
            LoadNewWord();
            AdjustControls();
            _mainControl.Resize += Form_Resize;
        }

        public override void Stop()
        {
            _flatProgressBar.Dispose();
            _labelTask.Dispose();
            _textBoxAnswer.Dispose();
            _animatedPressButton.Dispose();
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
                    SceneManager.LoadScene(new LevelResultScene(), new object[4] { _mainControl, _module, _totalAnswers, _wrongAnswers });
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
            var userAnswer = _textBoxAnswer.Text.Trim();

            if (userAnswer.Equals(_answer, StringComparison.OrdinalIgnoreCase))
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
                _answerResultPanel.Text = $"Неверно. Правильный ответ: {_answer}";
                _theme.UpdateWordStats(_currentWord, false);
            }
            _totalAnswers += 1;
            _answerResultPanel.Visible = true;
        }

        private void LoadNewWord()
        {
            var levelType = _theme.GetLevelType(_levelId);
            _currentWord = _theme.GetNextWord(_levelId);

            switch (levelType)
            {
                case LevelType.JapaneseToReading:
                    {
                        _task = $"Напишите на ромадзи: {_currentWord.Text}";
                        _answer = _currentWord.Reading;
                        break;
                    }
                case LevelType.TranslationToReading:
                    {
                        _task = $"Напишите на ромадзи: {_currentWord.Translation}";
                        _answer = _currentWord.Reading;
                        break;
                    }
                case LevelType.ReadingToTranslate:
                    {
                        _task = $"Напишите первод: {_currentWord.Reading}";
                        _answer = _currentWord.Translation;
                        break;
                    }
            }

            _labelTask?.Text = _task;
        }

        #endregion
        #region Настройка элементов управления

        private void AdjustControls()
        {
            _flatProgressBar = new FlatProgressBar
            {
                MaxValue = 30
            };

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

            _animatedPressButton = new AnimatedPressButton
            {
                Text = "Проверить"
            };
            _animatedPressButton.Click += RoundedButton_Click;

            _answerResultPanel = new AnswerResultPanel
            {
                Visible = false
            };

            _mainControl.Controls.Add(_flatProgressBar);
            _mainControl.Controls.Add(_labelTask);
            _mainControl.Controls.Add(_textBoxAnswer);
            _mainControl.Controls.Add(_animatedPressButton);
            _mainControl.Controls.Add(_answerResultPanel);

            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            _flatProgressBar.Size = new Size(_mainControl.Width * 8 / 10, 20);
            _flatProgressBar.Location = new Point((_mainControl.Size.Width - _flatProgressBar.Width) / 2, 20);

            using (var graphics = _mainControl.CreateGraphics())
            {
                var labelTextSize = graphics.MeasureString(_labelTask.Text, _labelTask.Font);
                var labelX = (_mainControl.Width - (int)labelTextSize.Width) / 2;
                var labelY = (_mainControl.Height / 2) - 100;
                _labelTask.Location = new Point(labelX, labelY);

                var textBoxX = (_mainControl.Width - _textBoxAnswer.Width) / 2;
                var textBoxY = labelY + (int)labelTextSize.Height + 20;
                _textBoxAnswer.Location = new Point(textBoxX, textBoxY);

                var buttonX = _textBoxAnswer.Right - _animatedPressButton.Width;
                var buttonY = _textBoxAnswer.Bottom + 20;
                _animatedPressButton.Location = new Point(buttonX, buttonY);
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
