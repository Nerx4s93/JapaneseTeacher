using System.Windows.Forms;

namespace JapaneseTeacher.GUI.Presenters
{
    public partial class SceneForm : Form
    {
        private int _sceneId;

        public int SceneId
        {
            get
            {
                return _sceneId;
            }
            set
            {
                _sceneId = value;
            }
        }

        public SceneForm()
        {
            InitializeComponent();
        }
    }
}
