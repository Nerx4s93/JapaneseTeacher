using System.Collections.Generic;

namespace JapaneseTeacher.СourseData;

internal class Level
{
    public string LevelId;
    public string Description;
    public List<LevelType> LevelTypes;
    public int CompletedSublevels;
    public int TotalSublevels;
}
