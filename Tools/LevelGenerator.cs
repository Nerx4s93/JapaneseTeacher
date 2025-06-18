using System;
using System.Collections.Generic;

using JapaneseTeacher.Data;
using JapaneseTeacher.Data.Сourse;
using JapaneseTeacher.Scenes.Content.Levels;

namespace JapaneseTeacher.Tools;

internal class LevelGenerator
{
    private readonly Theme _theme;
    private readonly string _levelId;
    private readonly int _countOfTasks;

    private readonly Queue<LevelTask> _levelTasks;

    public LevelGenerator(Theme theme, string levelId, int countOfTasks)
    {
        _theme = theme;
        _levelId = levelId;
        _countOfTasks = countOfTasks;
        _levelTasks = GenerateLevel();
    }

    public string GetTask()
    {
        return _levelTasks.Peek().Task;
    }

    public string GetAnswer()
    {
        return _levelTasks.Peek().Answer;
    }

    public bool Check(string userAnswer)
    {
        var levelTask = _levelTasks.Dequeue();

        if (userAnswer.Equals(levelTask.Answer, StringComparison.OrdinalIgnoreCase))
        {
            _theme.UpdateWordStats(levelTask.Word, true);
            return true;
        }
        else
        {
            _theme.UpdateWordStats(levelTask.Word, false);
            _levelTasks.Enqueue(levelTask);
            return false;
        }
    }

    private Queue<LevelTask> GenerateLevel()
    {
        var levelTasks = new Queue<LevelTask>();

        for (var i = 0; i < _countOfTasks; i++)
        {
            var levelTask = GenerateLevelTask();
            levelTasks.Enqueue(levelTask);
        }

        return levelTasks;
    }

    private LevelTask GenerateLevelTask()
    {
        var levelType = _theme.GetLevelType(_levelId);
        var word = _theme.GetNextWord(_levelId);

        var levelTask = new LevelTask
        {
            Word = word
        };
        switch (levelType)
        {
            case LevelType.JapaneseToReading:
            {
                levelTask.Task = $"Напишите на ромадзи: {word.Text}";
                levelTask.Answer = word.Reading;
                levelTask.Scene = new TextBoxTask();
                break;
            }
            case LevelType.TranslationToReading:
            {
                levelTask.Task = $"Напишите на ромадзи: {word.Translation}";
                levelTask.Answer = word.Reading;
                levelTask.Scene = new TextBoxTask();
                break;
            }
            case LevelType.ReadingToTranslate:
            {
                levelTask.Task = $"Напишите первод: {word.Reading}";
                levelTask.Answer = word.Translation;
                levelTask.Scene = new TextBoxTask();
                break;
            }
            case LevelType.JapaneseToTranslate:
            {
                levelTask.Task = $"Напишите первод: {word.Text}";
                levelTask.Answer = word.Translation;
                levelTask.Scene = new TextBoxTask();
                break;
            }
        }

        return levelTask;
    }
}