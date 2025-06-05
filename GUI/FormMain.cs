using System;

using JapaneseTeacher.Components;
using JapaneseTeacher.Data;
using JapaneseTeacher.GUI.Components;
using JapaneseTeacher.GUI.Presenters;
using JapaneseTeacher.UI;

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
            _themeBuilder.LevelButtonClick += ThemeBuilder_LevelButtonClick;

            _levelBuilder = new LevelBuilder(this);
            _levelBuilder.CompleteLevel += LevelBuilder_CompleteLevel;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            var theme = _globeData.GetThemeByName("Hiragana");
            _themeBuilder.Build(theme);
        }

        private void ThemeBuilder_LevelButtonClick(object sender)
        {
            CurrentSceneId = 2;
            _themeBuilder.StopHandle();
            var theme = _globeData.GetThemeByName("Hiragana");
            var level = (sender as ButtonLevel).Level;
            _levelBuilder.LoadLevel(theme, level);
        }

        private void LevelBuilder_CompleteLevel(Theme theme, string level)
        {
            CurrentSceneId = 1;
            _levelBuilder.StopHandle();
            _themeBuilder.Build(theme);
        }
    }
}
