using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

using Newtonsoft.Json;

using JapaneseTeacher.Properties;
using System;

namespace JapaneseTeacher.Data
{
    internal class Module
    {
        public string Name;
        public string Description;
        public Color Color;

        [JsonProperty("Levels")]
        private List<Level> _levels;
        [JsonProperty("VocabularySet")]
        private VocabularySet _vocabularySet;

        public Module(string name)
        {
            Name = name;
        }

        public static Module LoadFromFile(string name)
        {
            var path = Path.Combine("Themes", $"{name}.json");

            if (!File.Exists(path))
            {
                if (Resources.ResourceManager.GetObject(name) is string defaultJson)
                {
                    File.WriteAllText(path, defaultJson);
                    return JsonConvert.DeserializeObject<Module>(defaultJson);
                }
                throw new FileNotFoundException($"Файл {path} не найден");
            }

            var stringJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Module>(stringJson);
        }

        public void SaveToFile()
        {
            var path = Path.Combine("Themes", $"{Name}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public List<Level> GetLevels()
        {
            return _levels.ToList();
        }

        public void CompliteLevel(string levelId)
        {
            var level = _levels.First(x => x.LevelId == levelId);
            level.CompletedSublevels += 1;
            SaveToFile();
        }

        public Word GetNextWord() => _vocabularySet.GetNextWord();
        public Word GetNextWord(string level) => _vocabularySet.GetNextWord(level);
        public void UpdateWordStats(Word word, bool wasCorrect) => _vocabularySet.UpdateWordStats(word, wasCorrect);
    }
}
