using System;
using System.Drawing;
using System.Windows.Forms;

using JapaneseTeacher.Data.Сourse;
using JapaneseTeacher.Tools;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Scenes.Content;

internal class LevelScene : Scene
{
    // Переменные загрузки сцены
    //
    private Control _mainControl;
    private Module _module;
    private Theme _theme;
    private string _levelId;

    // Инструменты для создания уровня
    //
    private LevelGenerator _levelGenerator;
    private SceneManager _sceneManager;

    // Счётчик ответов
    //
    private int _totalAnswers;
    private int _wrongAnswers;

    // Элементы управления на сцене
    //
    private FlatProgressBar _flatProgressBar;
    private AnswerResultPanel _answerResultPanel;

    public override void Start(object[] args)
    {
        _mainControl = args[0] as Control;
        _module = args[1] as Module;
        _theme = args[2] as Theme;
        _levelId = args[3] as string;

        _levelGenerator = new LevelGenerator(_theme, _levelId, 30);
        _sceneManager = new SceneManager();

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

    private void SceneManager_OnGetMessage(object sender, object[] args)
    {
        if (!_answerResultPanel.Visible)
        {
            var userAnswer = args[0] as string;

            var result = _levelGenerator.Check(userAnswer);
            if (result.correct)
            {
                _answerResultPanel.WasCorrect = true;
            }
            else
            {
                _wrongAnswers += 1;
                _answerResultPanel.WasCorrect = false;
                _answerResultPanel.Text = $"Неверно. Правильный ответ: {result.answer}";
            }

            _flatProgressBar.Value = _levelGenerator.TotalTasks - _levelGenerator.RemainingTasks;
            _flatProgressBar.MaxValue = _levelGenerator.TotalTasks;
            _totalAnswers += 1;
            _answerResultPanel.Visible = true;
        }
        else
        {
            _answerResultPanel.Visible = false;

            if (_levelGenerator.CompliteLevel)
            {
                _theme.CompliteLevel(_levelId);
                SceneManager.LoadScene(new LevelResultScene(), [_mainControl, _module, _totalAnswers, _wrongAnswers]);
            }
            else
            {
                LoadNewWord();
            }
        }
    }

    private void LoadNewWord()
    {
        var scene = _levelGenerator.CurrentScene;
        var task = _levelGenerator.CurrentTask;
        _sceneManager.LoadScene(scene, [_mainControl, task]);
    }

    #endregion
    #region Настройка элементов управления

    private void AdjustControls()
    {
        _flatProgressBar = new FlatProgressBar
        {
            MaxValue = _levelGenerator.TotalTasks
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