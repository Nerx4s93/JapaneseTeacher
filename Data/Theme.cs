using System.Drawing;
using System.IO;

using Newtonsoft.Json;

using JapaneseTeacher.Properties;
using System.Collections.Generic;

namespace JapaneseTeacher.Data
{
    internal class Theme
    {
        public string Name;
        public string Description;
        public Color Color;

        private List<Level> Levels;
        private VocabularySet VocabularySet;

        public Theme(string name)
        {
            Name = name;
        }

        public static Theme LoadFromFile(string name)
        {
            var path = Path.Combine("Themes", $"{name}.json");

            if (!File.Exists(path))
            {
                if (Resources.ResourceManager.GetObject(name) is string defaultJson)
                {
                    File.WriteAllText(path, defaultJson);
                    return JsonConvert.DeserializeObject<Theme>(defaultJson);
                }
                throw new FileNotFoundException($"Файл {path} не найден");
            }

            var stringJson = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Theme>(stringJson);
        }

        public void SaveToFile()
        {
            var path = Path.Combine("Themes", $"{Name}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
