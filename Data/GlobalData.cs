using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace JapaneseTeacher.Data
{
    internal class GlobalData
    {
        private readonly string[] _themes = new string[1] { "Hiragana" };

        private List<VocabularySet> vocabularySets;
        
        public void LoadData()
        {
            if (!Directory.Exists("Themes"))
            {
                Directory.CreateDirectory("Themes");
            }

            #region Загрузка тем

            vocabularySets = new List<VocabularySet>();

            foreach (string theme in _themes)
            {
                var hiragana = new VocabularySet(theme);
                hiragana.LoadData();
                vocabularySets.Add(hiragana);
            }

            #endregion
        }
    }
}
