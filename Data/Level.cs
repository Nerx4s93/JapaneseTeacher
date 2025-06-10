using System.Collections.Generic;

namespace JapaneseTeacher.Data
{
    internal class Level
    {
        public string LevelId;
        public string Description;
        public List<LevelType> LevelTypes;
        public int CompletedSublevels;
        public int TotalSublevels;
    }
}
