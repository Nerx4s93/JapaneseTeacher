using System.Collections.Generic;
using System.Drawing;

namespace JapaneseTeacher.Lessons
{
    internal class Theme
    {
        public readonly string Name;
        public readonly string Description;
        public readonly Color Color;
        public readonly List<Lesson> Lessons;

        public Theme(string name, string description, Color color, List<Lesson> lessons)
        {
            Name = name;
            Description = description;
            Color = color;
            Lessons = lessons;
        }
    }
}
