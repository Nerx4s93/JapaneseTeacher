using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.UI;

namespace JapaneseTeacher.Tools
{
    internal class ThemeBuilder
    {
        private readonly Form _form;

        private readonly ModuleHeader _moduleHeader = new ModuleHeader();

        public ThemeBuilder(Form form)
        {
            _form = form;
        }

        private bool _update;

        public void Build(Theme theme)
        {
            _update = true;
            AdjustControls(theme);

        }

        private void AdjustControls(Theme theme)
        {

        }

        public void StopHandle()
        {
            _update = false;
        }
    }
}
