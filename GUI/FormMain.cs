using JapaneseTeacher.Data;
using JapaneseTeacher.GUI.Components;
using JapaneseTeacher.GUI.Presenters;
using JapaneseTeacher.Properties;

namespace JapaneseTeacher.GUI
{
    internal partial class FormMain : SceneForm
    {
        private readonly GlobalData _globeData;
        private readonly BackgroundRenderer _backgroundRenderer;
        public FormMain(GlobalData globalData)
        {
            InitializeComponent();
            _globeData = globalData;

            _backgroundRenderer = new BackgroundRenderer(this, Resources.MainFon);
        }
    }
}
