using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JapaneseTeacher.Data
{
    internal class GlobalData
    {
        private List<Theme> themes;
        
        public void LoadData()
        {
            if (!Directory.Exists("Themes"))
            {
                Directory.CreateDirectory("Themes");
            }

            #region Загрузка тем

            themes = new List<Theme>();

            var files = Directory.GetFiles("Themes");
            foreach (string file in files)
            {
                var themeName = Path.GetFileNameWithoutExtension(file);
                var theme = Theme.LoadFromFile(themeName);

                themes.Add(theme);
            }

            #endregion
        }

        public Theme GetThemeByName(string name)
        {
            return themes.First(t => t.Name == name);
        }
    }
}
