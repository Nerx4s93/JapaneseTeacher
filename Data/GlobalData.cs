using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JapaneseTeacher.Data
{
    internal class GlobalData
    {
        private List<Module> _modules;
        
        public void LoadData()
        {
            if (!Directory.Exists("Modules"))
            {
                Directory.CreateDirectory("Modules");
            }

            #region Загрузка модулей

            _modules = new List<Module>();

            var files = Directory.GetFiles("Modules");
            foreach (string file in files)
            {
                var themeName = Path.GetFileNameWithoutExtension(file);
                var module = Module.LoadFromFile(themeName);

                _modules.Add(module);
            }

            #endregion
        }

        public void SaveData()
        {
            foreach (var module in _modules)
            {
                module.SaveToFile();
            }
        }

        public Module GetModuleByName(string name)
        {
            return _modules.First(t => t.Name == name);
        }
    }
}
