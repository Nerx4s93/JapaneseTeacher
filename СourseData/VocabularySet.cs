﻿using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;

namespace JapaneseTeacher.СourseData;

internal class VocabularySet
{
    private readonly Random _random = new();

    [JsonProperty("Words")] private List<Word> _words;

    public Word GetNextWord()
    {
        return GetNextWord(null);
    }

    public Word GetNextWord(string level)
    {
        if (_words == null || !_words.Any()) throw new InvalidOperationException("В словоре отсутствуют слова");

        #region Фильтрация слов

        var filteredWords = string.IsNullOrEmpty(level)
            ? _words
            : _words.Where(w => w.Level == level).ToList();

        if (!filteredWords.Any()) throw new InvalidOperationException("Нет слов с указанным уровнем");

        #endregion

        // Обновляем счетчики времени, когда слова не показывались
        foreach (var word in filteredWords) word.TimesNotSeen++;

        var totalEncounters = filteredWords.Sum(w => w.Encounters);
        var wordWeights = new Dictionary<Word, double>();
        var totalWeight = 0d;

        // Предварительно вычисляем веса для всех слов
        foreach (var word in filteredWords)
        {
            var weight = GetWeight(word, totalEncounters);
            wordWeights[word] = weight;
            totalWeight += weight;
        }

        // Нормализуем веса и создаем взвешенный список
        var weightedList = new List<(Word Word, double CumulativeWeight)>();
        var cumulative = 0d;
        foreach (var kvp in wordWeights)
        {
            cumulative += kvp.Value / totalWeight;
            weightedList.Add((kvp.Key, cumulative));
        }

        // Выбираем случайное слово с учетом весов
        var randomValue = _random.NextDouble();
        foreach (var item in weightedList.Where(item => randomValue <= item.CumulativeWeight))
        {
            item.Word.TimesNotSeen = 0;
            return item.Word;
        }

        return filteredWords.Last();
    }

    public double GetWeight(Word word, int totalEncounters)
    {
        if (totalEncounters == 0) return 1.0;

        // Базовый вес - обратно пропорционален количеству показов
        var baseWeight = 1.0 / (1.0 + word.Encounters / (double)totalEncounters * _words.Count);
        // Фактор ошибок - увеличивает вес для слов с ошибками
        var mistakesFactor = 1.0 + Math.Pow(word.Mistakes + 1, 1.5);
        // Фактор серии - уменьшает вес для слов с длинной серией правильных ответов
        var streakFactor = Math.Max(0.2, 2.0 / (1.0 + Math.Exp(0.5 * word.Streak)));
        // Фактор времени не показа - увеличивает вес для давно не показываемых слов
        var notSeenFactor = Math.Min(10.0, 1.0 + Math.Log(1 + word.TimesNotSeen * 0.2));

        // Комбинированный вес
        var weight = baseWeight * mistakesFactor * streakFactor * notSeenFactor;

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
    }

    public List<Word> GetAllWords()
    {
        return _words.ToList();
    }
}