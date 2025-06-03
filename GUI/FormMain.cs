using System;

using JapaneseTeacher.Data;
using JapaneseTeacher.GUI.Presenters;
using JapaneseTeacher.Tools;

namespace JapaneseTeacher.GUI
{
    internal partial class FormMain : SceneForm
    {
        private readonly GlobalData _globeData;
        private readonly ThemeBuilder _themeBuilder;

        public FormMain(GlobalData globalData)
        {
            InitializeComponent();
            _globeData = globalData;
            _themeBuilder = new ThemeBuilder(this);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            var theme = _globeData.GetThemeByName("Hiragana");
            _themeBuilder.Build(theme);
        }
    }
}
