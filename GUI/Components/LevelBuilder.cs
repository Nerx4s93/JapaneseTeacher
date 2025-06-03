using System.Windows.Forms;

using JapaneseTeacher.Data;

namespace JapaneseTeacher.GUI.Components
{
    internal class LevelBuilder
    {
        private readonly Form _form;

        public LevelBuilder(Form form)
        {
            _form = form;
        }

        private Theme _theme;
        private string _levelId;

        public void LoadLevel(Theme theme, string levelId)
        {
            _theme = theme;
            _levelId = levelId;
        }
    }
}
