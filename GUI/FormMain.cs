using JapaneseTeacher.GUI.Presenters;

namespace JapaneseTeacher.GUI
{
    public partial class FormMain : SceneForm
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            CurrentSceneId = 1;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            CurrentSceneId = 0;
        }
    }
}
