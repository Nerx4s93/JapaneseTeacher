using System.Collections.Generic;
using System.Windows.Forms;

namespace JapaneseTeacher.GUI.Presenters
{
    public partial class SceneForm : Form
    {
        private int _sceneId;
        private Dictionary<int, List<Control>> _controlsOnScene;

        public int SceneId
        {
            get
            {
                return _sceneId;
            }
            set
            {
                if (_sceneId == value)
                {
                    return;
                }

                if (!_controlsOnScene.ContainsKey(_sceneId))
                {
                    _controlsOnScene[_sceneId] = new List<Control>();
                }

                foreach (Control control in Controls)
                {
                    _controlsOnScene[_sceneId].Add(control);
                }

                Controls.Clear();
                if (_controlsOnScene.TryGetValue(value, out var controls))
                {
                    foreach (var control in controls)
                    {
                        Controls.Add(control);
                    }
                }

                _sceneId = value;
            }
        }

        public SceneForm() : base()
        {
            _sceneId = 0;
            _controlsOnScene = new Dictionary<int, List<Control>>();
        }
    }
}
