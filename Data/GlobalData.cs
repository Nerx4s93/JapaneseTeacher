using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JapaneseTeacher.Data
{
    internal class GlobalData
    {
        private List<Module> themes;
        
        public void LoadData()
        {
            if (!Directory.Exists("Themes"))
            {
                Directory.CreateDirectory("Themes");
            }

            #region Загрузка тем

            themes = new List<Module>();

            var files = Directory.GetFiles("Themes");
            foreach (string file in files)
            {
                var themeName = Path.GetFileNameWithoutExtension(file);
                var theme = Module.LoadFromFile(themeName);

                themes.Add(theme);
            }

            #endregion
        }

        public Module GetThemeByName(string name)
        {
            return themes.First(t => t.Name == name);
        }
    }
}
