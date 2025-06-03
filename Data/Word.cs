namespace JapaneseTeacher.Data
{
    internal class Word
    {
        /// <summary>
        /// Слово на русском
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Перевод на японском
        /// </summary>
        public string Translation { get; set; }

        /// <summary>
        /// Количество встречь
        /// </summary>
        public int Encounters { get; set; }

        /// <summary>
        /// Ошибок
        /// </summary>
        public int Mistakes { get; set; }

        /// <summary>
        /// Количество правильных ответов подрят для исправления ошибки
        /// </summary>
        public int StreakForMistakes { get; set; }

        /// <summary>
        /// Количество правильных ответов подрят
        /// </summary>
        public int Streak { get; set; }


        /// <summary>
        /// Количество раз, когда слово не попадалось
        /// </summary>
        public int TimesNotSeen { get; set; }

        /// <summary>
        /// Уровень, к которому отностится слово
        /// </summary>
        public string Level;
    }
}