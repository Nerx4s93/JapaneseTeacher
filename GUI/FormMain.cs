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
            var module = _globeData.GetModuleByName("Алфавиты");
            _sceneManager.LoadScene(new ModuleScene(), new object[2] { panelBody, module });
        }

        private void buttonModuleAlphabets_Click(object sender, EventArgs e)
        {
            var module = _globeData.GetModuleByName("Алфавиты");
            _sceneManager.LoadScene(new ModuleScene(), new object[2] { panelBody, module });
        }

        private void buttonModuleStreet_Click(object sender, EventArgs e)
        {
            var module = _globeData.GetModuleByName("Улица");
            _sceneManager.LoadScene(new ModuleScene(), new object[2] { panelBody, module });
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _globeData.SaveData();
        }
    }
}