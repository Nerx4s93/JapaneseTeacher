using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using JapaneseTeacher.Properties;

namespace JapaneseTeacher.СourseData
{
    internal class Module
    {
        public string Name { get; set; }
        public List<Theme> Themes { get; set; }

        public static Module LoadFromFile(string name)
        {
            var path = Path.Combine("Modules", $"{name}.json");

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
            var path = Path.Combine("Modules", $"{Name}.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}
