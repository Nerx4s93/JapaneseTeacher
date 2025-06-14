using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.Scenes.Content.Levels;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content
{
    internal class LevelScene : Scene
    {
        private readonly Random _random = new Random();
        private readonly SceneManager _sceneManager = new SceneManager();

        private Control _mainControl;

        private Module _module;
        private Theme _theme;
        private string _levelId;
        private Word _currentWord;
        private string _task;
        private string _answer;

        private FlatProgressBar _flatProgressBar;
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
            _sceneManager.OnGetMessage += SceneManager_OnGetMessage;
        }

        public override void Stop()
        {
            _flatProgressBar.Dispose();
            _answerResultPanel.Dispose();
            _mainControl.Resize -= Form_Resize;
            _sceneManager.StopScene();
        }

        #region Проверка ответа

        private void SceneManager_OnGetMessage(object sendler, object[] args)
        {
            if (!_answerResultPanel.Visible)
            {
                var userAnswer = args[0] as string;

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
            else
            {
                _answerResultPanel.Visible = false;

                if (_flatProgressBar.Value == _flatProgressBar.MaxValue)
                {
                    _theme.CompliteLevel(_levelId);
                    SceneManager.LoadScene(new LevelResultScene(), new object[4] { _mainControl, _module, _totalAnswers, _wrongAnswers });
                }
                else
                {
                    LoadNewWord();
                }
            }
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
                case LevelType.JapaneseToTranslate:
                    {
                        _task = $"Напишите первод: {_currentWord.Text}";
                        _answer = _currentWord.Translation;
                        break;
                    }
            }

            _sceneManager.LoadScene(new TextBoxTask(), new object[2] { _mainControl, _task });
        }

        #endregion
        #region Настройка элементов управления

        private void AdjustControls()
        {
            _flatProgressBar = new FlatProgressBar
            {
                MaxValue = 3
            };

            _answerResultPanel = new AnswerResultPanel
            {
                Visible = false
            };

            _mainControl.Controls.Add(_flatProgressBar);
            _mainControl.Controls.Add(_answerResultPanel);

            Form_Resize(null, null);
        }

        private void MoveControls()
        {
            _flatProgressBar.Size = new Size(_mainControl.Width * 8 / 10, 20);
            _flatProgressBar.Location = new Point((_mainControl.Size.Width - _flatProgressBar.Width) / 2, 20);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            MoveControls();
        }

        #endregion
    }
}
