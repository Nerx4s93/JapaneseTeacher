using System;
using System.Windows.Forms;

using JapaneseTeacher.Data;
using JapaneseTeacher.Scenes;
using JapaneseTeacher.Scenes.Content;

namespace JapaneseTeacher.GUI
{
    internal partial class FormMain : Form
    {
        private readonly GlobalData _globeData;
        private readonly SceneManager _sceneManager;

        public FormMain(GlobalData globalData)
        {
            InitializeComponent();
            _globeData = globalData;
            _sceneManager = new SceneManager();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            var theme = _globeData.GetThemeByName("Hiragana");
            _sceneManager.LoadScene(new ThemeScene(), new object[2] { this, theme });
        }
    }
}