using System;

using JapaneseTeacher.Components;
using JapaneseTeacher.Data;
using JapaneseTeacher.GUI.Components;
using JapaneseTeacher.GUI.Presenters;

namespace JapaneseTeacher.GUI
{
    internal partial class FormMain : SceneFormBase
    {
        private readonly GlobalData _globeData;

        private readonly ThemeBuilder _themeBuilder;
        private readonly LevelBuilder _levelBuilder;

        public FormMain(GlobalData globalData)
        {
            InitializeComponent();
            _globeData = globalData;
            _themeBuilder = new ThemeBuilder(this);
            _levelBuilder = new LevelBuilder(this);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            var theme = _globeData.GetThemeByName("Hiragana");
            _levelBuilder.LoadLevel(theme, "1");
        }
    }
}
