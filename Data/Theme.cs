using System.Drawing;

namespace JapaneseTeacher.Data
{
    internal class Theme
    {
        public string Name;
        public string Description;
        public Color Color;

        private VocabularySet VocabularySet;

        public Theme(string name)
        {
            Name = name;
        }

        public void LoadData()
        {
            VocabularySet = new VocabularySet(Name);
            VocabularySet.LoadData();
        }
    }
}
