namespace JapaneseTeacher.Lessons
{
    internal class Lesson
    {
        public readonly int Levels = 0;
        public readonly int CompliteLevels = 0;
        public readonly LevelHandle LevelHandle;

        public Lesson(int compliteLevels, int levels, LevelHandle levelHandle)
        {
            CompliteLevels = compliteLevels;
            Levels = levels;
            LevelHandle = levelHandle;
        }
    }
}
