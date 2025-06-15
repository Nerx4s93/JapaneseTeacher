using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using Newtonsoft.Json;

namespace JapaneseTeacher.СourseData;

internal class Theme
{
    public string Name;
    public string Description;
    public Color Color;

    [JsonProperty("Levels")] private List<Level> _levels;
    [JsonProperty("VocabularySet")] private VocabularySet _vocabularySet;

    public List<Level> GetLevels()
    {
        return _levels.ToList();
    }

    public void CompliteLevel(string levelId)
    {
        var level = _levels.First(x => x.LevelId == levelId);
        level.CompletedSublevels += 1;
    }

    public LevelType GetLevelType(string levelId)
    {
        var level = _levels.First(x => x.LevelId == levelId);
        var random = new Random();
        return level.LevelTypes[random.Next(level.LevelTypes.Count)];
    }

    public Word GetNextWord() => _vocabularySet.GetNextWord();
    public Word GetNextWord(string level) => _vocabularySet.GetNextWord(level);
    public void UpdateWordStats(Word word, bool wasCorrect) => _vocabularySet.UpdateWordStats(word, wasCorrect);
}