using System.Collections.Generic;
using System.IO;

namespace JapaneseTeacher.Data
{
    internal class GlobalData
    {
        private List<VocabularySet> vocabularySets;
        
        public void LoadData()
        {
            if (!Directory.Exists("Themes"))
            {
                Directory.CreateDirectory("Themes");
            }

            #region Загрузка тем

            vocabularySets = new List<VocabularySet>();

            var files = Directory.GetFiles("Themes");
            foreach (string file in files)
            {
                var theme = Path.GetFileNameWithoutExtension(file);

                var hiragana = new VocabularySet(theme);
                hiragana.LoadData();

                vocabularySets.Add(hiragana);
            }

            #endregion
        }
    }
}
