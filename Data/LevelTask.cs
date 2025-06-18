using JapaneseTeacher.Data.Сourse;
using JapaneseTeacher.Scenes;

namespace JapaneseTeacher.Data;

internal class LevelTask
{
    public Word Word { get; set; }
    public string Task { get; set; }
    public string Answer { get; set; }
    public Scene Scene { get; set; }
}