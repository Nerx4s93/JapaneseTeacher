using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using JapaneseTeacher.Properties;

using Newtonsoft.Json;

namespace JapaneseTeacher.Data
{
    internal class VocabularySet
    {
        private readonly Random _random = new Random();
                
        public VocabularySet(string name)
        {
            Name = name;
            LoadData();
        }

        [JsonProperty("Name")]
        public readonly string Name;
        [JsonProperty("Words")]
        private List<Word> _words;

        public void LoadData()
        {
            var path = $"Themes\\{Name}.json";

            if (!File.Exists(path))
            {
                if (Resources.ResourceManager.GetObject(Name) is string defaultJson)
                {
                    File.WriteAllText(path, defaultJson);
                    _words = JsonConvert.DeserializeObject<List<Word>>(defaultJson);
                }
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    var stringJson = streamReader.ReadToEnd();
                    _words = JsonConvert.DeserializeObject<List<Word>>(stringJson);
                }
            }
        }

        public void SaveData()
        {
            var path = $"Theme\\{Name}.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(_words));
        }

        public bool AddWord(Word word)
        {
            if (_words.Any(w => w.Text == word.Text))
            {
                return false;
            }

            _words.Add(word);
            SaveData();
            return true;
        }

        public bool DeleteWord(Word word)
        {
            var wordToRemove = _words.FirstOrDefault(w => w.Text == word.Text);
            if (wordToRemove == null)
            {
                return false;
            }

            _words.Remove(wordToRemove);
            SaveData();
            return true;
        }

        public Word GetNextWord()
        {
            if (_words == null || !_words.Any())
            {
                return null;
            }

            // Обновляем счетчики времени, когда слова не показывались
            foreach (var word in _words)
            {
                word.TimesNotSeen++;
            }

            var totalEncounters = _words.Sum(w => w.Encounters);
            var wordWeights = new Dictionary<Word, double>();
            double totalWeight = 0;

            // Предварительно вычисляем веса для всех слов
            foreach (var word in _words)
            {
                var weight = GetWeight(word, totalEncounters);
                wordWeights[word] = weight;
                totalWeight += weight;
            }

            // Нормализуем веса и создаем взвешенный список
            var weightedList = new List<(Word Word, double CumulativeWeight)>();
            double cumulative = 0;
            foreach (var kvp in wordWeights)
            {
                cumulative += kvp.Value / totalWeight;
                weightedList.Add((kvp.Key, cumulative));
            }

            // Выбираем случайное слово с учетом весов
            double randomValue = _random.NextDouble();
            foreach (var item in weightedList)
            {
                if (randomValue <= item.CumulativeWeight)
                {
                    // Сбрасываем счетчик "не показывалось" для выбранного слова
                    item.Word.TimesNotSeen = 0;
                    return item.Word;
                }
            }

            return _words.Last();
        }

        public double GetWeight(Word word, int totalEncounters)
        {
            if (totalEncounters == 0)
            {
                return 1.0;
            }

            // Базовый вес - обратно пропорционален количеству показов
            double baseWeight = 1.0 / (1.0 + word.Encounters / (double)totalEncounters * _words.Count);
            // Фактор ошибок - увеличивает вес для слов с ошибками
            double mistakesFactor = 1.0 + Math.Pow(word.Mistakes + 1, 1.5);
            // Фактор серии - уменьшает вес для слов с длинной серией правильных ответов
            double streakFactor = Math.Max(0.2, 2.0 / (1.0 + Math.Exp(0.5 * word.Streak)));
            // Фактор времени не показа - увеличивает вес для давно не показываемых слов
            double notSeenFactor = Math.Min(10.0, 1.0 + Math.Log(1 + word.TimesNotSeen * 0.2));

            // Комбинированный вес
            double weight = baseWeight * mistakesFactor * streakFactor * notSeenFactor;

            // Гарантируем минимальный вес и возвращаем
            return Math.Max(0.1, weight);
        }

        public void UpdateWordStats(Word word, bool wasCorrect)
        {
            word.Encounters++;

            if (wasCorrect)
            {
                word.Streak++;
                word.StreakForMistakes++;
                if (word.StreakForMistakes == 3)
                {
                    word.StreakForMistakes = 0;
                    word.Mistakes = Math.Max(0, word.Mistakes - 1);
                }
            }
            else
            {
                word.Streak = 0;
                word.StreakForMistakes = 0;
                word.Mistakes++;
            }

            SaveData();
        }

        public List<Word> GetAllWords()
        {
            return _words.ToList();
        }
    }
}
