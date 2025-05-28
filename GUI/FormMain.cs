using JapaneseTeacher.Data;
using JapaneseTeacher.GUI.Presenters;

namespace JapaneseTeacher.GUI
{
    internal partial class FormMain : SceneForm
    {
        private readonly GlobalData _globeData;

        public FormMain(GlobalData globalData)
        {
            InitializeComponent();
            _globeData = globalData;
        }
    }
}
